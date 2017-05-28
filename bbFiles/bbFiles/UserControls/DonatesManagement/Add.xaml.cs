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

namespace bbFiles.UserControls.DonatesManagement
{
    /// <summary>
    /// Logika interakcji dla klasy Add.xaml
    /// </summary>
    public partial class Add : UserControl
    {
        User user;
        Structs.Donate donate = new Structs.Donate();
        public Add(User user)
        {
            this.user = user;
            Privileges.CheckAdminOrEmployee(user);
            InitializeComponent();
            this.DataContext = donate;

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Utilities.UcSendEndOfEdition(this);
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                donate.Add();
                Utilities.UcSendEndOfEdition(this);
            }
            catch (Exception ex)
            {
                lb_Message.Content = ex.Message;
            }
        }
    }
}
