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

namespace bbFiles.UserControls.OrderManagement
{
    /// <summary>
    /// Logika interakcji dla klasy Add.xaml
    /// </summary>
    /// 
    public partial class Add : UserControl
    {
        User user;
        Structs.Order order = new Structs.Order();
        public Add(User user)
        {
            this.user = user;
            InitializeComponent();
            this.DataContext = order;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Utilities.UcSendEndOfEdition(this);
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.Add();
                Utilities.UcSendEndOfEdition(this);
            }
            catch (Exception ex)
            {
                lb_Message.Content = ex.Message;
            }

        }
    }
}
