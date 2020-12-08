using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestWPPL.Booking;
using TestWPPL.Pickup;
using TestWPPL.Progress;
using TestWPPL.Payment;
using TestWPPL.Sparepart;
using TestWPPL.Profile;
using Velacro.UIElements.Basic;
using TestWPPL.Service;

namespace TestWPPL.Dashboard
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BenjolWindow : MyWindow
    {

        public BenjolWindow()
        {

            InitializeComponent();
            //ini nanti ngeload dashboard kalo udah ada, page yg di load disini nanti yang pertama kali di load
            pageTitle.Text = "Dashboard";
            appFrame.Navigate(new Dashboard());
        }

        //ini buat ngarahin kalo buttonnya di klik ntar frame nya ngeload page apa
        //pake appFrame.Navigate(namaPage)
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new Dashboard());
            pageTitle.Text = "Dashboard";
            returnButtonColor();
            dashboardButton.Background = Brushes.White;
        }

        private void bookingButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new BookingPage());
            pageTitle.Text = "Booking";
            returnButtonColor();
            bookingButton.Background = Brushes.White;
        }

        private void sparepartButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new SparepartPage());
            pageTitle.Text = "Spareparts";
            returnButtonColor();
            sparepartButton.Background = Brushes.White;

        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new PaymentPage());
            pageTitle.Text = "Payment";
            returnButtonColor();
            paymentButton.Background = Brushes.White;
        }

        private void pickupButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new ListPickupPage());
            pageTitle.Text = "Pickups";
            returnButtonColor();
            pickupButton.Background = Brushes.White;
        }

        private void servicesButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new ServicePage());
            pageTitle.Text = "Services";
            returnButtonColor();
            servicesButton.Background = Brushes.White;
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(new ProfilePage());
            pageTitle.Text = "Setting";
<<<<<<< HEAD
=======
            appFrame.Navigate(new ProfilePage());
            returnButtonColor();
            settingButton.Background = Brushes.White;
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