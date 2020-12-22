using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Model;
using TestWPPL.Pickup;
using TestWPPL.Progress;
using Velacro.UIElements.Basic;

namespace TestWPPL.Booking
{
    public partial class BookingPage : MyPage
    {
        public BookingPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new BookingController(this));
            getBooking();
        }

        public void setBooking(List<ModelBooking> bookings)
        {
            int id = 1;
            foreach(ModelBooking booking in bookings)
            {
                booking.num = id;
                if (booking.start_time == null)
                    booking.start_time = "-";
                if (booking.end_time == null)
                    booking.end_time = "-";
                id++;
            }

            this.Dispatcher.Invoke((Action)(() =>{
                this.bookingList.ItemsSource = bookings;
            }));
        }

        private void getBooking()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("booking", token);
        }

        private void progressBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelBooking dataObject = button.DataContext as ModelBooking;

            Console.WriteLine("id booking di progress : " + dataObject.booking_id);
            this.NavigationService.Navigate(new ProgressPage(dataObject.booking_id));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelBooking dataObject = button.DataContext as ModelBooking;

            Console.WriteLine("id booking di booking : " + dataObject.booking_id);

            String token = File.ReadAllText(@"userToken.txt");

            MessageBoxResult result = MessageBox.Show("Please contact the customer before canceling, or make sure the service is done. Proceed ?", "Delete Booking", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    getController().callMethod("deleteBooking", dataObject.booking_id, token);
                    break;
            }
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Delete Booking", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new BookingPage());
                        break;
                }
            });
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}