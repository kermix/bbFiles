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
    public interface IAcceptorsDataService
    {
        ObservableCollection<Acceptor> GetAcceptors();
        int CreateAcceptor(Acceptor Acceptor);
        bool DeleteDependentUser(Acceptor Acceptor);
    }
    public class AcceptorDataAccessService : IAcceptorsDataService
    {
        dbModel context = new dbModel();
        public ObservableCollection<Acceptor> GetAcceptors()
        {
            ObservableCollection<Acceptor> Acceptors = new ObservableCollection<Acceptor>();
            context.Dispose();
            context = new dbModel();
            context.Acceptors.Load();
            Acceptors = context.Acceptors.Local;
            return Acceptors;
        }

        public int CreateAcceptor(Acceptor Acceptor)
        {
            context.Entry(Acceptor).State = Acceptor.Id == 0 ?
                               EntityState.Added :
                               EntityState.Modified;
            context.SaveChanges();
            return Acceptor.Id;
        }

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
