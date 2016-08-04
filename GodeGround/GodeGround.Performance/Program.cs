using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using GodeGround.Performance.Strings;

namespace GodeGround.Performance
{
   class Program
   {
      static void Main(string[] args)
      {
         var summary = BenchmarkRunner.Run<StringBuilderPerformance>();         Console.WriteLine(summary);

         Console.ReadLine();
      }
   }
}
