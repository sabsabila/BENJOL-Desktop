using System.Linq;
using System.Windows;
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
        private List<ModelPayment> listPayments;
        private List<int> actualId = new List<int>();
        private CollectionView view;

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
            this.listPayments = payments;
            actualId.Clear();
            int id = 1;
            foreach (ModelPayment payment in payments)
            {
                actualId.Add(payment.payment_id);
                payment.payment_id = id;
                id++;
            }

            this.Dispatcher.Invoke((Action)(() => {
                paymentList.ItemsSource = payments;
                view = (CollectionView)CollectionViewSource.GetDefaultView(paymentList.ItemsSource);

            }));
        }



        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.paymentList.ItemsSource).Refresh();
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
            for (int i = 0; i < listPayments.Count; i++)
            {
                listPayments.ElementAt(i).payment_id = actualId.ElementAt(i);
            }

            Button button = sender as Button;
            ModelPayment dataObject = button.DataContext as ModelPayment;

            var editDialog = new UpdatePaymentStatusDialog(dataObject);

            if (editDialog.ShowDialog() == true)
            {
                this.NavigationService.Navigate(new PaymentPage());
            }

        }


    }
}
