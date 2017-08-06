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
            BloodTypeMarker BloodTypeMarker;

            switch (Donate.Donor.Blood_Type)
            {
                case BloodType.O:
                        BloodTypeMarker = BloodTypeMarker.O;
                    break;
                case BloodType.A:
                        BloodTypeMarker = BloodTypeMarker.A;
                    break;
                case BloodType.B:
                        BloodTypeMarker = BloodTypeMarker.B;
                    break;
                default:
                        BloodTypeMarker = BloodTypeMarker.AB;
                    break;
            }

            if (Donate.Donor.Blood_RhMarker)
                BloodTypeMarker += 1;

            var stat = context.Statistics.Find(BloodTypeMarker);
            stat.TotalAmount += Donate.Amount;
            context.Entry(stat).State = EntityState.Modified;

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
