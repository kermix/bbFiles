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

namespace bbFiles.UserControls.DonorManagement
{
    /// <summary>
    /// Logika interakcji dla klasy List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        RefreshList.Refresh<DataGrid, string> refreshList = RefreshList.RefreshDonors;
        public List()
        {
            InitializeComponent();
            this.Refresh();
    }
        public void Refresh()
        {
           refreshList(dg_DonorManagement);
        }
        public void Refresh(long pesel)
        {
            refreshList(dg_DonorManagement, pesel.ToString());
        }
        public bbFiles.Donors GetSelected()
        {
            bbFiles.Donors selectedRow = (bbFiles.Donors)dg_DonorManagement.SelectedItem;
            if (selectedRow == null)
            {
                throw new NullReferenceException(Properties.Strings.PositionIsNotSelected);
            }
            return selectedRow;
        }
    }
}
