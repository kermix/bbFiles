/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:bbFiles"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using bbFiles.Services;

namespace bbFiles.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<IUsersDataAccessService, UsersDataAccessService>();
            SimpleIoc.Default.Register<IAcceptorsDataService, AcceptorDataAccessService>();
            SimpleIoc.Default.Register<IDonorsAccessDataService, DonorsDataAccessService>();
            SimpleIoc.Default.Register<IDonatesDataAccessService, DonatesDataAccessService>();
            SimpleIoc.Default.Register<IOrdersDataAccessService, OrdersDataAccessService>();
            SimpleIoc.Default.Register<IMainDataAccessService, MainDataAccessService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UsersViewModel>();
            SimpleIoc.Default.Register<AcceptorsViewModel>();
            SimpleIoc.Default.Register<DonorsViewModel>();
            SimpleIoc.Default.Register<DonatesViewModel>();
            SimpleIoc.Default.Register<OrdersViewModel>();


        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public UsersViewModel User
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UsersViewModel>();
            }
        }
        public AcceptorsViewModel Acceptor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AcceptorsViewModel>();
            }
        }

        public DonorsViewModel Donor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DonorsViewModel>();
            }
        }

        public DonatesViewModel Donate
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DonatesViewModel>();
            }
        }
        public OrdersViewModel Order
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrdersViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}