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
    /// Logika interakcji dla klasy OrdersManagement.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        UserControls.OrdersManagement.List OrdersList = new UserControls.OrdersManagement.List();
        public User user { get; set; }
        bool _editEnded;
        public Orders(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = OrdersList;
        }

        private void btn_AddOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
