﻿using System;
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

namespace bbFiles.UserControls.DonorManagement
{
    /// <summary>
    /// Logika interakcji dla klasy AddEdit.xaml
    /// </summary>
    public partial class AddEdit : UserControl
    {
        User user;
        Structs.Donor donor;
        bool isEditing = false;

        public AddEdit(User User)
        {
            this.user = User;
            Privileges.CheckAdmin(user);
            InitializeComponent();
            donor = new Structs.Donor();
            this.DataContext = donor;
            tb_PESEL.IsReadOnly = false;
        }
        public AddEdit(User user, long pesel) : this(user)
        {
            tb_PESEL.IsReadOnly = true;
            databaseDataContext dc = new databaseDataContext();
            isEditing = true;
            var q = (from c in dc.Donors
                     where c.PESEL == pesel
                     select c).Single();
            donor = new Structs.Donor(q.Firstname, q.Surname, q.Birthdate, q.BloodType, q.RhMarker, q.PESEL, q.Email, q.PhoneNumber);
            this.DataContext = donor;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isEditing)
                    donor.Edit();
                else
                    donor.Add();
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
    }
}
