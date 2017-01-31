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

      public bool FoundFirstIndex(char c, out int index)
      {
         index = m_fileString.IndexOf(c, Position);
         Position = index;
         return true;
      }

      public void FindAll(char c, ICharFoundCallback callback)
      {
         Position = 0;
         callback.Begin(DateTime.Now);

         while (Position < m_fileString.Length)
         {
            int length = Position + 10 < m_fileString.Length ? 10 : m_fileString.Length - Position;
            var subString = m_fileString.Substring(Position, length);

            var index = subString.IndexOf(c);
            if (index == -1)
            {
               Position += 10;
               continue;
            }

            callback.CharacterFound(Position + index, subString, index);
            Position += index + 1;
         }

         callback.End(DateTime.Now);
      }

      public int Position { get; set; }

      #endregion
   }
}
