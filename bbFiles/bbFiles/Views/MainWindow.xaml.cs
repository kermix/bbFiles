using bbFiles.Messages;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace bbFiles.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<ErrorMessage>(this, (message) => ReceiveMessage(message));
        }

        private async void ReceiveMessage(ErrorMessage message)
        {
            await this.ShowMessageAsync(message.Title, message.Error);
        }
        private async void ShowLoginDialog(object sender, RoutedEventArgs e)
        {
            LoginDialogData result = await this.ShowLoginAsync(bbFiles.Resources.Strings.LoginTitle, "", new LoginDialogSettings {
                ColorScheme = this.MetroDialogOptions.ColorScheme,
                UsernameWatermark = bbFiles.Resources.Strings.Username,
                PasswordWatermark = bbFiles.Resources.Strings.Password}
                );
            if (result == null)
            {
                this.ShowLoginDialog(this, null);
            }
            else
            {
                var vm = ((ViewModel.MainViewModel)this.DataContext);
                vm.Login(result.Username, result.Password);
                if(vm.UserLevel != Role.Wrong)
                {
                }
                else
                    this.ShowLoginDialog(this, null);

            }

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLoginDialog(this, null);
        }
    }
}
