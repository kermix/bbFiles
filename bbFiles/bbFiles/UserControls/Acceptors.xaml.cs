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
    /// Logika interakcji dla klasy Acceptors.xaml
    /// </summary>
    public partial class Acceptors : UserControl, IUControlManagement
    {
        UserControls.AcceptorManagement.List AcceptorsList = new UserControls.AcceptorManagement.List();
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
                    cc_Content.Content = AcceptorsList;
                    AcceptorsList.Refresh();
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
        public Acceptors(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = AcceptorsList;
        }

        private void btn_AddAcceptor_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new UserManagement.AddEdit(user, true);
            editEnded = false;
            Window parentWindow = Window.GetWindow(this);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;
        }

        private void btn_EditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bbFiles.Acceptors row = AcceptorsList.GetSelected();
                databaseDataContext dc = new databaseDataContext();
                var username = (from c in dc.Credentials
                                where c.UserID == row.UserID
                                select c.Login).SingleOrDefault();
                if (username != null)
                {
                    cc_Content.Content = new UserManagement.AddEdit(user, username);
                }
                else
                    lb_Message.Content = Properties.Strings.DependentUserDoesNotExist;
            }
            catch (Exception ex)
            {
                lb_Message.Content = ex.Message;
            }
        }

        private void btn_DeleteDependentUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bbFiles.Acceptors row = AcceptorsList.GetSelected();

                databaseDataContext dc = new databaseDataContext();

                var user = (from c in dc.Credentials
                            where c.UserID == row.UserID
                            select c).Single();
                var acceptor = (from c in dc.Acceptors
                                where c.UserID == user.UserID
                                select c).SingleOrDefault();

                if(user.Login == this.user.Name)
                    throw new UserEditException(Properties.Strings.CannotRemoveYourself);

                if (acceptor != null)
                    new Structs.UserDataContext(new Structs.User(user.Login, user.Password, user.Role, user.RegisteredDate, user.LastLoggedDate),
                                                new Structs.Acceptor(acceptor.AcceptorName, acceptor.Address, acceptor.Email, acceptor.PhoneNumber)
                                                ).Delete();
                else
                    new Structs.UserDataContext(new Structs.User(user.Login, user.Password, user.Role, user.RegisteredDate, user.LastLoggedDate),
                                                null
                                                ).Delete();

                AcceptorsList.Refresh();
            }
            catch (InvalidOperationException)
            {
                lb_Message.Content = Properties.Strings.DependentUserDoesNotExist;
            }
            catch (Exception ex)
            {
                lb_Message.Content = ex.Message;
            }

        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            AcceptorsList.Refresh();
        }
    }
}
