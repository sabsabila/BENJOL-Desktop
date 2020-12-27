using System;
using System.IO;
using System.Windows;
using TestWPPL.Model;
using Velacro.UIElements.Basic;

namespace TestWPPL.Service
{
    public partial class ServiceEditDialog : MyWindow
    {
        ModelService dataObject;
        public ServiceEditDialog(ModelService _dataObject)
        {
            InitializeComponent();
            setController(new ServiceController(this));
            txtAnswer.Text = _dataObject.service_name;
            this.dataObject = _dataObject;
        }

        private void btnSaveService_Click(object sender, RoutedEventArgs e)
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("editService", txtAnswer.Text, dataObject.service_id, token);
            this.DialogResult = true;
            this.Close();
        }
        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
