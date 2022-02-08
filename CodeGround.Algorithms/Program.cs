using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGround.Algorithms
{
   class Program
   {
      static void Main(string[] args)
      {
         var array = new[] { 3, 2, 1, 9, 8, 7, 5, 6, 4 };

         var sorted = Sorting.QuickSort(array.ToList());

         Console.WriteLine(string.Join(", ", sorted));
         Console.ReadLine();
      }
   }
}
