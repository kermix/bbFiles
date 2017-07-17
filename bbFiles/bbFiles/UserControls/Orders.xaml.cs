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
    public partial class Orders : UserControl, IUControlManagement
    {
        UserControls.OrdersManagement.List OrdersList = new UserControls.OrdersManagement.List();
        public User user { get; set; }
        bool _editEnded;
        public bool editEnded
        {
            set
            {
                _editEnded = value;
                if (value == true)
                {
                    g_Manage.IsEnabled = true;
                    cc_Content.Content = OrdersList;
                    OrdersList.Refresh();
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
        public Orders(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = OrdersList;
        }

        private void btn_AddOrder_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new OrderManagement.Add(user);
            editEnded = false;
            Window parentWindow = Window.GetWindow(this);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            OrdersList.Refresh();

        }

        private void btn_MarkSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bbFiles.Orders row = OrdersList.GetSelected();
                var dc = new databaseDataContext();
                var q = (from c in dc.Orders
                        where c.OrderID == row.OrderID
                        select c).Single();
                q.Send = true;
                dc.SubmitChanges();
                
                this.OrdersList.Refresh();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
