using bbFiles.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            if(item.Login != null)
            {
                if (item.Login.Contains(SearchTextbox.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void SearchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(userDataGrid.ItemsSource).Refresh();

        }
    }
}
