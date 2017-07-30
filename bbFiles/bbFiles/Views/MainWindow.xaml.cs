using bbFiles.Messages;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

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
                PasswordWatermark = bbFiles.Resources.Strings.Password,
                NegativeButtonVisibility = Visibility.Visible,
                NegativeButtonText = bbFiles.Resources.Strings.Close,
                AffirmativeButtonText = bbFiles.Resources.Strings.LogIn
            });
            if (result != null)
            {
                var vm = ((ViewModel.MainViewModel)this.DataContext);
                vm.Login(result.Username, result.Password);
                if(vm.UserLevel == Role.Wrong)
                    this.ShowLoginDialog(this, null);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLoginDialog(this, null);
        }
    }
}
