using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bbFiles.UserControls.UserManagement
{
    /// <summary>
    /// Logika interakcji dla klasy List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        public List()
        {
            InitializeComponent();
            Utilities.RefreshGrid(UserManagementDataGrid, typeof(Credentials));
        }

        public void Refresh()
        {
            Utilities.RefreshGrid(UserManagementDataGrid, typeof(Credentials));
        }
        public Credentials GetSelected()
        {
            Credentials selectedRow = (Credentials)UserManagementDataGrid.SelectedItem;
            if(selectedRow == null)
            {
                throw new NullReferenceException(Properties.Strings.PositionIsNotSelected);
            }
            return selectedRow;
        }


    }
}
