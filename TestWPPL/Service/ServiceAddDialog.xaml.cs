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
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("postService", txtAnswer.Text, token);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
                        this.DialogResult = true;
                        this.Close();
                        break;
                }
            });
        }
    }
}
