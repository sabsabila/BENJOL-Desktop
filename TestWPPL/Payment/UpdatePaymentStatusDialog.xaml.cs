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
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;


namespace TestWPPL.Payment
{
    /// <summary>
    /// Interaction logic for UpdatePaymentStatusDialog.xaml
    /// </summary>
    public partial class UpdatePaymentStatusDialog : MyWindow
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private IMyButton save_Button;
        private IMyButton back_Button;
        private IMyTextBox statusTxtBox;
        ModelPayment dataObject;
        
        public UpdatePaymentStatusDialog(ModelPayment _dataObject)
        {
            InitializeComponent();
            setController(new PaymentController(this));
            this.dataObject = _dataObject;
            statusTxt.Text = _dataObject.status;
            Console.WriteLine("id yang dikirim : " + _dataObject.payment_id);
            
            initUIBuilders();
            initUIElements();
           
        }
        private void initUIElements()
        {
            save_Button = btnBuilder.activate(this, "saveButton")
                            .addOnClick(this, "SavePaymentStatusClick");
            back_Button = btnBuilder.activate(this, "backButton")
                        .addOnClick(this, "BackButtonClick");
            statusTxtBox = txtBoxBuilder.activate(this, "statusTxt");
            
        }


        public void SavePaymentStatusClick()
        {
        
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("updatePaymentStatus", statusTxtBox.getText(), dataObject.payment_id, token);
            this.DialogResult = true;
            this.Close();
            
            
        }

         public void BackButtonClick()
        {
            this.Close();
        }
    

        private void initUIBuilders()
        {
            txtBoxBuilder = new BuilderTextBox();
            btnBuilder = new BuilderButton();
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
