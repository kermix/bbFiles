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
    public interface IOrdersDataAccessService
    {
        ObservableCollection<Order> GetOrders();
        int CreateOrder(Order Order);
        Acceptor FindAcceptor(int Id);
        void MarkAsSend(Order Order);
        ObservableCollection<Donate> LinkDonates(Order Order);
    }
    public class OrdersDataAccessService : IOrdersDataAccessService
    {
        dbModel context = new dbModel();
        public ObservableCollection<Order> GetOrders()
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>();
            context.Dispose();
            context = new dbModel();
            context.Orders.Load();
            Orders = context.Orders.Local;
            return Orders;
        }
        public int CreateOrder(Order Order)
        {
            context.Orders.Add(Order);
            context.SaveChanges();
            return Order.Id;
        }

        public Acceptor FindAcceptor(int Id)
        {
            Acceptor acceptor = new Acceptor();
            context.Acceptors.Load();
            acceptor = context.Acceptors.Find(Id);
            return acceptor;
        }

        public void MarkAsSend(Order Order)
        {
            Order.Send = true;
            context.Entry(Order).State = EntityState.Modified;
            context.SaveChanges();
        }

        public ObservableCollection<Donate> LinkDonates(Order Order)
        {
            var avaliableDonates = context.Donates
                .Where(x =>
                    x.Avaliable == true &&
                    x.Donor.Blood_RhMarker == Order.Blood_RhMarker &&
                    x.Donor.Blood_Type == Order.Blood_Type)
               .OrderByDescending(x => x.Amount);

            var linkedDonates = new ObservableCollection<Donate>();
            int neededAmount = Order.Amount;

            foreach(var donate in avaliableDonates)
            {
                if (neededAmount <= 0)
                    break;

                linkedDonates.Add(donate);
                donate.Avaliable = false;
                context.Entry(donate).State = EntityState.Modified;
                neededAmount -= donate.Amount;
            }

            if (neededAmount > 0)
            {
                linkedDonates = new ObservableCollection<Donate>();
                context.Dispose();
                context = new dbModel();
            }
            else
                context.SaveChanges();

            return linkedDonates;
        }
    }
}
