using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace GodeGround.Performance.Strings
{
   public class StringBuilderPerformance
   {


      [Benchmark(Baseline = true)]
      // Constructs a name like "Foo<T1, T2, T3>"
      public string GenerateFullTypeNameRegular()
      {
         string name = "test";
         int arity = 30;
         StringBuilder sb = new StringBuilder();
         sb.Append(name);
         if (arity != 0)
         {
            sb.Append("<");
            for (int i = 1; i < arity; i++)
            {
               sb.Append('T'); sb.Append(i.ToString());
            }
            sb.Append('T'); sb.Append(arity.ToString());
         }
         return sb.ToString();
      }


      [Benchmark]
      public string GenerateFullTypeName()
      {
         string name = "test";
         int arity = 30;
         StringBuilder sb = AcquireBuilder();
         sb.Append(name);
         if (arity != 0)
         {
            sb.Append("<");
            for (int i = 1; i < arity; i++)
            {
               sb.Append('T'); sb.Append(i.ToString());
            }
            sb.Append('T'); sb.Append(arity.ToString());
         }
         return GetStringAndReleaseBuilder(sb);
      }

      [ThreadStatic]
      private static StringBuilder cachedStringBuilder;


      private static StringBuilder AcquireBuilder()
      {
         StringBuilder result = cachedStringBuilder;
         if (result == null)
         {
            return new StringBuilder();
         }
         result.Clear();
         //cachedStringBuilder = null;
         return result;
      }

      private static string GetStringAndReleaseBuilder(StringBuilder sb)
      {
         string result = sb.ToString();
         cachedStringBuilder = sb;
         return result;
      }


   }
}
