using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace GodeGround.Performance
{
   /// <summary>
   /// Shows how linkedlist does not perform better than list (array list) for doing insert even
   /// though is should according to Big(O) notation. Somewhere at a ratio of 10:1 of inserts vs iterating
   /// 
   /// See: https://jackmott.github.io/programming/2016/08/20/when-bigo-foolsya.html
   /// </summary>
   public class ListVsLinkedList
   {
      private List<int> m_arrayList = new List<int>();
      private const int Inserts = 10;
      private LinkedList<int> m_linkedList = new LinkedList<int>();


      [Benchmark(Baseline = true)]
      public int ArrayTest()
      {
         //In C#, List<T> is an array backed list.
         List<int> local = m_arrayList;
         int localInserts = Inserts;
         int sum = 0;
         for (int i = 0; i < localInserts; i++)
         {
            local.Insert(0, 1); //Insert the number 1 at the front
         }

         // For loops iterate over List<T> much faster than foreach
         for (int i = 0; i < local.Count; i++)
         {
            sum += local[i];  //do some work here so the JIT doesn't elide the loop entirely
         }
         return sum;
      }

      [Benchmark]
      public int ListTest()
      {
         LinkedList<int> local = m_linkedList;
         int localInserts = Inserts;
         int sum = 0;
         for (int i = 0; i < localInserts; i++)
         {
            local.AddFirst(1); //Insert the number 1 at the front
         }

         // Again, iterating the fastest possible way over this collection
         var node = local.First;
         for (int i = 0; i < local.Count; i++)
         {
            sum += node.Value;
            node = node.Next;
         }

         return sum;
      }
   }
}
