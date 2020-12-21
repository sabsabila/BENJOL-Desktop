using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TestWPPL.Model;
using Velacro.UIElements.Basic;

namespace TestWPPL.Dashboard {

    public partial class Dashboard : MyPage{
        public Dashboard() {
            InitializeComponent();
            setController(new DashboardController(this));
            getProfile();
        }

        public void setProfile(ModelBengkel bengkel)
        {
            this.Dispatcher.Invoke((Action)(() => {
                bengkelName.Text = bengkel.name;
                email.Text = bengkel.email;
                if (bengkel.phone_number != null)
                    telephone.Text = "+" + bengkel.phone_number;
                else
                    telephone.Text = "-";
                address.Text = bengkel.address;
                if (bengkel.profile_picture != null)
                    bengkelPicture.ImageSource = new BitmapImage(new Uri(ApiConstant.BASE_URL + bengkel.profile_picture));
            }));
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void getProfile()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("getProfile", token);
        }
    }
}
