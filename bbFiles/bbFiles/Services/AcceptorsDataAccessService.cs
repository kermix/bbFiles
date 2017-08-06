using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bbFiles.Entities;
using System.Data.Entity;
using GalaSoft.MvvmLight.Messaging;
using bbFiles.Messages;

namespace bbFiles.Services
{
    /// <exclude />
    public interface IAcceptorsDataService
    {
        ObservableCollection<Acceptor> GetAcceptors();
        int CreateAcceptor(Acceptor Acceptor);
        bool DeleteDependentUser(Acceptor Acceptor);
    }
    /// <summary>
    /// Service that serves Db connection and operations for Donates.
    /// </summary>
    public class AcceptorDataAccessService : IAcceptorsDataService
    {
        dbModel context = new dbModel();
        /// <summary>
        /// Gets all acceptors from the db.
        /// </summary>
        /// <returns>Observable collection of all acceptors</returns>
        public ObservableCollection<Acceptor> GetAcceptors()
        {
            ObservableCollection<Acceptor> Acceptors = new ObservableCollection<Acceptor>();
            context.Dispose();
            context = new dbModel();
            context.Acceptors.Load();
            Acceptors = context.Acceptors.Local;
            return Acceptors;
        }
        /// <summary>
        /// Adds or edits the <paramref name="Acceptor"/> to the db.
        /// </summary>
        /// <param name="Acceptor">The donate.</param>
        /// <returns>ID of added of edited acceptor.</returns>
        public int CreateAcceptor(Acceptor Acceptor)
        {
            if ((Acceptor.User.Id == 0) &&
                (context.Users.FirstOrDefault(x => x.Login == Acceptor.User.Login) != default(User)))
                return 0;

            context.Entry(Acceptor).State = Acceptor.Id == 0 ?
                               EntityState.Added :
                               EntityState.Modified;
            context.SaveChanges();
            return Acceptor.Id;
        }

        /// <summary>
        /// Deletes the associeted user from acceptor record.
        /// </summary>
        /// <param name="Acceptor">The acceptor.</param>
        /// <returns>True if deleted or false if there is no associeted user. </returns>
        public bool DeleteDependentUser(Acceptor Acceptor)
        {
            if (Acceptor.User != null)
            {
                context.Users.Attach(Acceptor.User);
                context.Users.Remove(Acceptor.User);
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
