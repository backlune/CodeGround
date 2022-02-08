using System;
using System.Threading.Tasks;
using GitHub;

namespace CodeGround.ReplacingCodeStrategies
{
   public class MyResultPublisher : IResultPublisher
   {
      public Task Publish<T, TClean>(Result<T, TClean> result)
      {
         Console.WriteLine(Environment.NewLine + Environment.NewLine);
         Console.WriteLine($"Publishing results for experiment '{result.ExperimentName}'");
         Console.WriteLine($"Result: {(result.Matched ? "MATCH" : "MISMATCH")}");
         Console.WriteLine($"Control value: {result.Control.Value}");
         Console.WriteLine($"Control duration: {result.Control.Duration}");
         foreach (var observation in result.Candidates)
         {

            Console.WriteLine($"Candidate name: {observation.Name}");
            Console.WriteLine($"Candidate value: {observation.Value}");
            Console.WriteLine($"Candidate duration: {observation.Duration}");
         }




         //if (result.Mismatched)
         //{
         //   // saved mismatched experiments to DB
         //   DbHelpers.SaveExperimentResults(result);
         //}

         return Task.FromResult(0);
      }
   }
}