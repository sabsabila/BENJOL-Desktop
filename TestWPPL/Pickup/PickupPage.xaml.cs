using System;
using System.Collections.Generic;
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
using Velacro.UIElements.Basic;
using TestWPPL.Dashboard;
using Velacro.UIElements.Button;
using Velacro.UIElements.RadioButton;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Pickup
{
    /// <summary>
    /// Interaction logic for PickupPage.xaml
    /// </summary>
    public partial class PickupPage : MyPage
    {
        private MyPage dashboard;
        private MyWindow benjolWindow;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderRadioButton radioButtonBuilder;
        private IMyButton save_btn;
        private IMyTextBox bookingId_TxtBox;
        private IMyRadioButton pickup_rb;
        private IMyRadioButton processing_rb;
        private IMyRadioButton delivering_rb;

        public PickupPage()
        {
            InitializeComponent();
           
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
