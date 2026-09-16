using Lap;

if (args.Length == 0 || args[0] is "-h" or "--help") { Help(); return; }

int runs = 10, warmup = 1;
var cmd = new List<string>();
bool pastSep = false;

for (int i = 0; i < args.Length; i++)
{
    if (pastSep) { cmd.Add(args[i]); continue; }
    switch (args[i])
    {
        case "--":               pastSep = true; break;
        case "-n" or "--runs":   runs   = int.Parse(args[++i]); break;
        case "-w" or "--warmup": warmup = int.Parse(args[++i]); break;
        default:
            if (!args[i].StartsWith('-')) { cmd.Add(args[i]); pastSep = true; }
            else { Console.Error.WriteLine($"  unknown flag: {args[i]}"); return; }
            break;
    }
}

if (cmd.Count == 0) { Console.Error.WriteLine("  no command given"); return; }

var times = Runner.Measure(cmd, runs, warmup);
Stats.Print(cmd, times, runs, warmup);

static void Help() => Console.WriteLine("""

  lap — benchmark any command

  usage:
    lap [flags] [--] <command> [args]

  flags:
    -n, --runs <n>     timed runs (default: 10)
    -w, --warmup <n>   warmup runs, not counted (default: 1)

  examples:
    lap -- sleep 0.1
    lap -n 20 -- ls /
    lap -n 5 -w 2 -- dotnet build

""");
