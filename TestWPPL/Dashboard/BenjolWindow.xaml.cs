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
using TestWPPL.Pickup;
using TestWPPL.Progress;
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

        public BenjolWindow()
        {
            InitializeComponent();
            progressPage = new ProgressPage();
            pickupPage = new PickupPage(1);

            //ini nanti ngeload dashboard kalo udah ada, page yg di load disini nanti yang pertama kali di load
            appFrame.Navigate(pickupPage);
        }

        //ini buat ngarahin kalo buttonnya di klik ntar frame nya ngeload page apa
        //pake appFrame.Navigate(namaPage)
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void progressButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(progressPage);
        }

        private void sparepartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pickupButton_Click(object sender, RoutedEventArgs e)
        {
            appFrame.Navigate(pickupPage);
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
