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
    /// Logika interakcji dla klasy OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Nie ładuj danych w czasie projektowania.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = ((ViewModel.OrdersViewModel)this.DataContext);
                System.Windows.Data.CollectionViewSource ordersCollectionViewSource =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));

                Binding bindingOrders = new Binding("Orders");
                bindingOrders.Source = vm;
                bindingOrders.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(ordersCollectionViewSource, CollectionViewSource.SourceProperty, bindingOrders);
                ordersCollectionViewSource.Filter += CollectionViewSource_Filter;

                System.Windows.Data.CollectionViewSource donatesCollectionViewSource =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderDonatesViewSource")));

                Binding bindingDonates = new Binding("SelectedOrder.Donates");
                bindingDonates.Source = vm;
                bindingDonates.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(donatesCollectionViewSource, CollectionViewSource.SourceProperty, bindingDonates);
            }
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Order item = e.Item as Order;
            if (item.Acceptor != null && item.Acceptor.Name != null)
            {
                if (item.Acceptor.Name.StartsWith(SearchTextbox.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void SearchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(orderDataGrid.ItemsSource).Refresh();
        }
    }
}
