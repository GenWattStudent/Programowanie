using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
{
    public class StringListModel
    {
        public ObservableCollection<string> Items { get; set; }
    }
}
