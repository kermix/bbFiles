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
    /// Logika interakcji dla klasy Donates.xaml
    /// </summary>
    public partial class Donates : UserControl, IUControlManagement
    {
        UserControls.DonatesManagement.List DonatesList = new UserControls.DonatesManagement.List();
        bool _editEnded;
        public User user { get; set; }
        public bool editEnded
        {
            set
            {
                _editEnded = value;
                if (value == true)
                {
                    g_Manage.IsEnabled = true;
                    cc_Content.Content = DonatesList;
                    DonatesList.Refresh();
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
        public Donates(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = DonatesList;
        }

        private void btn_AddDonate_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new DonatesManagement.Add(user);
            editEnded = false;
            Window parentWindow = Window.GetWindow(this);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Filter.Text != "")
                tb_Filter.Text = "";
            else
                DonatesList.Refresh();
        }

        private void tb_Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            long filter;
            if (tb_Filter.Text.Trim() != "")
            {
                bool pResult = long.TryParse(tb_Filter.Text.Trim(), out filter);
                if (pResult)
                    if (cb_Filter.SelectedIndex == 0)
                        DonatesList.Refresh(filter, false); // PESEL
                    else if (cb_Filter.SelectedIndex == 1)
                        DonatesList.Refresh(filter); //ID
            }
            else
                DonatesList.Refresh();
        }
    }
}
