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
using TestWPPL.Sparepart;
using Velacro.UIElements.Basic;

namespace TestWPPL.Dashboard
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BenjolWindow : MyWindow
    {
        private ProgressPage progressPage;
        private PickupPage pickupPage;
        private BookingPage bookingPage;
        private SparepartPage sparepartPage;

        public BenjolWindow()
        {
            
            InitializeComponent();
            progressPage = new ProgressPage();
            pickupPage = new PickupPage(1);
            bookingPage = new BookingPage();
            sparepartPage = new SparepartPage();

            //ini nanti ngeload dashboard kalo udah ada, page yg di load disini nanti yang pertama kali di load
            appFrame.Navigate(bookingPage);
            pageTitle.Text = "Dashboard";
        }

        //ini buat ngarahin kalo buttonnya di klik ntar frame nya ngeload page apa
        //pake appFrame.Navigate(namaPage)
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            pageTitle.Text = "Dashboard";
        }

        private void bookingButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(bookingPage);
            pageTitle.Text = "Booking";
        }

        private void sparepartButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(sparepartPage);
            pageTitle.Text = "Spareparts";
        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            pageTitle.Text = "Payments";
        }

        private void pickupButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(pickupPage);
            pageTitle.Text = "Pickups";
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            pageTitle.Text = "Setting";
        }
    }
}
