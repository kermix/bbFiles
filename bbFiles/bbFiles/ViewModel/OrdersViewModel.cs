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
    public class OrdersViewModel : ViewModelBase
    {
        IOrdersDataAccessService _serviceProxy;
        ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; RaisePropertyChanged("Orders"); }
        }
        Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                RaisePropertyChanged("SelectedOrder");
                RaisePropertyChanged("SelectedOrderAmount");
                RaisePropertyChanged("SelectedOrderBloodType");
                RaisePropertyChanged("SelectedOrderRhMarker");
                RaisePropertyChanged("SelectedOrderSend");
                RaisePropertyChanged("SelectedOrderAcceptorId");
                RaisePropertyChanged("SelectedOrderDate");
            }
        }

        #region Notifable SelectedDonate properties
        public int SelectedOrderAmount
        {
            get { return SelectedOrder.Amount; }
            set { SelectedOrder.Amount = value; RaisePropertyChanged("SelectedOrderAmount"); }
        }
        public BloodType SelectedOrderBloodType
        {
            get { return SelectedOrder.Blood_Type; }
            set { SelectedOrder.Blood_Type = value; RaisePropertyChanged("SelectedOrderBloodType"); }
        }
        public bool SelectedOrderRhMarker
        {
            get { return SelectedOrder.Blood_RhMarker; }
            set { SelectedOrder.Blood_RhMarker = value; RaisePropertyChanged("SelectedOrderRhMarker"); }
        }
        public bool SelectedOrderSend
        {
            get { return SelectedOrder.Send; }
            set { SelectedOrder.Send = value; RaisePropertyChanged("SelectedOrderSend"); }
        }
        public DateTime SelectedOrderDate
        {
            get { return SelectedOrder.OrderDate; }
            set { SelectedOrder.OrderDate = value; RaisePropertyChanged("SelectedOrderDate"); }
        }
        public int SelectedOrderAcceptorId
        {
            get { return SelectedOrder.AcceptorId; }
            set { SelectedOrder.AcceptorId = value; RaisePropertyChanged("SelectedOrderAcceptorId"); }
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
        bool _orderDetailsMode;
        public bool OrderDetailsMode
        {
            get { return _orderDetailsMode; }
            set
            {
                if (value == false)
                    InEdit = false;
                _orderDetailsMode = value;
                RaisePropertyChanged("OrderDetailsMode");
            }
        }
        #endregion
        #region Command Declarations
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand<Order> SendOrderCommand { get; set; }
        public RelayCommand<Order> MarkAsSendCommand { get; set; }
        public RelayCommand SaveOrderCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        #endregion

        public OrdersViewModel(IOrdersDataAccessService serviceProxy)
        {
            _serviceProxy = serviceProxy;
            Orders = new ObservableCollection<Order>();
            SelectedOrder = new Order();
            RefreshCommand = new RelayCommand(GetOrders);
            SendOrderCommand = new RelayCommand<Order>(SendOrder);
            MarkAsSendCommand = new RelayCommand<Order>(MarkAsSend);
            SaveOrderCommand = new RelayCommand(SaveOrder);
            CancelCommand = new RelayCommand(Cancel);
            GetOrders();
        }

        void GetOrders()
        {
            Orders.Clear();
            Orders = _serviceProxy.GetOrders();
            OrderDetailsMode = false;
        }
        void SaveOrder()
        {
            if (SelectedOrder.Amount < 0 || SelectedOrder.OrderDate == null || SelectedOrder.AcceptorId < 0)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.BlankFieldsError });
                return;
            }
            SelectedOrder.Acceptor = _serviceProxy.FindAcceptor(SelectedOrderAcceptorId);
            if (SelectedOrder.Acceptor == null)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.NoSuchAcceptorError });
                return;
            }
            System.Windows.MessageBox.Show(SelectedOrder.Blood_RhMarker.ToString() + " " + SelectedOrder.Blood_Type.ToString() + SelectedOrder.Amount.ToString());
            SelectedOrder.Donates = _serviceProxy.LinkDonates(SelectedOrder);
            if(SelectedOrder.Donates.Count == 0)
            {
                Messenger.Default.Send(new ErrorMessage() { Title = Resources.Strings.EditErrorTitle, Error = Resources.Strings.OrderTooBigError });
                return;
            }

            int id = _serviceProxy.CreateOrder(SelectedOrder);
            if (id != 0)
            {
                RaisePropertyChanged("SelectedOrder");
                GetOrders();
                (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            }
        }

        void SendOrder(Order Order)
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation(); ;
            OrderDetailsMode = true;
            if (Order == null)
            {
                SelectedOrder = new Order()
                {
                    Acceptor = new Acceptor()
                };
                SelectedOrderDate = DateTime.Now;
                SelectedOrderSend = false;
                InEdit = false;
            }
            else
            {
                InEdit = true;
                SelectedOrder = Order;
            }
        }
        
        void MarkAsSend(Order Order)
        {
            if (Order != null)
            {
                if(Order.Send != true)
                {
                    _serviceProxy.MarkAsSend(Order);
                    GetOrders();
                }
                else
                    Messenger.Default.Send(new ErrorMessage()
                    {
                        Title = Resources.Strings.OrderSelectionErrorTitle,
                        Error = Resources.Strings.OrderAlreadySendError
                    });

            }
            else
            {
                Messenger.Default.Send(new ErrorMessage()
                {
                    Title = Resources.Strings.OrderSelectionErrorTitle,
                    Error = Resources.Strings.SelectOrderFirstError
                });
            }

        }

        void Cancel()
        {
            (ServiceLocator.Current.GetInstance<MainViewModel>()).ToogleNavigation();
            GetOrders();
        }
    }
}
