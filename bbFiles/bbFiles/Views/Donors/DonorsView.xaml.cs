using bbFiles.Entities;
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

namespace bbFiles.Views
{
    /// <summary>
    /// Logika interakcji dla klasy DonorsView.xaml
    /// </summary>
    public partial class DonorsView : UserControl
    {
        public DonorsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Nie ładuj danych w czasie projektowania.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = ((ViewModel.DonorsViewModel)this.DataContext);
                System.Windows.Data.CollectionViewSource myCollectionViewSource =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("donorViewSource")));
                Binding b = new Binding("Donors");
                b.Source = vm;
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(myCollectionViewSource, CollectionViewSource.SourceProperty, b);
                myCollectionViewSource.Filter += CollectionViewSource_Filter;
            }
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Donor item = e.Item as Donor;
            if (item.PESEL != null)
            {
                if (item.PESEL.StartsWith(SearchTextbox.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void SearchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(donorDataGrid.ItemsSource).Refresh();
        }

    }
}
