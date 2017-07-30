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
    public interface IDonatesDataAccessService
    {
        ObservableCollection<Donate> GetDonates();
        int CreateDonate(Donate Donate);
        Donor FindDonor(string PESEL);
    }
    public class DonatesDataAccessService : IDonatesDataAccessService
    {
        dbModel context = new dbModel();
        public ObservableCollection<Donate> GetDonates()
        {
            ObservableCollection<Donate> Donates = new ObservableCollection<Donate>();
            context.Dispose();
            context = new dbModel();
            context.Donates.Load();
            Donates = context.Donates.Local;

            return Donates;
        }
        public int CreateDonate(Donate Donate)
        {
            context.Donates.Add(Donate);
            context.SaveChanges();
            return Donate.Id;
        }

        public Donor FindDonor(string PESEL)
        {
            Donor donor = new Donor();
            context.Donors.Load();
            donor = context.Donors.Find(PESEL);
            return donor;
        }


    }
}
