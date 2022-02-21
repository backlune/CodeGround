using System;
using System.Collections.Generic;
using System.Text;

namespace GodeGround.Base.CSharpVersions
{
    internal class Eight
    {


        void Indecies()
        {
            var words = new string[]
                {
                                // index from start    index from end
                    "The",      // 0                   ^9
                    "quick",    // 1                   ^8
                    "brown",    // 2                   ^7
                    "fox",      // 3                   ^6
                    "jumped",   // 4                   ^5
                    "over",     // 5                   ^4
                    "the",      // 6                   ^3
                    "lazy",     // 7                   ^2
                    "dog"       // 8                   ^1
                };

            var l = words.Length;
            int middle = l / 2;
            var quickBrownFox = words[1..4];

            var firstPart = words[..middle];
            //var secondPart = words[middle+1..]; // Not allowed to do +1 on Range
            var lastWord = words[^1];
            
        }

        void NullCoalesingAssigment()
        {
            List<int> numbers = null;
            int? i = null;

            numbers ??= new List<int>();
            numbers.Add(i ??= 17);
            numbers.Add(i ??= 20);

            Console.WriteLine(string.Join(" ", numbers));  // output: 17 17
            Console.WriteLine(i);  // output: 17
        }

    }
}
