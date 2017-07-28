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
using bbFiles.Entities;
using System.Data.Entity;
using System.ComponentModel;

namespace bbFiles.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Users.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {

        public UsersView()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Nie ładuj danych w czasie projektowania.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = ((ViewModel.UsersViewModel)this.DataContext);
                System.Windows.Data.CollectionViewSource myCollectionViewSource =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
                Binding b = new Binding("Users");
                b.Source = vm;
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(myCollectionViewSource, CollectionViewSource.SourceProperty, b);
            }
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            User item = e.Item as User;
            if (item.Login.Contains(SearchTextbox.Text))
                e.Accepted = true;
            else
                e.Accepted = false;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(userDataGrid.ItemsSource).Refresh();
        }
    }
}
