using System.Linq;
using System.Collections.ObjectModel;

namespace bbFiles
{
    partial class Utilities
    {
        public static ObservableCollection<Donors> GetAllDonors()
        {
            var dc = new databaseDataContext();
            var r = from c in dc.Donors select c;
            return new ObservableCollection<Donors>(r);
        }
        public static ObservableCollection<Donors> GetAllDonorsByPESEL(long pesel)
        {
            var dc = new databaseDataContext();
            var r = from c in dc.Donors where c.PESEL.ToString().Contains(pesel.ToString()) select c;
            return new ObservableCollection<Donors>(r);
        }
    }
}
