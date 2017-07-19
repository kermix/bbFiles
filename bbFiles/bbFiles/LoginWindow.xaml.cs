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
using MahApps.Metro.Controls;

namespace bbFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = SignIn.Verify(tb_User.Text, tb_Password.Password);
                if (user != null)
                {
                    if (!user.HasPasswordChanged())
                    {
                        (new ChangePasswordWindow(user)).Show();
                    }
                    else
                    {
                        (new DockerWindow(user)).Show();
                    }
                    this.Close();
                }
                else
                    lb_Message.Content = Properties.Strings.CantLoginMessage;
            }
            catch (Exception ex)
            {
                //lb_Message.Content = ex.Message;
                MessageBox.Show(ex.ToString());
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
                LoginButton_Click(this, null);
                e.Handled = true;
            }
        }
    }
}

