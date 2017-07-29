using bbFiles.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace bbFiles.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        IMainDataAccessService _serviceProxy;
        private static MainViewModel _instance;
        public static MainViewModel Instance { get { return _instance; } }
        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; RaisePropertyChanged("SelectedViewModel"); }
        }
        Role _userLevel = Role.Wrong;
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

        public MainViewModel(IMainDataAccessService serviceProxy)
        {
            _instance = this;
            _serviceProxy = serviceProxy;
            OpenUserViewCommand = new RelayCommand(OpenUserView);
            OpenAcceptorViewCommand = new RelayCommand(OpenAcceptorView);
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
                IsLogged = true;
            }
        }
    }
}