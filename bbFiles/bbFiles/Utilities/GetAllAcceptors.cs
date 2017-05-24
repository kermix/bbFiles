using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace bbFiles
{
    partial class Utilities
    {
        public static ObservableCollection<Acceptors> GetAllAcceptors()
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Acceptors select c;
            return new ObservableCollection<Acceptors>(r);
        }

        public static ObservableCollection<Acceptors> GetAllAcceptors(string name)
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Acceptors where c.AcceptorName == name select c;
            return new ObservableCollection<Acceptors>(r);
        }
    }
}
