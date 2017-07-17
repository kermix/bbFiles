using System.Windows;
using System.Windows.Controls;
using System;

namespace bbFiles.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Donors.xaml
    /// </summary>
    
    public partial class Donors : UserControl, IUControlManagement
    {
        UserControls.DonorManagement.List DonorsList = new UserControls.DonorManagement.List();
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
                    cc_Content.Content = DonorsList;
                    DonorsList.Refresh();
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
        public Donors(User user)
        {
            this.user = user;
            InitializeComponent();
            cc_Content.Content = DonorsList;
            
        }

        private void btn_AddDonor_Click(object sender, RoutedEventArgs e)
        {
            cc_Content.Content = new DonorManagement.AddEdit(user);
            editEnded = false;
            Window parentWindow = Window.GetWindow(this);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;
        }
        private void btn_EditDonor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bbFiles.Donors row = DonorsList.GetSelected();
                cc_Content.Content = new DonorManagement.AddEdit(user, row.PESEL);
                editEnded = false;
                Window parentWindow = Window.GetWindow(this);
                ((DockerWindow)parentWindow).g_Navigation.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (tb_PeselFilter.Text != "")
                tb_PeselFilter.Text = "";
            else
                DonorsList.Refresh();
        }

        private void tb_PeselFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            long pesel;
            if (tb_PeselFilter.Text.Trim() != "")
            {
                bool pResult = long.TryParse(tb_PeselFilter.Text.Trim(), out pesel);
                if (pResult)
                    DonorsList.Refresh(pesel);
            }
            else
                DonorsList.Refresh();
        }
    }
}
