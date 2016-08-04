```ini

Host Process Environment Information:
BenchmarkDotNet=v0.9.8.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Xeon(R) CPU E5-1620 v3 3.50GHz, ProcessorCount=8
Frequency=3410080 ticks, Resolution=293.2483 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit DEBUG
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1080.0

Type=StringBuilderPerformance  Mode=Throughput  GarbageCollection=Concurrent Workstation  

```
                      Method |    Median |    StdDev | Scaled |
---------------------------- |---------- |---------- |------- |
 GenerateFullTypeNameRegular | 2.9351 us | 0.1291 us |   1.00 |
        GenerateFullTypeName | 2.6774 us | 0.0345 us |   0.91 |
