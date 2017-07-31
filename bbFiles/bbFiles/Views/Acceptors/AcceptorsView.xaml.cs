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
    /// Logika interakcji dla klasy AcceptorsView.xaml
    /// </summary>
    public partial class AcceptorsView : UserControl
    {
        public AcceptorsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Nie ładuj danych w czasie projektowania.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = (ViewModel.AcceptorsViewModel)this.DataContext;

                System.Windows.Data.CollectionViewSource acceptorsCollectionViewSource =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("acceptorViewSource")));
                Binding acceptorsBinding = new Binding("Acceptors");
                acceptorsBinding.Source = vm;
                acceptorsBinding.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(acceptorsCollectionViewSource, CollectionViewSource.SourceProperty, acceptorsBinding);

                System.Windows.Data.CollectionViewSource acceptorOrdersCollectionViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("acceptorOrdersViewSource")));
                Binding ordersBinding = new Binding("SelectedAcceptor.Orders");
                ordersBinding.Source = vm;
                ordersBinding.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(acceptorOrdersCollectionViewSource, CollectionViewSource.SourceProperty, ordersBinding);



                acceptorsCollectionViewSource.Filter += CollectionViewSource_Filter;
            }
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        { 
            Acceptor item = e.Item as Acceptor;
            if (item.Name != null)
            {
                if (item.Name.Contains(SearchTextbox.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void SearchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(acceptorDataGrid.ItemsSource).Refresh();
        }

    }
}
