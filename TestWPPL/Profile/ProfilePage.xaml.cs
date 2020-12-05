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
using TestWPPL.Model;
using Velacro.Basic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;


namespace TestWPPL.Profile
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : MyPage
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private IMyButton updateButton;
        private IMyButton uploadButton;
        private IMyButton logoutButton;
        private IMyTextBox nameTxtBox;
        private IMyTextBox phoneTxtBox;
        private IMyTextBox emailTxtBox;
        private IMyTextBox addressTxtBox;
        private int sparepartId;
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
        }

        private void getEditedItem()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("getProfile", token);
        }

        public void onUpdateButtonClick()
        {
            ObjectBengkel newBengkel = new ObjectBengkel(nameTxtBox.getText(), phoneTxtBox.getText(), emailTxtBox.getText(), addressTxtBox.getText());
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("editProfile", uploadImage, newBengkel, token);
        }

        public void onUploadButtonClick()
        {
            uploadImage.Clear();
            Console.WriteLine("ini buat upload");
            OpenFile openFile = new OpenFile();
            uploadImage.Add(openFile.openFile(false)[0]);
            picture.Source = new BitmapImage(new Uri(uploadImage[0].fullPath));
            Console.WriteLine("panjangnya upload image list : " + uploadImage.Count);
        }

        public void onLogoutButtonClick()
        {
            String token = File.ReadAllText(@"userToken.txt");
            MessageBoxResult result = MessageBox.Show("Are you sure ?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    getController().callMethod("requestLogout", token);
                    break;
            }
        }

        public void setProfile(ModelBengkel bengkel)
        {
            this.Dispatcher.Invoke(() => {
                nameTxtBox.setText(bengkel.name);
                phoneTxtBox.setText(bengkel.phone_number);
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

        public void setLogoutStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Logout Success", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        string fullPath = @"userToken.txt";
                        File.WriteAllText(fullPath, "");
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        Window.GetWindow(this).Close();
                        break;
                }
            });
        }
    }
}
