# lap

benchmark any command. runs it N times, reports mean, min, max, p95, stddev.

```
lap -- sleep 0.1
```
```
  ·········

  sleep 0.1  10 runs, 1 warmup

  mean          103.21ms
  min           101.44ms
  max           108.32ms
  stddev          2.11ms
  median        102.88ms
  p95           107.14ms
```

## usage

```
lap [flags] [--] <command> [args]

  -n, --runs <n>     timed runs (default: 10)
  -w, --warmup <n>   warmup runs, not counted (default: 1)
```

## examples

```
lap -- dotnet build
lap -n 50 -- ls /usr/bin
lap -n 5 -w 3 -- python -c "import numpy"
```

## build

```
dotnet build -c Release
```

.NET 8+.
