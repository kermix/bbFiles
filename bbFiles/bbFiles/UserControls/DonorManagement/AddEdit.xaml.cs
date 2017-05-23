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

namespace bbFiles.UserControls.DonorManagement
{
    /// <summary>
    /// Logika interakcji dla klasy AddEdit.xaml
    /// </summary>
    public partial class AddEdit : UserControl
    {
        User user;
        public AddEdit(User User)
        {
            Privileges.CheckAdmin(user);
            InitializeComponent();
            this.user = User;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
