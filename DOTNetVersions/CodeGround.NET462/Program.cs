using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGround.NET462
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Some of what's new in .Net 4.6.2");

         // File name with more than 260 characters
         // https://blogs.msdn.microsoft.com/dotnet/2016/08/02/announcing-net-framework-4-6-2/
         // https://blogs.msdn.microsoft.com/jeremykuhne/2016/06/21/more-on-new-net-path-handling/
         // Must: The long path changes are controlled via the BlockLongPaths switch, which again is off by default if you target 4.6.2 or higher.

         string filenameOf286Characters =
            "lksldfjsdflkjsdfkljsdfkljsdfkljdsfkljsdfklsdjfklsdjfdsklfjsdklfjsdklfjsdklfjsdklfjsdklfjsdklfjsdklfjslkdfjsldkfjskldfjsdlkjfsdlkjflksdjfslkdfj" +
            "sldkfjslfkjsdlfkjsdlfjsdflkjsdflkjsdflkjsdflksdjflksdjfklsdjflksdjflksdjflksdjflsdkjfslkdfjsdlkfjsdlkfjsdlkfjdslkjfsdlkfjdskjdskfjskfjsdsk.txt";



         Console.ReadLine();

      }
   }
}
