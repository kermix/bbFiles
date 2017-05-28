using System.Linq;
using System.Collections.ObjectModel;

namespace bbFiles
{ 
    partial class Utilities
    {
        public static ObservableCollection<Donates> GetAllDonates()
        {
            var dc = new databaseDataContext();
            var r = from c in dc.Donates select c;
            return new ObservableCollection<Donates>(r);
        }
        public static ObservableCollection<Donates> GetAllDonatesByPESEL(long pesel)
        {
            var dc = new databaseDataContext();
            var r = from c in dc.Donates where c.DonorPESEL.ToString().Contains(pesel.ToString()) select c;
            return new ObservableCollection<Donates>(r);
        }


    }
}
