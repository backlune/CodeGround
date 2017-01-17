using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodeGround.Wpf.Models;

namespace GodeGround.Wpf.ViewModels
{
   class MainViewModel
   {

      public ObservableCollection<MyBasicModel> MyBasicModels { get; set; }
   }
}
