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
using System.Security.Principal;

namespace bbFiles
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        User user = null;
        public ChangePasswordWindow(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (tb_newPassword.Password.Length >= 8)
            {
                if (tb_newPassword.Password == tb_repeatNewPassword.Password)
                {
                    user.ChangePassword(tb_newPassword.Password);
                    lb_Message.Content = Properties.Strings.PasswordChanged;
                    MainWindow lw = new MainWindow();
                    lw.Show();
                    this.Close();
                }
                else
                {
                    lb_Message.Content = Properties.Strings.PasswordsDoNotMatch;
                }
            }
            else
            {
                lb_Message.Content = Properties.Strings.PasswordTooShort;
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangePassword_Click(this, null);
                e.Handled = true;
            }
        }
    }
}
