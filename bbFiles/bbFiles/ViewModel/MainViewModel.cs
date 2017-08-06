using bbFiles.Messages;
using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace bbFiles.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        IMainDataAccessService _serviceProxy;
        private object _selectedViewModel;
        /// <summary>
        /// Gets or sets the selected view model.
        /// </summary>
        /// <value>
        /// The selected view model.
        /// </value>
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; RaisePropertyChanged("SelectedViewModel"); }
        }
        /// <summary>
        /// Gets the logged user ID.
        /// </summary>
        /// <value>
        /// The user ID.
        /// </value>
        public int userId { get; private set; }
        Role _userLevel = Role.Wrong;
        /// <summary>
        /// Gets the user level.
        /// </summary>
        /// <value>
        /// The user level.
        /// </value>
        public Role UserLevel
        {
            get { return _userLevel; }
            private set
            {
                _userLevel = value;
                if (value >= Role.Admin)
                    IsAdmin = true;
                if (value >= Role.Worker)
                    IsWorker = true;
                if (value >= Role.Acceptor)
                    IsAcceptor = true;

                RaisePropertyChanged("UserLevel");
            }
        }
        bool _isLogged = false;
        /// <summary>
        /// Gets a value indicating whether user is logged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if user is logged; otherwise, <c>false</c>.
        /// </value>
        public bool IsLogged
        {
            get { return _isLogged; }
            private set
            {
                _isLogged = value;
                RaisePropertyChanged("IsLogged");
            }
        }
        bool _isWorker;
        /// <summary>
        /// Gets a value indicating whether logged user is worker.
        /// </summary>
        /// <value>
        ///   <c>true</c> if logged user is worker; otherwise, <c>false</c>.
        /// </value>
        public bool IsWorker
        {
            get { return _isWorker; }
            private set
            {
                _isWorker = value;
                RaisePropertyChanged("IsWorker");
            }
        }
        bool _isAcceptor;
        /// <summary>
        /// Gets a value indicating whether logged user is acceptor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if logged user is acceptor; otherwise, <c>false</c>.
        /// </value>
        public bool IsAcceptor
        {
            get { return _isAcceptor; }
            private set
            {
                _isAcceptor = value;
                RaisePropertyChanged("IsAcceptor");
            }
        }
        bool _isAdmin;
        /// <summary>
        /// Gets a value indicating whether logged user is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if logged user is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin
        {
            get { return _isAdmin; }
            private set
            {
                _isAdmin = value;
                RaisePropertyChanged("IsAdmin");
            }
        }
        bool _canNavigate;
        /// <summary>
        /// Gets a value indicating whether this instance can navigate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can navigate; otherwise, <c>false</c>.
        /// </value>
        public bool CanNavigate
        {
            get { return _canNavigate; }
            private set { _canNavigate = value; RaisePropertyChanged("CanNavigate"); }
        }

        /// <exclude />
        public RelayCommand OpenUserViewCommand { get; set; }
        /// <exclude />
        public RelayCommand OpenAcceptorViewCommand { get; set; }
        /// <exclude />
        public RelayCommand OpenDonorViewCommand { get; set; }
        /// <exclude />
        public RelayCommand OpenDonateViewCommand { get; set; }
        /// <exclude />
        public RelayCommand OpenOrderViewCommand { get; set; }
        /// <exclude />
        public RelayCommand OpenStatisticViewCommand { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="serviceProxy">The data access service.</param>
        public MainViewModel(IMainDataAccessService serviceProxy)
        {
            _serviceProxy = serviceProxy;
            OpenUserViewCommand = new RelayCommand(OpenUserView);
            OpenAcceptorViewCommand = new RelayCommand(OpenAcceptorView);
            OpenDonorViewCommand = new RelayCommand(OpenDonorView);
            OpenDonateViewCommand = new RelayCommand(OpenDonateView);
            OpenOrderViewCommand = new RelayCommand(OpenOrderView);
            OpenStatisticViewCommand = new RelayCommand(OpenStatisticView);


            IsWorker = IsAdmin = IsAcceptor = false;
            CanNavigate = true;
        }

        void OpenUserView()
        {
            SelectedViewModel = new UsersViewModel(new UsersDataAccessService());
        }
        void OpenAcceptorView()
        {
            SelectedViewModel = new AcceptorsViewModel(new AcceptorDataAccessService());
        }
        void OpenDonorView()
        {
            SelectedViewModel = new DonorsViewModel(new DonorsDataAccessService());
        }
        void OpenDonateView()
        {
            SelectedViewModel = new DonatesViewModel(new DonatesDataAccessService());
        }
        void OpenOrderView()
        {
            SelectedViewModel = new OrdersViewModel(new OrdersDataAccessService());
        }
        void OpenStatisticView()
        {
            SelectedViewModel = new StatisticsViewModel(new StatisticsDataAccessService());
        }

        /// <summary>
        /// Toogles the navigation.
        /// </summary>
        public void ToogleNavigation()
        {
            CanNavigate = !CanNavigate;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void Login(string username, string password)
        {
            var User = new Entities.User();

            try
            { 
                User = _serviceProxy.LogIn(username, password);
            }
            catch (SqlException)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.Error, Error = Resources.Strings.NoConnectionError });
                User = new Entities.User();
            }

            if(!EqualityComparer<Entities.User>.Default.Equals(User, default(Entities.User)))
            {
                UserLevel = User.Role;
                userId = User.Id;
                IsLogged = true;
            }
        }
    }
}