using System.Reflection;

namespace CodeGround.ReplacingCodeStrategies.OldSolution
{
   public interface IFileCharcterFinder
   {
      bool Initialize(string fileName);

      bool FoundFirstIndex(char c, out int index);

      void FindAll(char c, ICharFoundCallback callback);

      int Position { get; set; }

   }
}