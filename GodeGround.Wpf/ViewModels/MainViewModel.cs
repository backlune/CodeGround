using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodeGround.Wpf.GenericDataType;
using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.ViewModels
{
   public class MainViewModel
   {
      public MainViewModel()
      {
         MyBasicModels = new ObservableCollection<Base2>();
         MyBasicModels.Add(new Implementation1(new MyBasicModel() { Name = "hello1"}));
         MyBasicModels.Add(new Implementation1(new MyBasicModel() { Name = "hello2"}));
         MyBasicModels.Add(new Implementation1(new MyBasicModel() { Name = "hello3"}));
         MyBasicModels.Add(new Implementation1(new MyBasicModel() { Name = "hello4"}));
      }

      public ObservableCollection<Base2> MyBasicModels2 { get; set; }

      public ObservableCollection<Base2> MyBasicModels { get; set; }
   }
}
