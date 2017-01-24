using System;
using System.IO;
using System.Linq;

namespace CodeGround.ReplacingCodeStrategies.OldSolution
{
   public class FileCharacterFinder : IFileCharcterFinder, IDisposable
   {
      private string m_fileName;
      private FileStream m_fileStream;

      #region Implementation of IFileCharcterFinder

      public bool Initialize(string fileName)
      {
         m_fileName = fileName;
         m_fileStream = File.Open(m_fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
         Position = 0;

         return true;
      }

      public int FintFirstIndex(char c)
      {
         var sr = new StreamReader(m_fileStream);
         
         while (Position < m_fileStream.Length)
         {
            m_fileStream.Position = Position;
            char[] buffer = new char[12];
            int readCount = sr.ReadBlock(buffer, 0, 10);
            for (int i = 0; i < buffer.Length; i++)
            {
               if (c == buffer[i])
               {
                  Position += i;
                  return Position;

               }
            }
            Position += readCount;
         }
         return -1;
      }

      public void FindAll(char c, ICharFoundCallback callback)
      {
         callback.Begin(DateTime.Now);

         var sr = new StreamReader(m_fileStream);
         char[] buffer = new char[10];
         while (Position < m_fileStream.Length)
         {
            m_fileStream.Position = Position;
            int readCount = sr.Read(buffer, 0, 10);
            for (int i = 0; i < buffer.Length; i++)
            {
               if (c == buffer[i])
               {
                  Position += i;
                  callback.CharacterFound(Position, new string(buffer), i);
               }
            }
            Position += readCount;
            m_fileStream.Position = Position;
         }


         callback.End(DateTime.Now);
      }

      public int Position { get; set; }

      #endregion

      #region Implementation of IDisposable

      public void Dispose()
      {
         if (m_fileStream != null)
         {
            m_fileStream.Close();
            m_fileStream = null;
         }


      }

      #endregion
   }
}