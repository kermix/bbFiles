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

namespace bbFiles.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Donors.xaml
    /// </summary>
    public partial class Donors : UserControl
    {
        UserControls.DonorManagement.List DonorsList = new UserControls.DonorManagement.List();
        bool _editEnded;
        public User user { get; set; }
        public bool editEnded
        {
            set
            {
                _editEnded = value;
                if (value == true)
                {
                    g_Manage.IsEnabled = true;
                    cc_Content.Content = DonorsList;
                    DonorsList.Refresh();
                }
                else
                {
                    g_Manage.IsEnabled = false;
                }
            }
            get
            {
                return _editEnded;
            }
        }
        public Donors(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = DonorsList;
        }

        private void btn_AddDonor_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btn_EditDonor_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            DonorsList.Refresh();
        }


    }
}
