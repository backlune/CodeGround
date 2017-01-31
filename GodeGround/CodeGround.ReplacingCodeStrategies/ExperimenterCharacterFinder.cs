using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGround.ReplacingCodeStrategies.NewSolution;
using CodeGround.ReplacingCodeStrategies.OldSolution;
using GitHub;

namespace CodeGround.ReplacingCodeStrategies
{
   //TODO EB 20170120: here we should tests the two solution.
   public class ExperimenterCharacterFinder : IFileCharcterFinder
   {
      private IFileCharcterFinder m_oldImpl;
      private IFileCharcterFinder m_newImpl;
      private List<string> m_callbacks;

      public ExperimenterCharacterFinder()
      {
         m_oldImpl = new FileCharacterFinder();
         m_newImpl = new FileCharacterFinderVersion2();
         m_callbacks = new List<string>();
      }

      #region Implementation of IFileCharcterFinder

      public bool Initialize(string fileName)
      {
         return Scientist.Science<bool>("Init", experiment =>
         {
            experiment.Use(() => m_oldImpl.Initialize(fileName));
            experiment.Try(() => m_newImpl.Initialize(fileName));
         });
      }

      public bool FoundFirstIndex(char c, out int index)
      {
         bool retval = false;
         int nativeIndex = 0;
         Scientist.Science<int>("FintFirstIndex", experiment =>
         {
            experiment.Use(() => {  retval = m_oldImpl.FoundFirstIndex(c, out nativeIndex); return nativeIndex;  });
            experiment.Try(() =>
            {
               int managedIndex;
               m_newImpl.FoundFirstIndex(c, out managedIndex);
               return managedIndex;
            });
         });

         index = nativeIndex;
         return retval;
      }

      public void FindAll(char c, ICharFoundCallback callback)
      {
         var actualCallback1 = callback as CharFoundCallback;
         var actualCallback2 = new CharFoundCallback();
         Scientist.Science<string>("FindAll", experiment =>
         {
            experiment.Use(() => { m_oldImpl.FindAll(c, actualCallback1); return string.Join("\r\n", actualCallback1.Callbacks); });
            experiment.Try(() => { m_newImpl.FindAll(c, actualCallback2); return string.Join("\r\n", actualCallback2.Callbacks); });
         });
      }

      public int Position { get; set; }

      #endregion

   }

}
