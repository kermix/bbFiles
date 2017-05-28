using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles
{
    partial class Utilities
    {
        public static void RefreshGrid(System.Windows.Controls.DataGrid grid, Type type)
        {
            if (type == typeof(Credentials))
                grid.ItemsSource = GetAllCredentials();
            else if (type == typeof(Acceptors))
                grid.ItemsSource = GetAllAcceptors().OrderByDescending(x => x.UserID);
            else if (type == typeof(Donors))
                grid.ItemsSource = GetAllDonors();
            else if (type == typeof(Donates))
                grid.ItemsSource = GetAllDonates().OrderByDescending(x => x.Available); 
        }

        public static void RefreshGrid(System.Windows.Controls.DataGrid grid, Type type, long filter)
        {
            if (type == typeof(Donors))
                grid.ItemsSource = GetAllDonorsByPESEL(filter);
            if (type == typeof(Donates))
                grid.ItemsSource = GetAllDonatesByPESEL(filter).OrderByDescending(x => x.Available);
        }
    }
}
