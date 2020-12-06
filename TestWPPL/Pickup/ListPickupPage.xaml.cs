using System;
using System.Collections.Generic;
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
using TestWPPL.Model;
using Velacro.UIElements.Basic;

namespace TestWPPL.Pickup
{
    /// <summary>
    /// Interaction logic for ListPickupPage.xaml
    /// </summary>
    public partial class ListPickupPage : MyPage
    {
        private List<ModelPickup> listPickup;
        private List<int> actualId = new List<int>();
        public ListPickupPage()
        {
            InitializeComponent(); 
            this.KeepAlive = true;
            setController(new PickupController(this));
            getPickup();
        }

        public void setPickup(List<ModelPickup> pickups)
        {
            this.listPickup = pickups;
            actualId.Clear();
            int id = 1;
            foreach (ModelPickup pickup in pickups)
            {
                actualId.Add(pickup.booking_id);
                pickup.booking_id = id;
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
            for (int i = 0; i < listPickup.Count; i++)
            {
                listPickup.ElementAt(i).booking_id = actualId.ElementAt(i);
            }

            Button button = sender as Button;
            ModelPickup dataObject = button.DataContext as ModelPickup;
            Console.WriteLine("id booking di pickup : " + dataObject.booking_id);
            this.NavigationService.Navigate(new PickupPage(dataObject.booking_id));
        }
    }
}
