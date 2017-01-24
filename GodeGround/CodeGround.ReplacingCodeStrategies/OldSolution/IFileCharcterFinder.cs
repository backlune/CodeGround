using System.Reflection;

namespace CodeGround.ReplacingCodeStrategies.OldSolution
{
   public interface IFileCharcterFinder
   {
      bool Initialize(string fileName);

      int FintFirstIndex(char c);

      void FindAll(char c, ICharFoundCallback callback);

      int Position { get; set; }

   }
}