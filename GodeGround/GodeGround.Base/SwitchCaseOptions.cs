using System;
using System.Linq;

namespace GodeGround.Base
{
    class SwitchCaseOptions
    {
    }

    enum Color { Red, Green, Blue, Rainbow }

    internal class Example
    {
        public static string GroupingWithDefault()
        {
            Color c = (Color)(new Random()).Next(0, 3);
            switch (c)
            {
                case Color.Red:
                    return "The color is red";
                case Color.Green:
                    return "The color is green";
                case Color.Blue:
                    return "The color is blue";
                case Color.Rainbow:
                default:
                    return "The color is unknown.";
            }
        }

        public static string WithPatternMatching()
        {
            Color c = (Color)(new Random()).Next(0, 3);
            return c switch
            {
                Color.Red => "The color is red",
                Color.Green => "The color is green",
                Color.Blue => "The color is blue",
                _ => "The color is unknown."
            };
        }

        public static string WithPatternMatchingInline(Color c) =>
             c switch
             {
                 Color.Red => "The color is red",
                 Color.Green => "The color is green",
                 Color.Blue => "The color is blue",
                 _ => "The color is unknown."
             };
    }
}
