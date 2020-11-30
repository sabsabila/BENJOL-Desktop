using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWPPL.Dashboard;
using TestWPPL.Model;
using TestWPPL.Pickup;
using Velacro.UIElements.Basic;
using Velacro.UIElements.ListBox;

namespace TestWPPL.Booking
{
    public partial class BookingPage : MyPage
    {
        private String token;
        private List<ModelBooking> bookings;
        private PickupPage pickupPage;

        public BookingPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new BookingController(this));
            initUIBuilders();
            initUIElements();
            getBooking();
            pickupPage = new PickupPage();
        }
        int counter = 0;

        public void setBooking(List<ModelBooking> bookings)
        {
            this.bookings = bookings;
            this.Dispatcher.Invoke(()=> {
                bookingList.ItemsSource = this.bookings;
            });
        }

        private void initUIBuilders()
        {
        }

        private void initUIElements()
        {

        }

        private void getBooking()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("booking", token);
        }

        private void PickUpButton_OnClick(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            ModelBooking dataObject = button.DataContext as ModelBooking;
            int index = bookings.IndexOf(dataObject);
            Console.WriteLine(this.bookings.Count);
            this.NavigationService.Navigate(new PickupPage());
        }
    }
}
