using System;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using TestWPPL.Model;
using Velacro.UIElements.Basic;
using System.Windows.Input;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Payment
{
    /// <summary>
    /// Interaction logic for AddCostDialog.xaml
    /// </summary>
    public partial class AddCostDialog : MyWindow
    {
        private int bookingId;
        
        public AddCostDialog(int bookingId)
        {
            InitializeComponent();
            this.bookingId = bookingId;
            Console.WriteLine("id yang dikirim : " + bookingId);
            setController(new PaymentController(this));
            noteTxtBox.Text = "";
            costTxtBox.Text = "";

        }

        

        public void btnSaveCostPayment_Click(object sender, RoutedEventArgs e)
        {
            if (!costTxtBox.Text.Equals(""))
            {
                String token = File.ReadAllText(@"userToken.txt");
                getController().callMethod("updateServiceCost", Int32.Parse(costTxtBox.Text), noteTxtBox.Text,bookingId,token);
                this.DialogResult = true;
                this.Close();
            }
            else
                MessageBox.Show("Please fill in all fields before saving", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void costTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
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
                        
                        break;
                }
            });
        }
    }
}
