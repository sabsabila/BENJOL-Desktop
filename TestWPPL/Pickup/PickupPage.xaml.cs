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
using TestWPPL.Booking;
using TestWPPL.Dashboard;
using TestWPPL.Login;
using TestWPPL.Model;
using Velacro.Basic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.RadioButton;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Pickup
{
    /// <summary>
    /// Interaction logic for PickupPage.xaml
    /// </summary>
    public partial class PickupPage : MyPage
    {
        private int _bookingId;
        private BuilderButton buttonBuilder;
        private BuilderRadioButton radioButtonBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton buttonSave;
        private IMyButton buttonBack;
        private IMyTextBlock nameTxtBlock;
        private IMyTextBlock phoneNumberTxtBlock;
        private IMyRadioButton radioButtonPickup1;
        private IMyRadioButton radioButtonPickup2;
        private IMyRadioButton radioButtonPickup3;

        public PickupPage(int _bookingId)
        {
            InitializeComponent();
            this._bookingId = _bookingId;
            this.KeepAlive = true;
            setController(new PickupController(this));
            initUIBuilders();
            initUIElements();
            getUser();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            radioButtonBuilder = new BuilderRadioButton();
            txtBlockBuilder = new BuilderTextBlock();
        }

        public void initUIElements()
        {
            buttonSave = buttonBuilder
                .activate(this, "saveButton")
                .addOnClick(this, "onSaveButtonClick");
            buttonBack = buttonBuilder
                .activate(this, "backButton")
                .addOnClick(this, "onBackButtonClick");
            nameTxtBlock = txtBlockBuilder.activate(this, "nameTxt");
            phoneNumberTxtBlock = txtBlockBuilder.activate(this, "phoneNumberTxt");
            radioButtonPickup1 = radioButtonBuilder
                .activate(this, "pickupStatus1")
                .setGroupName("pickupStatusGroup")
                .addOnChecked(getController(), "onRadioButtonPickup1Checked");
            radioButtonPickup2 = radioButtonBuilder
                .activate(this, "pickupStatus2")
                .setGroupName("pickupStatusGroup")
                .addOnChecked(getController(), "onRadioButtonPickup2Checked");
            radioButtonPickup3 = radioButtonBuilder
                .activate(this, "pickupStatus3")
                .setGroupName("pickupStatusGroup")
                .addOnChecked(getController(), "onRadioButtonPickup3Checked");
        }

        public void onSaveButtonClick()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("pickup", _bookingId, token);
        }

        public void onBackButtonClick()
        {
            this.NavigationService.Navigate(new BookingPage());
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Set Pickup Status", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new ListPickupPage());
                        break;
                }
            });
        }

        private void getUser()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestUser", _bookingId, token);
        }

        public void setUser(ModelUser user)
        {
            this.Dispatcher.Invoke(() => {
                nameTxtBlock.setText(user.first_name + " " + user.last_name);
                if(user.phone_number != null)
                    phoneNumberTxtBlock.setText(user.phone_number);
            });
        }
    }
}
