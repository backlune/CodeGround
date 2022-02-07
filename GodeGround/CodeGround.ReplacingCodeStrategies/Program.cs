using CodeGround.ReplacingCodeStrategies.OldSolution;
using GitHub;
using System;
using System.Linq;

namespace CodeGround.ReplacingCodeStrategies
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Scientist.ResultPublisher = new FireAndForgetResultPublisher(new MyResultPublisher(onPublisherException));
            Scientist.ResultPublisher = new MyResultPublisher();

            var fileName = args[0];
            var character = args[1].First();

            //const int percentEnabled = 50;
            //Random rand = new Random();
            //Scientist.Enabled(() => rand.Next(100) < percentEnabled);

            var Exp = new ExperimenterCharacterFinder();
            Exp.Initialize(fileName);
            int index;
            Exp.FoundFirstIndex(character, out index);
            Console.WriteLine("First found at:" + index);
            Exp.Position = 0;

            Exp.FindAll(character, new CharFoundCallback());
        }
    }
}
