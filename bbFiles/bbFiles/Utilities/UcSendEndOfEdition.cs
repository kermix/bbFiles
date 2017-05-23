using System.Windows;
using System.Windows.Controls;

namespace bbFiles
{
    partial class Utilities
    {
        public static void UcSendEndOfEdition(UserControl uc)
        {
            DependencyObject ucParent = uc.Parent;
            while (!(ucParent is UserControl))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }
            ((IUControlManagement)ucParent).editEnded = true;
            Window parentWindow = Window.GetWindow(ucParent);
            ((DockerWindow)parentWindow).g_Navigation.IsEnabled = true;
        }
    }
}
