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

namespace bbFiles.UserControls.DonatesManagement
{
    /// <summary>
    /// Logika interakcji dla klasy List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        public List()
        {
            InitializeComponent();
            Utilities.RefreshGrid(dg_DonateManagement, typeof(bbFiles.Donates));
        }

        public void Refresh()
        {
            Utilities.RefreshGrid(dg_DonateManagement, typeof(bbFiles.Donates));
        }
        public void Refresh(long pesel)
        {
            Utilities.RefreshGrid(dg_DonateManagement, typeof(bbFiles.Donates), pesel);
        }

        private void dg_DonateManagement_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (((bbFiles.Donates)(e.Row.DataContext)).Available == false )
            {
                e.Row.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
