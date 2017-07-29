using bbFiles.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Services
{
    public interface IDonorsAccessDataService
    {
        ObservableCollection<Donor> GetDonors();
        string CreateDonor(Donor Donor);
    }
    class DonorsAccessDataService : IDonorsAccessDataService
    {
        dbModel context = new dbModel();

        public ObservableCollection<Donor> GetDonors()
        {
            context.Dispose();
            context = new dbModel();
            context.Users.Load();
            ObservableCollection<Donor> Donors = context.Donors.Local;
            return Donors;
        }

        public string CreateDonor(Donor Donor)
        {
            
            context.Entry(Donor).State = context.Donors.Any(x => x.PESEL == Donor.PESEL) ?
                                    EntityState.Modified :
                                    EntityState.Added;
           
            context.SaveChanges();
            return Donor.PESEL;
        }
    }
}
