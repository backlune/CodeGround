using System;
using System.Collections.Generic;

namespace CodeGround.ReplacingCodeStrategies.OldSolution
{
   public interface ICharFoundCallback
   {
      void Begin(DateTime dateTime);

      void CharacterFound(int index, string sourundingCharacters, int foundCharIndexInString);

      void End(DateTime dateTime);

   }
}