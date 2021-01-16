using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TestWPPL.Model;
using Velacro.Basic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.PasswordBox;
using Velacro.UIElements.TextBox;


namespace TestWPPL.Profile
{
    public partial class ProfilePage : MyPage
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private BuilderPasswordBox passBoxBuilder;
        private IMyButton updateButton, uploadButton, logoutButton;
        private IMyTextBox nameTxtBox, phoneTxtBox, emailTxtBox, addressTxtBox;
        private IMyPasswordBox oldPass, newPass;
        private MyList<MyFile> uploadImage = new MyList<MyFile>();

        public ProfilePage()
        {
            InitializeComponent();
            setController(new ProfileController(this));
            initUIBuilders();
            initUIElements();
            getEditedItem();
        }

        private void initUIBuilders()
        {
            txtBoxBuilder = new BuilderTextBox();
            btnBuilder = new BuilderButton();
            passBoxBuilder = new BuilderPasswordBox();
        }

        private void initUIElements()
        {
            updateButton = btnBuilder.activate(this, "updateBtn")
                            .addOnClick(this, "onUpdateButtonClick");
            uploadButton = btnBuilder.activate(this, "uploadBtn")
                        .addOnClick(this, "onUploadButtonClick");
            logoutButton = btnBuilder.activate(this, "logoutBtn")
                            .addOnClick(this, "onLogoutButtonClick");
            nameTxtBox = txtBoxBuilder.activate(this, "nameTxt");
            phoneTxtBox = txtBoxBuilder.activate(this, "phoneTxt");
            emailTxtBox = txtBoxBuilder.activate(this, "emailTxt");
            addressTxtBox = txtBoxBuilder.activate(this, "addressTxt");
            oldPass = passBoxBuilder.activate(this, "oldPassword");
            newPass = passBoxBuilder.activate(this, "newPassword");
        }

        private void getEditedItem()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("getProfile", token);
        }

        public void onUpdateButtonClick()
        {
            MessageBoxResult result;
            if (!nameTxtBox.getText().Equals("") && !phoneTxtBox.getText().Equals("") && !emailTxtBox.getText().Equals("") && !addressTxtBox.getText().Equals(""))
            {
                String phoneNumber = "62" + phoneTxtBox.getText();
                ObjectBengkel newBengkel = new ObjectBengkel(nameTxtBox.getText(), phoneNumber, emailTxtBox.getText(), addressTxtBox.getText());
                String token = File.ReadAllText(@"userToken.txt");
                String[] password = new String[2];
                if (!newPass.getPassword().ToString().Equals(""))
                {
                    password[0] = oldPass.getPassword();
                    password[1] = newPass.getPassword();
                }
                getController().callMethod("editProfile", uploadImage, password, newBengkel, token);
            }else
                result = MessageBox.Show("All field must be filled !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void onUploadButtonClick()
        {
            uploadImage.Clear();
            OpenFile openFile = new OpenFile();
            uploadImage.Add(openFile.openFile(false)[0]);
            if (uploadImage[0] != null)
            {
                if (uploadImage[0].extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase) ||
                    uploadImage[0].extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                    uploadImage[0].extension.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                    picture.Source = new BitmapImage(new Uri(uploadImage[0].fullPath));
                else
                {
                    MessageBoxResult result = MessageBox.Show("File format not supported !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    uploadImage.Clear();
                }
            }
            else
                uploadImage.Clear();
        }

        public void onLogoutButtonClick()
        {
            String token = File.ReadAllText(@"userToken.txt");
            MessageBoxResult result = MessageBox.Show("Are you sure ?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    string fullPath = @"userToken.txt";
                    File.WriteAllText(fullPath, "");
                    var mainWindow = new MainWindow();
                    Window.GetWindow(this).Close();
                    mainWindow.Show();
                    break;
            }
        }

        public void setProfile(ModelBengkel bengkel)
        {
            this.Dispatcher.Invoke(() => {
                nameTxtBox.setText(bengkel.name);
                string number = "";
                if (bengkel.phone_number != null)
                    number = bengkel.phone_number.Substring(bengkel.phone_number.IndexOf('2') + 1);
                phoneTxtBox.setText(number);
                emailTxtBox.setText(bengkel.email);
                addressTxtBox.setText(bengkel.address);
                if (bengkel.profile_picture != null)
                    picture.Source = new BitmapImage(new Uri(ApiConstant.BASE_URL + bengkel.profile_picture));
            });
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Edit Profile", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new ProfilePage());
                        break;
                }
            });
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void phoneTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
