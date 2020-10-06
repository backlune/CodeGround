using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodeGround.Base
{
    class SwitchCaseOptions
    {
    }

    enum Color { Red, Green, Blue, Rainbow }

    internal class Example
    {
        public static void GroupingWithDefault()
        {
            Color c = (Color)(new Random()).Next(0, 3);
            switch (c)
            {
                case Color.Red:
                    Console.WriteLine("The color is red");
                    break;
                case Color.Green:
                    Console.WriteLine("The color is green");
                    break;
                case Color.Blue:
                    Console.WriteLine("The color is blue");
                    break;
                case Color.Rainbow:
                default:
                    Console.WriteLine("The color is unknown.");
                    break;
            }
        }
    }
}
