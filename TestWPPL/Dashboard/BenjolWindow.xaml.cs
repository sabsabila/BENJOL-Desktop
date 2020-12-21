using System.Windows;
using System.Windows.Media;
using TestWPPL.Booking;
using TestWPPL.Pickup;
using TestWPPL.Payment;
using TestWPPL.Sparepart;
using TestWPPL.Profile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;
using TestWPPL.Service;

namespace TestWPPL.Dashboard
{
    public partial class BenjolWindow : MyWindow
    {

        public BenjolWindow()
        {

            InitializeComponent();
            pageTitle.Text = "Dashboard";
            appFrame.Navigate(new Dashboard());
        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new Dashboard());
            pageTitle.Text = "Dashboard";
            returnButtonColor();
            dashboardButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
        }

        private void bookingButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new BookingPage());
            pageTitle.Text = "Booking";
            returnButtonColor();
            bookingButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
        }

        private void sparepartButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new SparepartPage());
            pageTitle.Text = "Spareparts";
            returnButtonColor();
            sparepartButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
            

        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new PaymentPage());
            pageTitle.Text = "Payment";
            returnButtonColor();
            paymentButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
        }

        private void pickupButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new ListPickupPage());
            pageTitle.Text = "Pickups";
            returnButtonColor();
            pickupButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
            
        }

        private void servicesButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new ServicePage());
            pageTitle.Text = "Services";
            returnButtonColor();
            servicesButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new ProfilePage());
            pageTitle.Text = "Setting";
<<<<<<< HEAD
=======
            appFrame.Navigate(new ProfilePage());
            returnButtonColor();
            settingButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0F0F7"));
        }

        private void returnButtonColor()
        {
            dashboardButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
            bookingButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
            sparepartButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
            paymentButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
            pickupButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
            servicesButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
            settingButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFCCA53"));
>>>>>>> 3228037abb565b89d77d4fa764561afe908d5df9
        }


    }
}