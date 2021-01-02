using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TestWPPL.Model;
using TestWPPL.Pickup;
using TestWPPL.Progress;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Booking
{
    public partial class BookingPage : MyPage
    {
        private BuilderButton builderButton;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton refreshButton;
        private IMyTextBox searchTextBox;
        private CollectionView view;

        public BookingPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new BookingController(this));
            initUIBuilders();
            initUIElements();
            getBooking();
        }

        private void initUIBuilders()
        {
            builderButton = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            refreshButton = builderButton
                .activate(this, "refreshBtn")
                .addOnClick(this, "onRefreshButtonClick");
            searchTextBox = txtBoxBuilder.activate(this, "searchBox");
        }

        public void onRefreshButtonClick()
        {
            getBooking();
        }

        public void setBooking(List<ModelBooking> bookings)
        {
            int id = 1;
            foreach (ModelBooking booking in bookings)
            {
                DateTime date = DateTime.Parse(booking.repairment_date);
                booking.repairment_date = date.ToString("dd MMMM yyyy");
                booking.num = id;
                if (booking.start_time == null)
                    booking.start_time = "-";
                if (booking.end_time == null)
                    booking.end_time = "-";
                id++;
            }

            this.Dispatcher.Invoke(() => {
                bookingList.ItemsSource = bookings;
                view = (CollectionView)CollectionViewSource.GetDefaultView(bookingList.ItemsSource);
                view.Filter = UserFilter;
            });
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(searchTextBox.getText()))
                return true;
            else
                return ((item as ModelBooking).status.IndexOf(searchTextBox.getText(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (item as ModelBooking).repairment_date.IndexOf(searchTextBox.getText(), StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.bookingList.ItemsSource).Refresh();
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

            MessageBoxResult result;
            if (dataObject.status.Equals("finished"))
                result = MessageBox.Show("This booking has already been finished.", "Set Service Time", MessageBoxButton.OK, MessageBoxImage.Information);
            else if(dataObject.status.Equals("canceled"))
                result = MessageBox.Show("This booking has been canceled.", "Set Service Time", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                this.NavigationService.Navigate(new ProgressPage(dataObject.booking_id, dataObject.start_time, dataObject.end_time));
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelBooking dataObject = button.DataContext as ModelBooking;

            if (dataObject.status.Equals("ongoing"))
            {
                MessageBoxResult result = MessageBox.Show("Can't cancel on going booking.", "Cancel Booking", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (dataObject.status.Equals("finished"))
            {
                MessageBoxResult result = MessageBox.Show("This booking has already been finished.", "Cancel Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dataObject.status.Equals("canceled"))
            {
                MessageBoxResult result = MessageBox.Show("This booking has already been canceled.", "Cancel Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                String token = File.ReadAllText(@"userToken.txt");

                MessageBoxResult result = MessageBox.Show("Please contact the customer before canceling, or make sure the service is done. Proceed ?", "Cancel Booking", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        getController().callMethod("statusBooking", dataObject.booking_id, "canceled", token);
                        break;
                }
            }
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelBooking dataObject = button.DataContext as ModelBooking;

            if (dataObject.status.Equals("upcoming"))
            {
                MessageBoxResult result = MessageBox.Show("Please process this booking first before finishing it.", "Finish Booking", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (dataObject.status.Equals("finished"))
            {
                MessageBoxResult result = MessageBox.Show("This booking has already been finished.", "Finish Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dataObject.status.Equals("canceled"))
            {
                MessageBoxResult result = MessageBox.Show("This booking has already been canceled.", "Finish Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                String token = File.ReadAllText(@"userToken.txt");

                MessageBoxResult result = MessageBox.Show("Finish this booking ?", "Finish Booking", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        getController().callMethod("statusBooking", dataObject.booking_id, "finished", token);
                        break;
                }
            }
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Status Booking", MessageBoxButton.OK, MessageBoxImage.Information);
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