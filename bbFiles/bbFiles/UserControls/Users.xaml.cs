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
using System.ComponentModel;

namespace bbFiles.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Users.xaml
    /// </summary>
    public partial class Users : UserControl, IUControlManagement
    {
        UserControls.UserManagement.List UserList = new UserControls.UserManagement.List();
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
                    cc_Content.Content = UserList;
                    UserList.Refresh();
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

        public Users(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = UserList;
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            UserList.Refresh();
        }

        private void btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserManagement.AddEdit(user);
            editEnded = false;
            Window parentWindow = Window.GetWindow(this);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;

        }

        private void btn_EditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Credentials row = UserList.GetSelected();
                cc_Content.Content = new UserManagement.AddEdit(user, row.Login);
                editEnded = false;
                Window parentWindow = Window.GetWindow(this);
                ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Credentials row = UserList.GetSelected();
                databaseDataContext dc = new databaseDataContext();

                var user = (from c in dc.Credentials
                            where c.UserID == row.UserID
                            select c).Single();
                var acceptor = (from c in dc.Acceptors
                                where c.UserID == user.UserID
                                select c).SingleOrDefault();

                if (user.Login == this.user.Name)
                    throw new UserEditException(Properties.Strings.CannotRemoveYourself);

                if (acceptor != null)
                    new Structs.UserDataContext(new Structs.User(user.Login, user.Password, user.Role, user.RegisteredDate, user.LastLoggedDate),
                                                new Structs.Acceptor(acceptor.AcceptorName, acceptor.Address, acceptor.Email, acceptor.PhoneNumber)
                                                ).Delete();
                else
                    new Structs.UserDataContext(new Structs.User(user.Login, user.Password, user.Role, user.RegisteredDate, user.LastLoggedDate),
                                                null
                                                ).Delete();
                UserList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
