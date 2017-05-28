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


namespace bbFiles.UserControls.UserManagement
{
    /// <summary>
    /// Logika interakcji dla klasy AddEdit.xaml
    /// </summary>
    public partial class AddEdit : UserControl
    {
        User user;
        Structs.UserDataContext sUser;
        bool isEditing = false;

        public AddEdit(User user)
        {
            Privileges.CheckAdmin(user);
            InitializeComponent();
            this.user = user;
            sUser = new Structs.UserDataContext(new Structs.User(), new Structs.Acceptor());
            this.DataContext = sUser;
            cb_HasToChangePassword.IsEnabled = false;
            tb_Username.IsReadOnly = false;
        }

        public AddEdit(User user, bool acceptor) : this(user)
        {
            if (acceptor == true)
            {
                cb_Role.SelectedIndex = 0;
                cb_Role.IsEnabled = false;
            }
        }
        public AddEdit(User user, string name) : this(user)
        {
            databaseDataContext dc = new databaseDataContext();
            isEditing = true;
            var q = (from c in dc.Credentials
                     where c.Login == name
                     select c).Single();
            var q2 = (from c in dc.Acceptors
                      where c.UserID == q.UserID
                      select c).SingleOrDefault();
            sUser = q2 == null ?
                new Structs.UserDataContext(new Structs.User(q.Login, q.Password, q.Role,
                                                q.RegisteredDate, q.LastLoggedDate, !q.PasswordChanged),
                                            new Structs.Acceptor()) :
                new Structs.UserDataContext(new Structs.User(q.Login, q.Password, q.Role,
                                                q.RegisteredDate, q.LastLoggedDate, !q.PasswordChanged),
                                            new Structs.Acceptor(q2.AcceptorName,
                                                q2.Address, q2.Address, q2.PhoneNumber));
            this.DataContext = sUser;
            cb_HasToChangePassword.IsEnabled = true;
            tb_Username.IsReadOnly = true;
            cb_Role.IsEnabled = false;
            if (sUser.user.role != Roles.ACCEPTOR)
            {
                g_AcceptorData.IsEnabled = true;
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!sUser.user.IsPasswordLengthProper())
                    throw new ArgumentOutOfRangeException(Properties.Strings.PasswordTooShort);

                if (isEditing)
                    sUser.Edit();
                else
                    sUser.Add();
                Utilities.UcSendEndOfEdition(this);
            }
            catch (Exception ex)
            {
                lb_Message.Content = ex.Message;
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Utilities.UcSendEndOfEdition(this);
        }

        private void cb_Role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            g_AcceptorData.IsEnabled = ((ComboBox)sender).SelectedIndex == 0 ? true : false;
        }


    }
}
