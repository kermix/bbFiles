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

    /// <exclude />
    public interface IDonorsAccessDataService
    {
        ObservableCollection<Donor> GetDonors();
        string CreateDonor(Donor Donor);
    }

    /// <summary>
    /// Service that serves Db connection and operations for Donors.
    /// </summary>
    class DonorsDataAccessService : IDonorsAccessDataService
    {
        dbModel context = new dbModel();
        /// <summary>
        /// Gets all donors from the db.
        /// </summary>
        /// <returns>Observable collection of all donors</returns>
        public ObservableCollection<Donor> GetDonors()
        {
            ObservableCollection<Donor> Donors = new ObservableCollection<Donor>();
            context.Dispose();
            context = new dbModel();
            context.Donors.Load();
            Donors = context.Donors.Local;
            return Donors;
        }
        /// <summary>
        /// Adds <paramref name="Donor"/> to the db.
        /// </summary>
        /// <param name="Donor">The donate.</param>
        /// <returns>Id of added donor</returns>
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
