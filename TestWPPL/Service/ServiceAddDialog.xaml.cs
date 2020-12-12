using System;
using System.IO;
using System.Windows;
using Velacro.UIElements.Basic;

namespace TestWPPL.Service
{
    public partial class ServiceAddDialog : MyWindow
    {

        public ServiceAddDialog()
        {
            InitializeComponent();
            setController(new ServiceController(this));
            txtAnswer.Text = "";
        }

        private void btnSaveService_Click(object sender, RoutedEventArgs e)
        {
            if (!txtAnswer.Text.Equals(""))
            {
                String token = File.ReadAllText(@"userToken.txt");
                getController().callMethod("postService", txtAnswer.Text, token);
            }else
                MessageBox.Show("Please fill in all fields before saving", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
                        this.DialogResult = true;
                        this.Close();
                        break;
                }
            });
        }
    }
}
