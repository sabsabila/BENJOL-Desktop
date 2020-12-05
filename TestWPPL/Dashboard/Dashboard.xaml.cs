using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TestWPPL.Annotations;
using TestWPPL.Model;
using Velacro.Basic;
using Velacro.Chart.LineChart;
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
                telephone.Text = bengkel.phone_number;
                address.Text = bengkel.address;
                if (bengkel.profile_picture != null)
                    bengkelPicture.ImageSource = new BitmapImage(new Uri(ApiConstant.BASE_URL + bengkel.profile_picture));
            }));
        }

        private void getProfile()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("getProfile", token);
        }
    }
}
