using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.GenericDataType
{

   public abstract class Base2 : Base<MyBasicModel> // where T : MyBasicModel
   {
      protected Base2(MyBasicModel m) : base(m)
      {
      }

   }
}
