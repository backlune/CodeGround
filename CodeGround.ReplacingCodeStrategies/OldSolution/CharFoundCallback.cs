using System;
using System.Collections.Generic;

namespace CodeGround.ReplacingCodeStrategies.OldSolution
{
   public class CharFoundCallback : ICharFoundCallback
   {
      private List<string> m_callbacks;

      public CharFoundCallback()
      {
         m_callbacks = new List<string>();
      }

      public List<string> Callbacks
      {
         get { return m_callbacks; }
      }

      #region Implementation of ICharFoundCallback

      public void Begin(DateTime dateTime)
      {
         m_callbacks.Add("Start: " + dateTime.ToLongDateString());
      }

      public void CharacterFound(int index, string sourundingCharacters, int foundCharIndexInString)
      {
         m_callbacks.Add($"at fileindex {index} with {sourundingCharacters} at {foundCharIndexInString}");
      }

      public void End(DateTime dateTime)
      {
         m_callbacks.Add("End: " + dateTime.ToLongDateString());
      }

      #endregion
   }
}