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
using System.Windows.Shapes;
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
