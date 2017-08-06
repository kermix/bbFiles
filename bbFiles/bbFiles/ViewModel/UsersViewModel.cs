using bbFiles.Entities;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using bbFiles.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using bbFiles.Messages;
using System;
using Microsoft.Practices.ServiceLocation;

namespace bbFiles.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        IUsersDataAccessService _serviceProxy;
        ObservableCollection<User> _users;
        /// <summary>
        /// Gets or sets the users obesrvable collection. On set raises property changed event.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged("Users");
            }
        }
        User _selectedUser;
        /// <summary>
        /// Gets or sets the selected user. Indicates on accually selected user on DataGrid.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                RaisePropertyChanged("SelectedUser");
                RaisePropertyChanged("SelectedUserLogin");
                RaisePropertyChanged("SelectedUserPassword");
                RaisePropertyChanged("SelectedUserRegisteredDate");
                RaisePropertyChanged("SelectedUserLastLoginDate");
                RaisePropertyChanged("SelectedUserPasswordChaged");
                RaisePropertyChanged("SelectedUserRole");
                RaisePropertyChanged("SelectedUserAcceptorName");
                RaisePropertyChanged("SelectedUserAcceptorAddress_Street");
                RaisePropertyChanged("SelectedUserAcceptorAddress_City");
                RaisePropertyChanged("SelectedUserAcceptorAddress_PostalCode");
                RaisePropertyChanged("SelectedUserAcceptorContact_Phone");
                RaisePropertyChanged("SelectedUserAcceptorContact_Email");
            }
        }

        #region Notifable SelectedUser properties
        /// <exclude />
        public string SelectedUserLogin
        {
            get { return SelectedUser.Login; }
            set
            {
                SelectedUser.Login = value;
                RaisePropertyChanged("SelectedUserLogin");
            }
        }
        /// <exclude />
        public string SelectedUserPassword
        {
            get { return SelectedUser.Password; }
            set
            {
                SelectedUser.Password = value;
                RaisePropertyChanged("SelectedUserPassword");
            }
        }
        /// <exclude />
        public System.DateTime SelectedUserRegisteredDate
        {
            get { return SelectedUser.RegisteredDate; }
            set
            {
                SelectedUser.RegisteredDate = value;
                RaisePropertyChanged("SelectedUserRegisteredDate");
            }
        }
        /// <exclude />
        public Nullable<System.DateTime> SelectedUserLastLoginDate
        {
            get { return SelectedUser.LastLoginDate; }
            set
            {
                SelectedUser.LastLoginDate = value;
                RaisePropertyChanged("SelectedUserLastLoginDate");
            }
        }
        /// <exclude />
        public bool SelectedUserPasswordChaged
        {
            get { return SelectedUser.PasswordChaged; }
            set
            {
                SelectedUser.PasswordChaged = value;
                RaisePropertyChanged("SelectedUserPasswordChaged");
            }
        }
        /// <exclude />
        public bbFiles.Role SelectedUserRole
        {
            get { return SelectedUser.Role; }
            set
            {
                SelectedUser.Role = value;
                RaisePropertyChanged("SelectedUserRole");
            }
        }
        /// <exclude />
        public string SelectedUserAcceptorName
        {
            get { return SelectedUser.Acceptor.Name; }
            set
            {
                SelectedUser.Acceptor.Name = value;
                RaisePropertyChanged("SelectedUserAcceptorName");
            }
        }
        /// <exclude />
        public string SelectedUserAcceptorAddress_Street
        {
            get { return SelectedUser.Acceptor.Address_Street; }
            set
            {
                SelectedUser.Acceptor.Address_Street = value;
                RaisePropertyChanged("SelectedUserAcceptorAddress_Street");
            }
        }
        /// <exclude />
        public string SelectedUserAcceptorAddress_City
        {
            get { return SelectedUser.Acceptor.Address_City; }
            set
            {
                SelectedUser.Acceptor.Address_City = value;
                RaisePropertyChanged("SelectedUserAcceptorAddress_City");
            }
        }
        /// <exclude />
        public string SelectedUserAcceptorAddress_PostalCode
        {
            get { return SelectedUser.Acceptor.Address_PostalCode; }
            set
            {
                SelectedUser.Acceptor.Address_PostalCode = value;
                RaisePropertyChanged("SelectedUserAcceptorAddress_PostalCode");
            }
        }
        /// <exclude />
        public string SelectedUserAcceptorContact_Phone
        {
            get { return SelectedUser.Acceptor.Contact_Phone; }
            set
            {
                SelectedUser.Acceptor.Contact_Phone = value;
                RaisePropertyChanged("SelectedUserAcceptorContact_Phone");
            }
        }
        /// <exclude />
        public string SelectedUserAcceptorContact_Email
        {
            get { return SelectedUser.Acceptor.Contact_Email; }
            set
            {
                SelectedUser.Acceptor.Contact_Email = value;
                RaisePropertyChanged("SelectedUserAcceptorContact_Email");
            }
        }
        #endregion
        #region Visiblility Parameters
        bool _userAcceptorGridVisibility;
        /// <summary>
        /// Gets or sets a value indicating whether [user acceptor grid visibility].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user acceptor grid visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool UserAcceptorGridVisibility
        {
            get { return _userAcceptorGridVisibility; }
            set
            {
                _userAcceptorGridVisibility = value;
                RaisePropertyChanged("UserAcceptorGridVisibility");
            }
        }
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
        bool _userDetailsMode;
        /// <summary>
        /// Gets or sets a value indicating whether [user details mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user details mode]; otherwise, <c>false</c>.
        /// </value>
        public bool UserDetailsMode
        {
            get { return _userDetailsMode; }
            set
            {
                if (value == false)
                    InEdit = false;
                _userDetailsMode = value;
                RaisePropertyChanged("UserDetailsMode");
            }
        }
        #endregion
        #region Command Declarations
        /// <exclude />
        public RelayCommand RefreshCommand { get; set; }
        /// <exclude />
        public RelayCommand<User> SendUserCommand { get; set; }
        /// <exclude />
        public RelayCommand SaveUserCommand { get; set; }
        /// <exclude />
        public RelayCommand CheckRoleCommand { get; set; }
        /// <exclude />
        public RelayCommand<User> DeleteUserCommand { get; set; }
        /// <exclude />
        public RelayCommand CancelCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersViewModel"/> class.
        /// </summary>
        /// <param name="serviceProxy">The data access service.</param>
        public UsersViewModel(IUsersDataAccessService serviceProxy)
        {
            _serviceProxy = serviceProxy;
            Users = new ObservableCollection<User>();
            SelectedUser = new User();
            RefreshCommand = new RelayCommand(GetUsers);
            SendUserCommand = new RelayCommand<User>(SendUser);
            SaveUserCommand = new RelayCommand(SaveUser);
            CheckRoleCommand = new RelayCommand(CheckRole);
            DeleteUserCommand = new RelayCommand<User>(DeleteUser);
            CancelCommand = new RelayCommand(Cancel);
            GetUsers();
        }
        #endregion

        void GetUsers()
        {
            Users.Clear();
            Users = _serviceProxy.GetUsers();
            UserDetailsMode = false;
        }
        void SaveUser()
        {
            if( string.IsNullOrWhiteSpace(SelectedUser.Login) || string.IsNullOrWhiteSpace(SelectedUser.Password)  ||
                SelectedUser.RegisteredDate == null ||
                    (SelectedUser.Acceptor != null && 
                        (
                            string.IsNullOrWhiteSpace(SelectedUser.Acceptor.Name) || string.IsNullOrWhiteSpace(SelectedUser.Acceptor.Address_City) ||
                            string.IsNullOrWhiteSpace(SelectedUser.Acceptor.Address_Street) || 
                            string.IsNullOrWhiteSpace(SelectedUser.Acceptor.Address_PostalCode) || 
                            string.IsNullOrWhiteSpace(SelectedUser.Acceptor.Contact_Email) ||
                            string.IsNullOrWhiteSpace(SelectedUser.Acceptor.Contact_Phone)
                        )))
            {
                Messenger.Default.Send(new ErrorMessage(){ Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.BlankFieldsError });
                return;
            }

            int id = _serviceProxy.CreateUser(SelectedUser);
            if (id != 0)
            {
                RaisePropertyChanged("SelectedUser");
                GetUsers();
                (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            }
        }
        void SendUser(User User)
        {
            if(User != null && User.Id == ServiceLocator.Current.GetInstance<MainViewModel>().userId)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.UserSelectionErrorTitle, Error = Resources.Strings.CannotDeleteOrEditYourselfError });
                return;
            }
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            UserDetailsMode = true;
            if (User == null)
            {
                SelectedUser = new User();
                SelectedUserRegisteredDate = DateTime.Now;
                SelectedUserPasswordChaged = false;
                InEdit = false;
            }
            else
            {
                InEdit = true;
                SelectedUser = User;
            }
            CheckRole();
        }
        void Cancel()
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            GetUsers();
        }
        void CheckRole()
        {
            if (UserDetailsMode == true)
            {
                if (SelectedUser.Role != Role.Acceptor)
                {
                    SelectedUser.Acceptor = null;
                        UserAcceptorGridVisibility = false;
                }
                else
                {
                    UserAcceptorGridVisibility = true;
                    if (SelectedUser.Acceptor == null)
                        SelectedUser.Acceptor = new Acceptor();
                }
            }
        }
        void DeleteUser(User User)
        {
            if (User != null && User.Id == ServiceLocator.Current.GetInstance<MainViewModel>().userId)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.UserSelectionErrorTitle, Error = Resources.Strings.CannotDeleteOrEditYourselfError });
                return;
            }
            if (User != null)
            {
                _serviceProxy.DeleteUser(User);
                GetUsers();
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
