using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using GodeGround.Performance.Strings;

namespace GodeGround.Performance
{
   class Program
   {
      static void Main(string[] args)
      {
         //var summary = BenchmarkRunner.Run<ListVsLinkedList>();
         BenchmarkRunner.Run<ForeachAbstractionInLists>(ManualConfig
            .Create(DefaultConfig.Instance).With(new Job { LaunchCount = 10, WarmupCount = 5, TargetCount = 10 }));
         //BenchmarkRunner.Run<ListVsLinkedList>(ManualConfig
         //         .Create(DefaultConfig.Instance).With(Job.Clr));

         // Console.WriteLine(summary);

         Console.ReadLine();
      }
   }
}
