namespace Lap;

static class Stats
{
    public static void Print(List<string> cmd, double[] ms, int runs, int warmup)
    {
        var sorted = ms.OrderBy(x => x).ToArray();
        double mean   = ms.Average();
        double min    = sorted[0];
        double max    = sorted[^1];
        double stddev = Math.Sqrt(ms.Select(x => Math.Pow(x - mean, 2)).Average());
        double median = P(sorted, 0.50);
        double p95    = P(sorted, 0.95);

        string F(double v) => v >= 1000 ? $"{v / 1000:F3}s" : $"{v:F2}ms";

        Console.WriteLine();
        Console.WriteLine($"  \x1b[1m{string.Join(' ', cmd)}\x1b[0m  \x1b[2m{runs} runs, {warmup} warmup\x1b[0m");
        Console.WriteLine();
        Row("mean",   F(mean),   true);
        Row("min",    F(min));
        Row("max",    F(max));
        Row("stddev", F(stddev));
        Row("median", F(median));
        Row("p95",    F(p95));
        Console.WriteLine();
    }

    static void Row(string label, string val, bool bold = false)
    {
        string v = bold ? $"\x1b[1m{val}\x1b[0m" : val;
        Console.WriteLine($"  {label,-8} {v,12}");
    }

    static double P(double[] sorted, double p)
    {
        double idx = p * (sorted.Length - 1);
        int lo = (int)idx, hi = Math.Min(lo + 1, sorted.Length - 1);
        return sorted[lo] + (sorted[hi] - sorted[lo]) * (idx - lo);
    }
}
