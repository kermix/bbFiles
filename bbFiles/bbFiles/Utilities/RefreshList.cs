using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace bbFiles
{ 
    static class RefreshList
    {
        
        public delegate void Refresh<T1, T2>(System.Windows.Controls.DataGrid datagrid, string filter = "");
        public static void RefreshCredentials(System.Windows.Controls.DataGrid grid, string filter = "")
        {
            IQueryable<Credentials> user = from c in (new databaseDataContext()).Credentials
                                           select c;
            grid.ItemsSource = new ObservableCollection<Credentials>(user);
        }
        public static void RefreshAcceptors(System.Windows.Controls.DataGrid grid, string filter)
        {
            IQueryable<Acceptors> acceptor = (from c in (new databaseDataContext()).Acceptors
                                             select c).OrderBy(x => x.UserID);         
            grid.ItemsSource = new ObservableCollection<Acceptors>(acceptor);
        }
        public static void RefreshDonors(System.Windows.Controls.DataGrid grid, string filter)
        {
            IQueryable<Donors> donor = from c in (new databaseDataContext()).Donors
                                       where c.PESEL.ToString().StartsWith(filter.Trim())
                                       select c;
            grid.ItemsSource = new ObservableCollection<Donors>(donor);
        }
        public static void RefreshDonates(System.Windows.Controls.DataGrid grid, string filter)
        {
            IQueryable<Donates> donate = (from c in (new databaseDataContext()).Donates
                                         where c.DonateID.ToString().StartsWith(filter.Trim())
                                         select c).OrderByDescending(x => x.Available);
            grid.ItemsSource = new ObservableCollection<Donates>(donate);
        }
        public static void RefreshDonatesByPesel(System.Windows.Controls.DataGrid grid, string filter)
        {
            IQueryable<Donates> donate = (from c in (new databaseDataContext()).Donates
                                         where c.DonorPESEL.ToString().StartsWith(filter.Trim())
                                         select c).OrderByDescending(x => x.Available);
            grid.ItemsSource = new ObservableCollection<Donates>(donate);
        }
        public static void RefreshOrders(System.Windows.Controls.DataGrid grid, string filter)
        {
            IQueryable<Orders> order = (from c in (new databaseDataContext()).Orders
                                        select c).OrderBy(x => x.Send);
            grid.ItemsSource = new ObservableCollection<Orders>(order);
        }
    }
}
