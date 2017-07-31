using bbFiles.Entities;
using bbFiles.Messages;
using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;

namespace bbFiles.ViewModel
{
    public class DonorsViewModel : ViewModelBase
    {
        IDonorsAccessDataService _serviceProxy;
        ObservableCollection<Donor> _donors;
        public ObservableCollection<Donor> Donors {
            get
            {
                return _donors;
            }
            set
            {
                _donors = value;
                RaisePropertyChanged("Donors");
            }
        }
        Donor _selectedDonor;
        public Donor SelectedDonor
        {
            get
            {
                return _selectedDonor;
            }
            set
            {
                _selectedDonor = value;
                RaisePropertyChanged("SelectedDonor");
                RaisePropertyChanged("SelectedDonorPESEL");
                RaisePropertyChanged("SelectedDonorFirstname");
                RaisePropertyChanged("SelectedDonorSurname");
                RaisePropertyChanged("SelectedDonorAddressStreet");
                RaisePropertyChanged("SelectedDonorAddressCity");
                RaisePropertyChanged("SelectedDonorAddressPostalCode");
                RaisePropertyChanged("SelectedDonorContactPhone");
                RaisePropertyChanged("SelectedDonorContactEmail");
                RaisePropertyChanged("SelectedDonorBloodType");
                RaisePropertyChanged("SelectedDonorRhMarker");
                RaisePropertyChanged("SelectedUserDonates");
            }
        }

        #region Notifable SelectedDonor properties
        public string SelectedDonorPESEL {
            get { return SelectedDonor.PESEL; }
            set { SelectedDonor.PESEL = value; RaisePropertyChanged("SelectedDonorPESEL"); }
        }
        public string SelectedDonorFirstname
        {
            get { return SelectedDonor.Firstname; }
            set { SelectedDonor.Firstname = value; RaisePropertyChanged("SelectedDonorFirstname"); }
        }
        public string SelectedDonorSurname
        {
            get { return SelectedDonor.Surname; }
            set { SelectedDonor.Surname = value; RaisePropertyChanged("SelectedDonorSurname"); }
        }
        public string SelectedDonorAddressStreet
        {
            get { return SelectedDonor.Address_Street; }
            set { SelectedDonor.Address_Street = value; RaisePropertyChanged("SelectedDonorAddressStreet"); }
        }
        public string SelectedDonorAddressCity
        {
            get { return SelectedDonor.Address_City; }
            set { SelectedDonor.Address_City = value; RaisePropertyChanged("SelectedDonorAddressCity"); }
        }
        public string SelectedDonorAddressPostalCode
        {
            get { return SelectedDonor.Address_PostalCode; }
            set { SelectedDonor.Address_PostalCode = value; RaisePropertyChanged("SelectedDonorAddressPostalCode"); }
        }
        public string SelectedDonorContactPhone
        {
            get { return SelectedDonor.Contact_Phone; }
            set { SelectedDonor.Contact_Phone = value; RaisePropertyChanged("SelectedDonorContactPhone"); }
        }
        public string SelectedDonorContactEmail
        {
            get { return SelectedDonor.Contact_Email; }
            set { SelectedDonor.Contact_Email = value; RaisePropertyChanged("SelectedDonorContactEmail"); }
        }
        public BloodType SelectedDonorBloodType
        {
            get { return SelectedDonor.Blood_Type; }
            set { SelectedDonor.Blood_Type = value; RaisePropertyChanged("SelectedDonorBloodType"); }
        }
        public bool SelectedDonorRhMarker
        {
            get { return SelectedDonor.Blood_RhMarker; }
            set { SelectedDonor.Blood_RhMarker = value; RaisePropertyChanged("SelectedDonorRhMarker"); }
        }
        public ObservableCollection<Donate> SelectedUserDonates
        {
            get { return SelectedDonor.Donates; }
            set { SelectedDonor.Donates = value; RaisePropertyChanged("SelectedUserDonates"); }
        }
        #endregion
        #region Visibility parameters
        bool _inEdit;
        public bool InEdit
        {
            get { return !_inEdit; }
            set
            {
                _inEdit = value;
                RaisePropertyChanged("InEdit");
            }
        }
        bool _donorDetailsMode;
        public bool DonorDetailsMode
        {
            get { return _donorDetailsMode; }
            set
            {
                if (value == false)
                    InEdit = false;
                _donorDetailsMode = value;
                RaisePropertyChanged("DonorDetailsMode");
            }
        }
        #endregion
        #region Command Declarations
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand<Donor> SendDonorCommand { get; set; }
        public RelayCommand SaveDonorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        #endregion

        public DonorsViewModel(IDonorsAccessDataService serviceProxy)
        {
            _serviceProxy = serviceProxy;
            Donors = new ObservableCollection<Donor>();
            SelectedDonor = new Donor();
            RefreshCommand = new RelayCommand(GetDonors);
            SendDonorCommand = new RelayCommand<Donor>(SendDonor);
            SaveDonorCommand = new RelayCommand(SaveDonor);
            CancelCommand = new RelayCommand(Cancel);
            GetDonors();
        }

        void GetDonors()
        {
            Donors.Clear();
            Donors = _serviceProxy.GetDonors();
            DonorDetailsMode = false;
        }

        void SaveDonor()
        {
            if (string.IsNullOrWhiteSpace(SelectedDonor.PESEL) || string.IsNullOrWhiteSpace(SelectedDonor.Firstname) ||
                string.IsNullOrWhiteSpace(SelectedDonor.Surname) || string.IsNullOrWhiteSpace(SelectedDonor.Address_Street) ||
                string.IsNullOrWhiteSpace(SelectedDonor.Address_City) || string.IsNullOrWhiteSpace(SelectedDonor.Address_PostalCode) ||
                string.IsNullOrWhiteSpace(SelectedDonor.Contact_Email) || string.IsNullOrWhiteSpace(SelectedDonor.Contact_Phone))
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.BlankFieldsError });
                return;
            }

            string pesel = _serviceProxy.CreateDonor(SelectedDonor);
            if (pesel != "")
            {
                RaisePropertyChanged("SelectedDonor");
                GetDonors();
                (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            }
            else
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.Error, Error = Resources.Strings.DonorAlreadyExistsError });
            }
        }

        void SendDonor(Donor Donor)
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            DonorDetailsMode = true;
            if (Donor == null)
            {
                SelectedDonor = new Donor();
                InEdit = false;
            }
            else
            {
                InEdit = true;
                SelectedDonor = Donor;
            }
        }

        void Cancel()
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            GetDonors();
        }
    }
}
