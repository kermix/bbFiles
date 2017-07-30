using bbFiles.Entities;
using bbFiles.Messages;
using GalaSoft.MvvmLight.Messaging;
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
    class DonorsDataAccessService : IDonorsAccessDataService
    {
        dbModel context = new dbModel();
        public ObservableCollection<Donor> GetDonors()
        {
            ObservableCollection<Donor> Donors = new ObservableCollection<Donor>();
            context.Dispose();
            context = new dbModel();
            context.Donors.Load();
            Donors = context.Donors.Local;
            return Donors;
        }

        public string CreateDonor(Donor Donor)
        {
            if (!context.Donors.Any(x => x.PESEL == Donor.PESEL))
            {
                context.Entry(Donor).State = EntityState.Added;
                context.SaveChanges();
                return Donor.PESEL;
            }
            else
                return "";
        }
    }
}
