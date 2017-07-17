using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace bbFiles.UserControls.OrdersManagement
{
    /// <summary>
    /// Logika interakcji dla klasy List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        RefreshList.Refresh<DataGrid, string> refreshList = RefreshList.RefreshOrders;
        public List()
        {
            InitializeComponent();
            this.Refresh();
        }

        public void Refresh()
        {
            refreshList(dg_OrderManagement);
        }

        public bbFiles.Orders GetSelected()
        {
            bbFiles.Orders selectedRow = (bbFiles.Orders)dg_OrderManagement.SelectedItem;
            if (selectedRow == null)
            {
                throw new NullReferenceException(Properties.Strings.PositionIsNotSelected);
            }
            return selectedRow;
        }
    }
    [ValueConversion(typeof(string), typeof(List<string>))]
    public class DonatesIDsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Split(',').ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
