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

namespace bbFiles.UserControls.AcceptorManagement
{
    /// <summary>
    /// Logika interakcji dla klasy List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        RefreshList.Refresh<DataGrid, string> refreshList = RefreshList.RefreshAcceptors;
        public List()
        {
            InitializeComponent();
            this.Refresh();
        }

        public void Refresh()
        {
            refreshList(dg_AcceptorManagement);
        }

        public bbFiles.Acceptors GetSelected()
        {
            bbFiles.Acceptors selectedRow = (bbFiles.Acceptors)dg_AcceptorManagement.SelectedItem;
            if (selectedRow == null)
            {
                throw new NullReferenceException("Żadna pozycja nie została wybrana");
            }
            return selectedRow;
        }
    }
}
