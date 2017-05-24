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
        public static ObservableCollection<Donors> GetAllDonorsBySurname(string name)
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Donors where c.Surname == name select c;
            return new ObservableCollection<Donors>(r);
        }
        public static ObservableCollection<Donors> GetAllDonorsByPESEL(int pesel)
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Donors where c.PESEL == pesel select c;
            return new ObservableCollection<Donors>(r);
        }
    }
}
