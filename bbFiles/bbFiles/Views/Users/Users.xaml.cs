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

namespace bbFiles.Views.Users
{
    /// <summary>
    /// Logika interakcji dla klasy Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        
        public Users()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel.UsersViewModel)this.DataContext;
            if (viewModel.ReadAllCommand.CanExecute(new List<int>()))
                viewModel.ReadAllCommand.Execute(new List<int>());
            
            
        }

    }
}
