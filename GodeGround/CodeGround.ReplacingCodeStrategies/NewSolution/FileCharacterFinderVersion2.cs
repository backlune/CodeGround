using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGround.ReplacingCodeStrategies.OldSolution;

namespace CodeGround.ReplacingCodeStrategies.NewSolution
{
   class FileCharacterFinderVersion2 : IFileCharcterFinder
   {
      private string m_fileName;
      private string m_fileString;

      #region Implementation of IFileCharcterFinder

      public bool Initialize(string fileName)
      {
         m_fileName = fileName;
         m_fileString = File.ReadAllText(m_fileName);
         Position = 0;

         return true;
      }

      public int FintFirstIndex(char c)
      {
         var index = m_fileString.IndexOf(c, Position);
         Position = index;
         return Position;
      }

      public void FindAll(char c, ICharFoundCallback callback)
      {
         Position = 0;
         callback.Begin(DateTime.Now);

         while (Position < m_fileString.Length)
         {
            var subString = m_fileString.Substring(Position, Position + 10);

            var index = subString.IndexOf(c);
            if(index == -1)
               break;
            callback.CharacterFound(Position + index, subString, index);
            Position = index;
         }

         callback.End(DateTime.Now);
      }

      public int Position { get; set; }

      #endregion
   }
}
