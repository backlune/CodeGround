using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.GenericDataType
{
   public class Base3 : Base<MyBasicModelDerived> // where T : MyBasicModel
   {
      protected Base3(MyBasicModelDerived m) : base(m)
      {
      }

   }
}