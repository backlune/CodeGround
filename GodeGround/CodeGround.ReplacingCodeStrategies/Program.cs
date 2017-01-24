using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGround.ReplacingCodeStrategies.OldSolution;
using GitHub;

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

         var Exp = new ExperimenterCharacterFinder();
         Exp.Initialize(fileName);
         Console.WriteLine("First found at:" + Exp.FintFirstIndex(character));
         Exp.Position = 0;

         Exp.FindAll(character, new CharFoundCallback());
      }
   }
}
