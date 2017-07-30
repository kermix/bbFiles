using bbFiles.Entities;
using bbFiles.Messages;
using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.ViewModel
{
    public class DonatesViewModel : ViewModelBase
    {
        IDonatesDataAccessService _serviceProxy;
        ObservableCollection<Donate> _donates;
        public ObservableCollection<Donate> Donates
        {
            get { return _donates; }
            set { _donates = value; RaisePropertyChanged("Donates"); }
        }
        Donate _selectedDonate;
        public Donate SelectedDonate
        {
            get { return _selectedDonate; }
            set
            {
                _selectedDonate = value;
                RaisePropertyChanged("SelectedDonate");
                RaisePropertyChanged("SelectedDonateAmount");
                RaisePropertyChanged("SelectedDonateAvaliable");
                RaisePropertyChanged("SelectedDonateDate");
                RaisePropertyChanged("SelectedDonateDateDonorPesel");
            }
        }

        #region Notifable SelectedAcceptor properties
        public int SelectedDonateAmount
        {
            get { return SelectedDonate.Amount; }
            set
            {
                SelectedDonate.Amount = value;
                RaisePropertyChanged("SelectedDonateAmount");
            }
        }
        public bool SelectedDonateAvaliable
        {
            get { return SelectedDonate.Avaliable; }
            set
            {
                SelectedDonate.Avaliable = value;
                RaisePropertyChanged("SelectedDonateAvaliable");
            }
        }
        public DateTime SelectedDonateDate
        {
            get { return SelectedDonate.DonateDate; }
            set
            {
                SelectedDonate.DonateDate = value;
                RaisePropertyChanged("SelectedDonateDate");
            }
        }
        public string SelectedDonateDateDonorPesel
        {
            get { return SelectedDonate.Donor_PESEL; }
            set { SelectedDonate.Donor_PESEL = value; RaisePropertyChanged("SelectedDonateDateDonorPesel"); }
        }
        #endregion
        #region Command Declarations
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand<Donate> SendDonateCommand { get; set; }
        public RelayCommand SaveDonateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        #endregion
        #region Visiblility Parameters
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
        bool _donateDetailsMode;
        public bool DonateDetailsMode
        {
            get { return _donateDetailsMode; }
            set
            {
                if (value == false)
                    InEdit = false;
                _donateDetailsMode = value;
                RaisePropertyChanged("DonateDetailsMode");
            }
        }
        #endregion
        #region Constructor
        public DonatesViewModel(IDonatesDataAccessService serviceProxy)
        {
            _serviceProxy = serviceProxy;
            Donates = new ObservableCollection<Donate>();
            SelectedDonate = new Donate();
            RefreshCommand = new RelayCommand(GetDonates);
            SendDonateCommand = new RelayCommand<Donate>(SendDonate);
            SaveDonateCommand = new RelayCommand(SaveDonate);
            CancelCommand = new RelayCommand(Cancel);
            GetDonates();
        }
        #endregion

        void GetDonates()
        {
            Donates.Clear();
            Donates = _serviceProxy.GetDonates();
            DonateDetailsMode = false;
        }
        void SaveDonate()
        {
            if (SelectedDonate.Amount < 0 || SelectedDonate.DonateDate == null || string.IsNullOrWhiteSpace(SelectedDonate.Donor_PESEL))
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.BlankFieldsError });
                return;
            }
            SelectedDonate.Donor = _serviceProxy.FindDonor(SelectedDonateDateDonorPesel);
            if (SelectedDonate.Donor == null)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.NoSuchDonorError });
                return;
            }

            int id = _serviceProxy.CreateDonate(SelectedDonate);
            if (id != 0)
            {
                RaisePropertyChanged("SelectedDonate");
                GetDonates();
                (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            }
        }

        void SendDonate(Donate Donate)
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation(); ;
            DonateDetailsMode = true;
            if (Donate == null)
            {
                SelectedDonate = new Donate()
                {
                    Donor = new Donor()
                };
                SelectedDonateAvaliable = true;
                SelectedDonateDate = DateTime.Now;
                InEdit = false;
            }
            else
            {
                InEdit = true;
                SelectedDonate = Donate;
            }
        }

        void Cancel()
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            GetDonates();
        }
       

    }
}
