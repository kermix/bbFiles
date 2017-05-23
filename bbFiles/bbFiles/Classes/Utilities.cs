using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bbFiles
{
    public class Utilities
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="type"></param>
        public static void refreshGrid(System.Windows.Controls.DataGrid grid, Type type)
        {
            if (type == typeof(Credentials))
                grid.ItemsSource = GetAllCredentials();
            else if (type == typeof(Acceptors))
                grid.ItemsSource = GetAllAcceptors();
            else if (type == typeof(Donors))
                grid.ItemsSource = GetAllDonors();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Roles, string> GetAllRoles()
        {
            Dictionary<Roles, string> RolesDictionary = new Dictionary<Roles, string>();
            RolesDictionary.Add(Roles.ACCEPTOR, Properties.Strings.Acceptor);
            RolesDictionary.Add(Roles.EMPLOYEE, Properties.Strings.Employee);
            RolesDictionary.Add(Roles.ADMIN, Properties.Strings.Admin);
            return RolesDictionary;
        }
        public static Dictionary<BloodTypes, string> GetAllBloodTypes()
        {
            Dictionary<BloodTypes, string> BloodTypesDictionary = new Dictionary<BloodTypes, string>();
            BloodTypesDictionary.Add(BloodTypes.O, "0");
            BloodTypesDictionary.Add(BloodTypes.A, "A");
            BloodTypesDictionary.Add(BloodTypes.B, "B");
            BloodTypesDictionary.Add(BloodTypes.AB, "AB");
            return BloodTypesDictionary;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Credentials> GetAllCredentials()
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Credentials select c;
            return new ObservableCollection<Credentials>(r);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Acceptors> GetAllAcceptors()
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Acceptors select c;
            return new ObservableCollection<Acceptors>(r);
        }
        public static ObservableCollection<Donors> GetAllDonors()
        {
            databaseDataContext dc = new databaseDataContext();
            var r = from c in dc.Donors select c;
            return new ObservableCollection<Donors>(r);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uc"></param>
        public static void ucSendEndOfEdition(UserControl uc)
        {
            DependencyObject ucParent = uc.Parent;
            while (!(ucParent is UserControl))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }
            ((IUControlManagement)ucParent).editEnded = true;
            Window parentWindow = Window.GetWindow(ucParent);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = true;
        }
    }
}
