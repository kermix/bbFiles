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
                grid.ItemsSource = GetAllAcceptors();
            else if (type == typeof(Donors))
                grid.ItemsSource = GetAllDonors();
        }
    }
}
