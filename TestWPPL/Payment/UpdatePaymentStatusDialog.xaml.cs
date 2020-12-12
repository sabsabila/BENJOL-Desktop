using System;
using System.IO;
using System.Windows;
using TestWPPL.Model;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.RadioButton;


namespace TestWPPL.Payment
{
    public partial class UpdatePaymentStatusDialog : MyWindow
    {
        private BuilderButton btnBuilder;
        private BuilderRadioButton radioButtonBuilder;
        private IMyButton save_Button;
        private IMyButton back_Button;
        private IMyRadioButton radioButtonPayment1;
        private IMyRadioButton radioButtonPayment2;
        ModelPayment dataObject;

        public UpdatePaymentStatusDialog(ModelPayment _dataObject)
        {
            InitializeComponent();
            setController(new PaymentController(this));
            this.dataObject = _dataObject;
            initUIBuilders();
            initUIElements();

        }

        private void initUIBuilders()
        {
            radioButtonBuilder = new BuilderRadioButton();
            btnBuilder = new BuilderButton();
        }
        private void initUIElements()
        {
            save_Button = btnBuilder.activate(this, "saveButton")
                            .addOnClick(this, "SavePaymentStatusClick");
            back_Button = btnBuilder.activate(this, "backButton")
                        .addOnClick(this, "BackButtonClick");

            radioButtonPayment1 = radioButtonBuilder
               .activate(this, "paymentStatus1")
               .setGroupName("pickupStatusGroup")
               .addOnChecked(getController(), "onRadioButtonPickup1Checked");
            radioButtonPayment2 = radioButtonBuilder
                .activate(this, "paymentStatus2")
                .setGroupName("pickupStatusGroup")
                .addOnChecked(getController(), "onRadioButtonPickup2Checked");

        }


        public void SavePaymentStatusClick()
        {

            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("updatePaymentStatus", dataObject.payment_id, token);
            this.DialogResult = true;
            this.Close();
        }

        public void BackButtonClick()
        {
            this.Close();
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
                        this.Close();
                        var navigation = new PaymentPage();
                        break;
                }
            });
        }

    }
}
