﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bbFiles.Services;
using bbFiles.Entities;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using bbFiles.Messages;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace bbFiles.ViewModel
{
    public class AcceptorsViewModel : ViewModelBase
    {
        IAcceptorsDataService _serviceProxy;
        ObservableCollection<Acceptor> _acceptors;
        /// <summary>
        /// Gets or sets the acceptors obesrvable collection. On set raises property changed event.
        /// </summary>
        /// <value>
        /// The acceptors.
        /// </value>
        public ObservableCollection<Acceptor> Acceptors
        {
            get { return _acceptors; }
            set
            {
                _acceptors = value;
                RaisePropertyChanged("Acceptors");
            }
        }
        Acceptor _selectedAcceptor;
        /// <summary>
        /// Gets or sets the selected acceptor. Indicates on accually selected acceptor on DataGrid.
        /// </summary>
        /// <value>
        /// The selected acceptor.
        /// </value>
        public Acceptor SelectedAcceptor
        {
            get { return _selectedAcceptor; }
            set
            {
                _selectedAcceptor = value;
                RaisePropertyChanged("SelectedAcceptor");;
                RaisePropertyChanged("SelectedAcceptorName");
                RaisePropertyChanged("SelectedAcceptorAddress_Street");
                RaisePropertyChanged("SelectedAcceptorAddress_City");
                RaisePropertyChanged("SelectedAcceptorAddress_PostalCode");
                RaisePropertyChanged("SelectedAcceptorContact_Phone");
                RaisePropertyChanged("SelectedAcceptorContact_Email");
                RaisePropertyChanged("SelectedAcceptorOrders");
                RaisePropertyChanged("SelectedAcceptorUserLogin");
                RaisePropertyChanged("SelectedAcceptorUserPassword");
                RaisePropertyChanged("SelectedAcceptorUserRegisteredDate");
                RaisePropertyChanged("SelectedAcceptorUserLastLoginDate");
                RaisePropertyChanged("SelectedAcceptorUserPasswordChaged");
                RaisePropertyChanged("SelectedAcceptorUserRole");
            }
        }

        #region Notifable SelectedAcceptor properties
        /// <exclude />
        public string SelectedAcceptorName
        {
            get { return SelectedAcceptor.Name; }
            set
            {
                SelectedAcceptor.Name = value;
                RaisePropertyChanged("SelectedAcceptorName");
            }
        }
        /// <exclude />
        public string SelectedAcceptorAddress_Street
        {
            get { return SelectedAcceptor.Address_Street; }
            set
            {
                SelectedAcceptor.Address_Street = value;
                RaisePropertyChanged("SelectedAcceptorAddress_Street");
            }
        }
        /// <exclude />
        public string SelectedAcceptorAddress_City
        {
            get { return SelectedAcceptor.Address_City; }
            set
            {
                SelectedAcceptor.Address_City = value;
                RaisePropertyChanged("SelectedAcceptorAddress_City");
            }
        }
        /// <exclude />
        public string SelectedAcceptorAddress_PostalCode
        {
            get { return SelectedAcceptor.Address_PostalCode; }
            set
            {
                SelectedAcceptor.Address_PostalCode = value;
                RaisePropertyChanged("SelectedAcceptorAddress_PostalCode");
            }
        }
        /// <exclude />
        public string SelectedAcceptorContact_Phone
        {
            get { return SelectedAcceptor.Contact_Phone; }
            set
            {
                SelectedAcceptor.Contact_Phone = value;
                RaisePropertyChanged("SelectedAcceptorContact_Phone");
            }
        }
        /// <exclude />
        public string SelectedAcceptorContact_Email
        {
            get { return SelectedAcceptor.Contact_Email; }
            set
            {
                SelectedAcceptor.Contact_Email = value;
                RaisePropertyChanged("SelectedAcceptorContact_Email");
            }
        }
        /// <exclude />
        public ObservableCollection<Order> SelectedAcceptorOrders
        {
            get { return SelectedAcceptor.Orders; }
            set { SelectedAcceptor.Orders = value; RaisePropertyChanged("SelectedAcceptorOrders"); }
        }
        /// <exclude />
        public string SelectedAcceptorUserLogin
        {
            get { return SelectedAcceptor.User.Login; }
            set
            {
                SelectedAcceptor.User.Login = value;
                RaisePropertyChanged("SelectedAcceptorUserLogin");
            }
        }
        /// <exclude />
        public string SelectedAcceptorUserPassword
        {
            get { return SelectedAcceptor.User.Password; }
            set
            {
                SelectedAcceptor.User.Password = value;
                RaisePropertyChanged("SelectedAcceptorUserPassword");
            }
        }
        /// <exclude />
        public DateTime SelectedAcceptorUserRegisteredDate
        {
            get { return SelectedAcceptor.User.RegisteredDate; }
            set
            {
                SelectedAcceptor.User.RegisteredDate = value;
                RaisePropertyChanged("SelectedAcceptorUserRegisteredDate");
            }
        }
        /// <exclude />
        public DateTime? SelectedAcceptorUserLastLoginDate
        {
            get { return SelectedAcceptor.User.LastLoginDate; }
            set
            {
                SelectedAcceptor.User.LastLoginDate = value;
                RaisePropertyChanged("SelectedAcceptorUserLastLoginDate");
            }
        }
        /// <exclude />
        public bool SelectedAcceptorUserPasswordChaged
        {
            get { return SelectedAcceptor.User.PasswordChaged; }
            set
            {
                SelectedAcceptor.User.PasswordChaged = value;
                RaisePropertyChanged("SelectedAcceptorUserPasswordChaged");
            }
        }
        /// <exclude />
        public Role SelectedAcceptorUserRole
        {
            get { return SelectedAcceptor.User.Role; }
            set
            {
                SelectedAcceptor.User.Role = value;
                RaisePropertyChanged("SelectedAcceptorUserRole");
            }
        }
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
        bool _acceptorDetailsMode;
        /// <summary>
        /// Gets or sets a value indicating whether [acceptor details mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [acceptor details mode]; otherwise, <c>false</c>.
        /// </value>
        public bool AcceptorDetailsMode
        {
            get { return _acceptorDetailsMode; }
            set
            {
                if (value == false)
                    InEdit = false;
                _acceptorDetailsMode = value;
                RaisePropertyChanged("AcceptorDetailsMode");
            }
        }
        #endregion
        #region Command Declarations
        /// <exclude />
        public RelayCommand RefreshCommand { get; set; }
        /// <exclude />
        public RelayCommand<Acceptor> SendAcceptorCommand { get; set; }
        /// <exclude />
        public RelayCommand SaveAcceptorCommand { get; set; }
        /// <exclude />
        public RelayCommand<Acceptor> DeleteAcceptorUserCommand { get; set; }
        /// <exclude />
        public RelayCommand SearchCommand { get; set; }
        /// <exclude />
        public RelayCommand CancelCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptorsViewModel"/> class.
        /// </summary>
        /// <param name="serviceProxy">The data access service.</param>
        public AcceptorsViewModel(IAcceptorsDataService serviceProxy)
        {
            _serviceProxy = serviceProxy;
            Acceptors = new ObservableCollection<Acceptor>();
            SelectedAcceptor = new Acceptor();
            RefreshCommand = new RelayCommand(GetAcceptors);
            SendAcceptorCommand = new RelayCommand<Acceptor>(SendAcceptor);
            SaveAcceptorCommand = new RelayCommand(SaveAcceptor);
            DeleteAcceptorUserCommand = new RelayCommand<Acceptor>(DeleteAcceptorUser);
            CancelCommand = new RelayCommand(Cancel);
            GetAcceptors();
        }
        #endregion

        void GetAcceptors()
        {
            Acceptors.Clear();
            Acceptors = _serviceProxy.GetAcceptors();
            AcceptorDetailsMode = false;
        }
        void SaveAcceptor()
        {

            if ((string.IsNullOrWhiteSpace(SelectedAcceptor.Name) || string.IsNullOrWhiteSpace(SelectedAcceptor.Address_City) ||
                            string.IsNullOrWhiteSpace(SelectedAcceptor.Address_Street) || string.IsNullOrWhiteSpace(SelectedAcceptor.Address_PostalCode) ||
                            string.IsNullOrWhiteSpace(SelectedAcceptor.Contact_Email) || string.IsNullOrWhiteSpace(SelectedAcceptor.Contact_Phone)) ||
                (SelectedAcceptor.User != null && (string.IsNullOrWhiteSpace(SelectedAcceptor.User.Login) || string.IsNullOrWhiteSpace(SelectedAcceptor.User.Password) ||
                SelectedAcceptor.User.RegisteredDate == null)))
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.BlankFieldsError });
                return;
            }

            int id = _serviceProxy.CreateAcceptor(SelectedAcceptor);
            if (id != 0)
            {
                RaisePropertyChanged("SelectedAcceptor");
                GetAcceptors();
                (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            }
            else
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.DuplicatedUserNameError });
        }
        void SendAcceptor(Acceptor Acceptor)
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            AcceptorDetailsMode = true;
            if (Acceptor == null)
            {
                SelectedAcceptor = new Acceptor()
                {
                    User = new User()
                };
                SelectedAcceptorUserRegisteredDate = DateTime.Now;
                SelectedAcceptorUserPasswordChaged = false;
                SelectedAcceptorUserRole = Role.Acceptor;
                InEdit = false;
            }
            else
            {
                InEdit = true;
                SelectedAcceptor = Acceptor;
            }
        }
        void Cancel()
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            GetAcceptors();
        }
        void DeleteAcceptorUser(Acceptor Acceptor)
        {
            if (Acceptor != null)
            {
                bool deletionResult = _serviceProxy.DeleteDependentUser(Acceptor);
                if (deletionResult)
                    GetAcceptors();
                else
                    Messenger.Default.Send(new ErrorMessage() { Error = Resources.Strings.UserSelectionErrorTitle, Title = Resources.Strings.NoDependentUserError });
            }
            else
            {
                Messenger.Default.Send(new ErrorMessage()
                {
                    Title = Resources.Strings.UserSelectionErrorTitle,
                    Error = Resources.Strings.SelectUserFirstError
                });
            }
        }
    }
}
