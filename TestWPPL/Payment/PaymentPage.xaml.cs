using System.Windows;
using System.IO;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using TestWPPL.Model;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using System.Windows.Data;

namespace TestWPPL.Payment
{
    public partial class PaymentPage : MyPage
    {

        private BuilderButton builderButton;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton refreshButton;
        private IMyTextBox searchTextBox;
        private CollectionView view;

        public PaymentPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new PaymentController(this));
            initUIBuilders();
            initUIElements();
            getPayment();
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
                DateTime date = DateTime.Parse(payment.repairment_date);
                payment.repairment_date = date.ToString("dd MMMM yyyy");
                if (payment.status.Equals("unpaid"))
                {
                    payment.buttonAction = "Process Payment";
                    if (payment.service_cost == null)
                        payment.buttonAction = "Unprocessed";
                }
                else if (payment.status.Equals("pending"))
                    payment.buttonAction = "Confirm Payment";
                else if (payment.status.Equals("paid"))
                    payment.buttonAction = "Confirmed";

                if (payment.receipt == null)
                    payment.receipt = "/image/image.png";
                else
                    payment.receipt = ApiConstant.BASE_URL + payment.receipt;
                if (payment.bengkel_note == null)
                    payment.bengkel_note = "-";
                id++;
            }

            this.Dispatcher.Invoke(() => {
                paymentList.ItemsSource = payments;
                view = (CollectionView)CollectionViewSource.GetDefaultView(paymentList.ItemsSource);
                view.Filter = UserFilter;
            });
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(searchTextBox.getText()))
                return true;
            else
                return ((item as ModelPayment).status.IndexOf(searchTextBox.getText(), StringComparison.OrdinalIgnoreCase) >= 0);
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
            else if(dataObject.buttonAction.Equals("Unprocessed"))
                result = MessageBox.Show("Please input cost first", "Process Payment", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                result = MessageBox.Show("Invoice has been paid", "Finished payment", MessageBoxButton.OK, MessageBoxImage.Information);

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
