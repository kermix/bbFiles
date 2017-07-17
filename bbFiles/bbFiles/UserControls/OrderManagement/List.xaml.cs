using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace bbFiles.UserControls.OrdersManagement
{
    /// <summary>
    /// Logika interakcji dla klasy List.xaml
    /// </summary>
    public partial class List : UserControl
    {

        public List()
        {
            InitializeComponent();
            
        }

        private void dg_OrderManagement_LoadingRow(object sender, DataGridRowEventArgs e)
        {
           
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
