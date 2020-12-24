using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Collections.Generic;
using TestWPPL.Model;
using Velacro.UIElements.Basic;

namespace TestWPPL.Payment
{
    public partial class PaymentPage : MyPage
    {
        public PaymentPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new PaymentController(this));
            getPayment();
        }
        
        public void getPayment()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("showPayments", token);
        }

        public void setPayment(List<ModelPayment> payments)
        {
            int id = 1;

            foreach (ModelPayment payment in payments)
            {
                payment.num = id;
                if (payment.receipt == null)
                    payment.receipt = "/image/image.png";
                else
                    payment.receipt = ApiConstant.BASE_URL + payment.receipt;
                id++;
            }

            foreach (ModelPayment payment in payments)
            {
                if (payment.status.Equals("unpaid"))
                    payment.buttonAction = "Process invoice";
                else if (payment.status.Equals("verification"))
                    payment.buttonAction = "Verification";
                else if (payment.status.Equals("paid"))
                    payment.buttonAction = "Done";
                payment.num = id;
                id++;
            }


            this.Dispatcher.Invoke((Action)(() => {
                paymentList.ItemsSource = payments;
            }));
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new PaymentPage());
                        break;
                }
            });
        }

        public void onUpdateStatusPaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            String status = null;
            Button button = sender as Button;

            if (button.Content.Equals("Process invoice"))
            {
                status = "verification";
            }
            else if (button.Content.Equals("Verification"))
            {
                status = "paid";
            }
            else if (button.Content.Equals("Done"))
            {
                status = null;
            }

               

            ModelPayment dataObject = button.DataContext as ModelPayment;
            String token = File.ReadAllText(@"userToken.txt");
            MessageBoxResult result;
            if (status != null)
                getController().callMethod("updatePaymentStatus", status, dataObject.payment_id, token);
            else
                result = MessageBox.Show("invoice has been paid !", "Finished payment", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void onInputCostBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelPayment dataObject = button.DataContext as ModelPayment;

            var editDialog2 = new AddCostDialog(dataObject.booking_id);

            if (editDialog2.ShowDialog() == true)
            {
                this.NavigationService.Navigate(new PaymentPage());
            }
            
        }

    }
}
