using System;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using System.IO;
using TestWPPL.Model;
using Velacro.UIElements.TextBlock;
using TestWPPL.Booking;

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
        private IMyTextBox txtbox_startTime;
        private IMyTextBox txtbox_endTime;
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
            txtbox_startTime = txtBoxBuilder.activate(this, "startTime_txtBox");
            txtbox_endTime = txtBoxBuilder.activate(this, "endTime_txtBox");
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
            getController().callMethod("editProgress", bookingId, startTime_txtBox.Text, endTime_txtBox.Text, token);
            Console.WriteLine(token);
        }

        public void onBackButtonClick()
        {
            this.NavigationService.Navigate(new BookingPage());
        }

        public void setProgressStatus(string _status)
        {
            System.Diagnostics.Debug.WriteLine("Test setprogress page");
            this.Dispatcher.Invoke(() => {
                save_btn.setText(_status);
                Console.WriteLine(_status);
            });
        }

        private void getUser()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestUser", bookingId, token);
        }

        public void setUser(ModelUser user)
        {
            this.Dispatcher.Invoke(() => {
                nameTxtBlock.setText(user.first_name + " " + user.last_name);
                if (user.phone_number != null)
                    phoneNumberTxtBlock.setText(user.phone_number);
            });
        }
    }
}
