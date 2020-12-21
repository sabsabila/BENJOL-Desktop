using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Model;
using Velacro.UIElements.Basic;

namespace TestWPPL.Pickup
{
    public partial class ListPickupPage : MyPage
    {
        public ListPickupPage()
        {
            InitializeComponent(); 
            this.KeepAlive = true;
            setController(new PickupController(this));
            getPickup();
        }

        public void setPickup(List<ModelPickup> pickups)
        {
            int id = 1;
            foreach (ModelPickup pickup in pickups)
            {
                pickup.num = id;
                id++;
            }

            this.Dispatcher.Invoke((Action)(() => {
                this.pickupList.ItemsSource = pickups;
            }));
        }

        private void getPickup()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestPickup", token);
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelPickup dataObject = button.DataContext as ModelPickup;
            this.NavigationService.Navigate(new PickupPage(dataObject.booking_id));
        }
    }
}
