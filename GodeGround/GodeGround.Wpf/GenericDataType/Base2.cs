using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.GenericDataType
{
   public interface IBase2
   {
      object Name { get; }
   }

   public abstract class Base2<T> : Base<T> where T : MyBasicModel, IBase2
   {
      protected Base2(T m) : base(m)
      {
      }
   }
}
