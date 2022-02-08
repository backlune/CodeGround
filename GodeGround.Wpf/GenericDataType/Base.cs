using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.GenericDataType
{
   public abstract class Base<T>
   {
      public MyBasicModel Model { get; set; }

      public Base(MyBasicModel m)
      {
         Model = m;
      }

      public string Name => Model.ToString();

   }
}
