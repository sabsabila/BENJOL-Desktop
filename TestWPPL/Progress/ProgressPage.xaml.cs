using System;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using System.IO;

namespace TestWPPL.Progress
{
    /// <summary>
    /// Interaction logic for ProgressPage.xaml
    /// </summary>
    public partial class ProgressPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBox txtBoxBuilder1;
        private IMyButton save_btn;
        private IMyTextBox txtbox_startTime;
        private IMyTextBox txtbox_endTime;
        private ProgressController progressController;

        public ProgressPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new ProgressController(this));
            initUIBuilders1();
            initUIElements1();
        }

        private void initUIElements1()
        {
            save_btn = buttonBuilder.activate(this, "saveButton")
                .addOnClick(this, "onSaveButtonClick");
            txtbox_startTime = txtBoxBuilder.activate(this, "startTime_txtBox");
            txtbox_endTime = txtBoxBuilder1.activate(this, "endTime_txtBox");
        }

        private void initUIBuilders1()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBoxBuilder1 = new BuilderTextBox();
            System.Diagnostics.Debug.WriteLine("Test uibu page");
        }

       

        public void onSaveButtonClick()
        {
            System.Diagnostics.Debug.WriteLine("Test onsave page");
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("editProgress", startTime_txtBox.Text, endTime_txtBox.Text, token);
            Console.WriteLine(token);
        }

        public void setProgressStatus(string _status)
        {
            System.Diagnostics.Debug.WriteLine("Test setprogress page");
            this.Dispatcher.Invoke(() => {
                save_btn.setText(_status);
                Console.WriteLine(_status);
            });
        }
    }
}
