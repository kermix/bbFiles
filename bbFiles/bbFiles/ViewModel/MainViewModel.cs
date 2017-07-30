using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace bbFiles.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        IMainDataAccessService _serviceProxy;
        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; RaisePropertyChanged("SelectedViewModel"); }
        }
        Role _userLevel = Role.Wrong;
        public int userId { get; private set; }
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
        public bool CanNavigate
        {
            get { return _canNavigate; }
            private set { _canNavigate = value; RaisePropertyChanged("CanNavigate"); }
        }

        public RelayCommand OpenUserViewCommand { get; set; }
        public RelayCommand OpenAcceptorViewCommand { get; set; }
        public RelayCommand OpenDonorViewCommand { get; set; }
        public RelayCommand OpenDonateViewCommand { get; set; }
        public RelayCommand OpenOrderViewCommand { get; set; }
        public RelayCommand OpenStatisticViewCommand { get; set; }


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

        public void ToogleNavigation()
        {
            CanNavigate = !CanNavigate;
        }

        public void Login(string username, string password)
        {
            var User = _serviceProxy.LogIn(username, password);

            if(!EqualityComparer<Entities.User>.Default.Equals(User, default(Entities.User)))
            {
                UserLevel = User.Role;
                userId = User.Id;
                IsLogged = true;
            }
        }
    }
}