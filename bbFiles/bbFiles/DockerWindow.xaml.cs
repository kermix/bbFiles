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
using System.Windows.Shapes;

namespace bbFiles
{
    /// <summary>
    /// Logika interakcji dla klasy DockerWindow.xaml
    /// </summary>
    public partial class DockerWindow : Window
    {
        private User user = null;
        public DockerWindow(User user)
        {
            this.user = user;
            InitializeComponent();
            if (user.IsInRole("Admin"))
            {
                btn_Acceptors.IsEnabled = true;
                btn_Accepts.IsEnabled = true;
                btn_Donates.IsEnabled = true;
                btn_Donors.IsEnabled = true;
                btn_User.IsEnabled = true;
                btn_Statistics.IsEnabled = true;
            }
            else if (user.IsInRole("Employee"))
            {
                btn_Acceptors.IsEnabled = true;
                btn_Accepts.IsEnabled = true;
                btn_Donates.IsEnabled = true;
                btn_Donors.IsEnabled = true;
                btn_Statistics.IsEnabled = true;
            }
            else if (user.IsInRole("Acceptor"))
            {
                btn_Accepts.IsEnabled = true;
                btn_Statistics.IsEnabled = true;
            }
            else
            {
                throw new UndefinedUserRole(Properties.Strings.UndefinedRole);
            }
            cc_Content.Content = new UserControls.Statistics();
        }

        private void btn_UserClick(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserControls.Users(user);
        }

        private void btn_Acceptors_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserControls.Acceptors(user);
        }

        private void btn_Statistics_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserControls.Statistics();
        }

        private void btn_Donors_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserControls.Donors(user);
        }

        private void btn_Donates_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserControls.Donates(user);
        }
    }
}
