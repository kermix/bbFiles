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
    /// <exclude />
    public interface IOrdersDataAccessService
    {
        ObservableCollection<Order> GetOrders();
        int CreateOrder(Order Order);
        Acceptor FindAcceptor(int Id);
        void MarkAsSend(Order Order);
        ObservableCollection<Donate> LinkDonates(Order Order);
    }
    /// <summary>
    /// Serves connecition and operations with db for Orders.
    /// </summary>
    public class OrdersDataAccessService : IOrdersDataAccessService
    {
        dbModel context = new dbModel();
        /// <summary>
        /// Gets all orders from the db.
        /// </summary>
        /// <returns>Observable collection of all orders</returns>
        public ObservableCollection<Order> GetOrders()
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>();
            context.Dispose();
            context = new dbModel();
            context.Orders.Load();
            Orders = context.Orders.Local;
            return Orders;
        }

        /// <summary>
        /// Adds an <paramref name="Order"/> to the db.
        /// </summary>
        /// <param name="Order">The order.</param>
        /// <returns>ID of the added order.</returns>
        public int CreateOrder(Order Order)
        {
            context.Orders.Add(Order);
            context.SaveChanges();
            return Order.Id;
        }

        /// <summary>
        /// Finds the acceptor by ID.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Acceptor or null if nothing matched</returns>
        public Acceptor FindAcceptor(int Id)
        {
            Acceptor acceptor = new Acceptor();
            context.Acceptors.Load();
            acceptor = context.Acceptors.Find(Id);
            return acceptor;
        }

        /// <summary>
        /// Changes Send value of the Order to true.
        /// </summary>
        /// <param name="Order">The order.</param>
        public void MarkAsSend(Order Order)
        {
            Order.Send = true;
            context.Entry(Order).State = EntityState.Modified;
            context.SaveChanges();
        }


        /// <summary>
        /// Finds proper donates for an <paramref name="Order"/>. When there is need amount of blood in bank sets Avaliable to false in choosen donates.
        /// Makes changes to the statistics.
        /// </summary>
        /// <param name="Order">The order.</param>
        /// <returns>Obeservable collection of choosen donates or empty observable collection if there is not enought blood in bank.</returns>
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


            BloodTypeMarker BloodTypeMarker;
            switch (Order.Blood_Type)
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
            if (Order.Blood_RhMarker)
                BloodTypeMarker += 1;


            int blockerAmount = linkedDonates.Select(x => x.Amount).Sum();
            var stat = context.Statistics.Find(BloodTypeMarker);
            stat.TotalAmount -= blockerAmount;
            context.Entry(stat).State = EntityState.Modified;

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
