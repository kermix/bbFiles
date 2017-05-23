using System.Linq;
using System.Collections.ObjectModel;

namespace bbFiles
{
    partial class Utilities
    {
        public static ObservableCollection<Donors> GetAllDonors()
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Donors select c;
            return new ObservableCollection<Donors>(r);
        }
    }
}
