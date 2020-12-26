using System.Windows;
using System.IO;
using System.Windows.Controls;
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
                if (payment.status.Equals("unpaid"))
                    payment.buttonAction = "Process Payment";
                else if (payment.status.Equals("pending"))
                    payment.buttonAction = "Confirm Payment";
                else if (payment.status.Equals("paid"))
                    payment.buttonAction = "Confirmed";

                if (payment.receipt == null)
                    payment.receipt = "/image/image.png";
                else
                    payment.receipt = ApiConstant.BASE_URL + payment.receipt;
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

            if (button.Content.Equals("Process Payment"))
            {
                status = "pending";
            }
            else if (button.Content.Equals("Confirm Payment"))
            {
                status = "paid";
            }
            else if (button.Content.Equals("Confirmed"))
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

            if (dataObject.status.Equals("unpaid"))
            {
                var editDialog2 = new AddCostDialog(dataObject.booking_id);

                if (editDialog2.ShowDialog() == true)
                {
                    this.NavigationService.Navigate(new PaymentPage());
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Can't edit service cost as payment have been processed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
