using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp
{
    class CollectionsList
    {
        public ObservableCollection<DataInfo> DataInform { get; set; }
        public ObservableCollection<UserEventsList> EventListInform { get; set; }
    }
}
