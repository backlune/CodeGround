using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.GenericDataType
{
   public class Implementation2 : Base3
   {
      public Implementation2(MyBasicModel m) : base(m as MyBasicModelDerived) 
      {
      }


   }
}