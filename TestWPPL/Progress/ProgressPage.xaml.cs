using System;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using System.IO;
using TestWPPL.Model;
using Velacro.UIElements.TextBlock;
using TestWPPL.Booking;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace TestWPPL.Progress
{
    /// <summary>
    /// Interaction logic for ProgressPage.xaml
    /// </summary>
    public partial class ProgressPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton save_btn;
        private IMyButton back_btn;
        private IMyTextBox startTimeH;
        private IMyTextBox startTimeM;
        private IMyTextBox startTimeS;
        private IMyTextBox endTimeH;
        private IMyTextBox endTimeM;
        private IMyTextBox endTimeS;
        private IMyTextBlock nameTxtBlock;
        private IMyTextBlock phoneNumberTxtBlock;
        private int bookingId;

        public ProgressPage(int _bookingId)
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new ProgressController(this));
            initUIBuilders();
            initUIElements();
            this.bookingId = _bookingId;
            getUser();
        }

        private void initUIElements()
        {
            save_btn = buttonBuilder.activate(this, "saveButton")
                .addOnClick(this, "onSaveButtonClick");
            back_btn = buttonBuilder.activate(this, "backButton")
                .addOnClick(this, "onBackButtonClick");
            startTimeH = txtBoxBuilder.activate(this, "startTimeHour");
            startTimeM = txtBoxBuilder.activate(this, "startTimeMinute");
            startTimeS = txtBoxBuilder.activate(this, "startTimeSecond");
            endTimeH = txtBoxBuilder.activate(this, "endTimeHour");
            endTimeM = txtBoxBuilder.activate(this, "endTimeMinute");
            endTimeS = txtBoxBuilder.activate(this, "endTimeSecond");
            nameTxtBlock = txtBlockBuilder.activate(this, "nameTxt");
            phoneNumberTxtBlock = txtBlockBuilder.activate(this, "phoneNumberTxt");
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
            System.Diagnostics.Debug.WriteLine("Test uibu page");
        }

       

        public void onSaveButtonClick()
        {
            System.Diagnostics.Debug.WriteLine("Test onsave page");
            String token = File.ReadAllText(@"userToken.txt");
            String startTime = startTimeH.getText().ToString() + ":" + startTimeM.getText().ToString() + ":" + startTimeS.getText().ToString();
            String endTime = endTimeH.getText().ToString() + ":" + endTimeM.getText().ToString() + ":" + endTimeS.getText().ToString();
            Console.WriteLine("INI START TIME" + startTime);
            getController().callMethod("editProgress", bookingId, startTime, endTime, token);
            Console.WriteLine(token);
        }

        public void onBackButtonClick()
        {
            this.NavigationService.Navigate(new BookingPage());
        }

        public void setProgressStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Set Pickup Status", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new BookingPage());
                        break;
                }
            });
        }

        private void getUser()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestUser", bookingId, token);
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void setUser(ModelUser user)
        {
            this.Dispatcher.Invoke(() => {
                nameTxtBlock.setText(user.first_name + " " + user.last_name);
                if (user.phone_number != null)
                    phoneNumberTxtBlock.setText(user.phone_number);
                if (user.profile_picture != null)
                    picture.ImageSource = new BitmapImage(new Uri(ApiConstant.BASE_URL + user.profile_picture));
            });
        }

        private void startTimeHour_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void endTimeHour_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void startTimeMinute_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void startTimeSecond_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void endTimeMinute_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void endTimeSecond_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
