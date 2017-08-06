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
        /// <summary>
        /// Gets or sets the donates obesrvable collection. On set raises property changed event.
        /// </summary>
        /// <value>
        /// The donates.
        /// </value>
        public ObservableCollection<Donate> Donates
        {
            get { return _donates; }
            set { _donates = value; RaisePropertyChanged("Donates"); }
        }
        Donate _selectedDonate;
        /// <summary>
        /// Gets or sets the selected donate. Indicates on accually selected donate on DataGrid.
        /// </summary>
        /// <value>
        /// The selected donate.
        /// </value>
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
        /// <exclude />
        public int SelectedDonateAmount
        {
            get { return SelectedDonate.Amount; }
            set
            {
                SelectedDonate.Amount = value;
                RaisePropertyChanged("SelectedDonateAmount");
            }
        }
        /// <exclude />
        public bool SelectedDonateAvaliable
        {
            get { return SelectedDonate.Avaliable; }
            set
            {
                SelectedDonate.Avaliable = value;
                RaisePropertyChanged("SelectedDonateAvaliable");
            }
        }
        /// <exclude />
        public DateTime SelectedDonateDate
        {
            get { return SelectedDonate.DonateDate; }
            set
            {
                SelectedDonate.DonateDate = value;
                RaisePropertyChanged("SelectedDonateDate");
            }
        }
        /// <exclude />
        public string SelectedDonateDateDonorPesel
        {
            get { return SelectedDonate.Donor_PESEL; }
            set { SelectedDonate.Donor_PESEL = value; RaisePropertyChanged("SelectedDonateDateDonorPesel"); }
        }
        #endregion
        #region Command Declarations
        /// <exclude />
        public RelayCommand RefreshCommand { get; set; }
        /// <exclude />
        public RelayCommand<Donate> SendDonateCommand { get; set; }
        /// <exclude />
        public RelayCommand SaveDonateCommand { get; set; }
        /// <exclude />
        public RelayCommand CancelCommand { get; set; }
        #endregion
        #region Visiblility Parameters
        bool _inEdit;
        /// <summary>
        /// Gets or sets a value indicating whether [in edit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in edit]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether [donate details mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [donate details mode]; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="DonatesViewModel"/> class.
        /// </summary>
        /// <param name="serviceProxy">The data access service.</param>
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
