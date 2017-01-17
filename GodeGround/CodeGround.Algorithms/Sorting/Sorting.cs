using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGround.Algorithms
{
   public class Sorting
   {
      public static List<int> QuickSort(List<int> array)
      {
         //TODO: This is not the correct implementation!
         if (array.Count < 1)
            return array;

         int pivotIndex = array.Count / 2;
         int pivot = array[pivotIndex];


         var larger = new List<int>();
         var smaller = new List<int>();
         for (int i = 0; i < array.Count; i++) // O(n)
         {
            if (i != pivotIndex)
            {
               if (array[i] < pivot)
                  smaller.Add(array[i]);
               else
                  larger.Add(array[i]);
            }
         }

         List<int> sortedArray = new List<int>();
         sortedArray.AddRange(QuickSort(smaller)); // O(log n)
         sortedArray.Add(pivot);
         sortedArray.AddRange(QuickSort(larger)); // O(log n)
         return sortedArray;
      }
   }
}
