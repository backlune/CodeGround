using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace GodeGround.Performance
{
   /// <summary>
   /// " Notice that the foreach abstraction works out well with raw Arrays, but not with Array List or Linked List."
   /// See: https://jackmott.github.io/programming/2016/08/20/when-bigo-foolsya.html
   /// </summary>
   public class ForeachAbstractionInLists
   {
      private const int Entries = 10000;

      //TODO: Need to initialize lists!
      private LinkedList<int> linkedList = new LinkedList<int>();
     private List<int> arrayList = new List<int>();
     private int[] rawArray = new int[Entries];

      public ForeachAbstractionInLists()
      {
         for (int i = 0; i < Entries; i++)
         {
            linkedList.AddLast(i);
            arrayList.Add(i);
            rawArray[i] = i;
         }
      }

     [Benchmark(Baseline = true)]
      public int LinkedListLinq()
      {
         var local = linkedList;
         return local.Sum();
      }

      [Benchmark]
      public int LinkedListForEach()
      {
         var local = linkedList;
         int sum = 0;
         checked
         {
            foreach (var node in local)
            {
               sum += node;
            }
         }
         return sum;
      }

      [Benchmark]
      public int LinkedListFor()
      {
         var local = linkedList;
         int sum = 0;
         var node = local.First;
         for (int i = 0; i < local.Count; i++)
         {
            checked
            {
               sum += node.Value;
               node = node.Next;
            }
         }

         return sum;
      }

      [Benchmark]
      public int ArrayListFor()
      {
         //In C#, List<T> is an array backed list
         List<int> local = arrayList;
         int sum = 0;

         for (int i = 0; i < local.Count; i++)
         {
            checked
            {
               sum += local[i];
            }
         }

         return sum;
      }

      [Benchmark]
      public int ArrayListForEach()
      {
         //In C#, List<T> is an array backed list
         List<int> local = arrayList;
         int sum = 0;
         checked
         {
            foreach (var x in local)
            {
               sum += x;
            }
         }
         return sum;
      }

      [Benchmark]
      public int RawArrayLinq()
      {
         int[] local = rawArray;
         return local.Sum();
      }

      [Benchmark]
      public int RawArrayForEach()
      {
         int[] local = rawArray;
         int sum = 0;
         checked
         {
            foreach (var x in local)
            {
               sum += x;
            }
         }
         return sum;
      }

      [Benchmark]
      public int RawArrayFor()
      {
         int[] local = rawArray;
         int sum = 0;

         for (int i = 0; i < local.Length; i++)
         {
            checked
            {
               sum += local[i];
            }
         }

         return sum;
      }
   }
}
