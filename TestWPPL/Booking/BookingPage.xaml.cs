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
        private List<ModelBooking> listBooking;
        private List<int> actualId = new List<int>();

        public BookingPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new BookingController(this));
            initUIBuilders();
            initUIElements();
            getBooking();
        }

        public void setBooking(List<ModelBooking> bookings)
        {
            this.listBooking = bookings;

            int id = 1;
            foreach(ModelBooking booking in bookings)
            {
                actualId.Add(booking.booking_id);
                booking.booking_id = id;
                id++;
            }

            this.Dispatcher.Invoke((Action)(() =>{
                this.bookingList.ItemsSource = bookings;
            }));
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
            for (int i = 0; i < listBooking.Count; i++)
            {
                listBooking.ElementAt(i).booking_id = actualId.ElementAt(i);
            }

            Button button = sender as Button;
            ModelBooking dataObject = button.DataContext as ModelBooking;
            this.NavigationService.Navigate(new PickupPage(dataObject.booking_id));
        }
    }
}