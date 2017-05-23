﻿using System.Linq;
using System.Collections.ObjectModel;

namespace bbFiles
{
    partial class Utilities
    {
        public static ObservableCollection<Credentials> GetAllCredentials()
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Credentials select c;
            return new ObservableCollection<Credentials>(r);
        }
        public static ObservableCollection<Credentials> GetAllCredentials(string username)
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Credentials where c.Login == username select c;
            return new ObservableCollection<Credentials>(r);
        }
    }
}
