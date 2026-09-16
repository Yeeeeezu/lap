using System.Diagnostics;

namespace Lap;

static class Runner
{
    public static double[] Measure(List<string> cmd, int runs, int warmup)
    {
        var results = new double[runs];
        Console.Write("  ");

        for (int i = 0; i < warmup + runs; i++)
        {
            var psi = new ProcessStartInfo(cmd[0], string.Join(' ', cmd.Skip(1)))
            {
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
            };

            var sw = Stopwatch.StartNew();
            using var p = Process.Start(psi)!;
            p.WaitForExit();
            sw.Stop();

            if (i < warmup)
            {
                Console.Write("\x1b[2m·\x1b[0m");
                continue;
            }
            results[i - warmup] = sw.Elapsed.TotalMilliseconds;
            Console.Write("\x1b[32m·\x1b[0m");
        }

        Console.WriteLine();
        return results;
    }
}
