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
    /// Logika interakcji dla klasy DonatesView.xaml
    /// </summary>
    public partial class DonatesView : UserControl
    {
        public DonatesView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Nie ładuj danych w czasie projektowania.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = ((ViewModel.DonatesViewModel)this.DataContext);
                System.Windows.Data.CollectionViewSource myCollectionViewSource =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("donateViewSource")));
                Binding b = new Binding("Donates");
                b.Source = vm;
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(myCollectionViewSource, CollectionViewSource.SourceProperty, b);
                myCollectionViewSource.Filter += CollectionViewSource_Filter;
            }
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Donate item = e.Item as Donate;
            if (item.Donor_PESEL != null)
            {
                if (item.Donor_PESEL.StartsWith(SearchTextbox.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void SearchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(donateDataGrid.ItemsSource).Refresh();
        }
    }
}
