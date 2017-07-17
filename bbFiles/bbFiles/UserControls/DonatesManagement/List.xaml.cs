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
        RefreshList.Refresh<DataGrid, string> refreshList;
        public List()
        {
            InitializeComponent();
            this.Refresh();
        }
        public void Refresh()
        {
            refreshList = RefreshList.RefreshDonates;
            refreshList(dg_DonateManagement);
        }
        public void Refresh(long filter, bool isID = true)
        {
            if (isID)
                refreshList = RefreshList.RefreshDonates;
            else
                refreshList = RefreshList.RefreshDonatesByPesel;
            refreshList(dg_DonateManagement, filter.ToString());
        }
    }
}
