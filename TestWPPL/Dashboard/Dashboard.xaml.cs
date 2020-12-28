using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TestWPPL.Model;
using TestWPPL.Model.TestWPPL.Model;
using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;

namespace TestWPPL.Dashboard {

    public partial class Dashboard : MyPage{

        private BuilderTextBlock txtBlockBuilder;
        private IMyTextBlock bengkelNameTxtBlock;
        private IMyTextBlock bengkelEmailTxtBlock;
        private IMyTextBlock bengkelTelephoneTxtBlock;
        private IMyTextBlock bengkelAddressTxtBlock;
        private IMyTextBlock finishedTxtBlock;
        private IMyTextBlock canceledTxtBlock;
        private IMyTextBlock upcomingTxtBlock;
        private IMyTextBlock revenueTxtBlock;
        private IMyTextBlock unpaidTxtBlock;
        private IMyTextBlock pendingTxtBlock;

        public Dashboard() {
            InitializeComponent();
            setController(new DashboardController(this));
            initUIBuilders();
            initUIElements();
            getProfile();
            getStatictics();
        }

        private void getStatictics()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("getBookings", "finished", token);
            getController().callMethod("getBookings", "upcoming", token);
            getController().callMethod("getBookings", "canceled", token);
            getController().callMethod("getRevenue", "paid", token);
            getController().callMethod("getRevenue", "unpaid", token);
            getController().callMethod("getRevenue", "pending", token);
        }

        private void initUIBuilders()
        {
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void initUIElements()
        {
            bengkelNameTxtBlock = txtBlockBuilder.activate(this, "bengkelName");
            bengkelAddressTxtBlock = txtBlockBuilder.activate(this, "address");
            bengkelEmailTxtBlock = txtBlockBuilder.activate(this, "email");
            bengkelTelephoneTxtBlock = txtBlockBuilder.activate(this, "telephone");
            finishedTxtBlock = txtBlockBuilder.activate(this, "bookingsDone"); 
            canceledTxtBlock = txtBlockBuilder.activate(this, "bookingsCanceled");
            upcomingTxtBlock = txtBlockBuilder.activate(this, "bookingsUpcoming");
            revenueTxtBlock = txtBlockBuilder.activate(this, "totalRevenue");
            unpaidTxtBlock = txtBlockBuilder.activate(this, "unpaidServices");
            pendingTxtBlock = txtBlockBuilder.activate(this, "pendingRevenue");
        }

        public void setProfile(ModelBengkel bengkel)
        {
            this.Dispatcher.Invoke((Action)(() => {
                bengkelNameTxtBlock.setText(bengkel.name);
                bengkelEmailTxtBlock.setText(bengkel.email);
                if (bengkel.phone_number != null)
                    bengkelTelephoneTxtBlock.setText("+" + bengkel.phone_number);
                else
                    bengkelTelephoneTxtBlock.setText("-");
                bengkelAddressTxtBlock.setText(bengkel.address);
                if (bengkel.profile_picture != null)
                    bengkelPicture.ImageSource = new BitmapImage(new Uri(ApiConstant.BASE_URL + bengkel.profile_picture));
            }));
        }

        public void setDone(ModelBookingCount count)
        {
            this.Dispatcher.Invoke((Action)(() => {
                if (count == null)
                    finishedTxtBlock.setText("0");
                else
                    finishedTxtBlock.setText(count.booking_count.ToString());
            }));
        }

        public void setCanceled(ModelBookingCount count)
        {
            this.Dispatcher.Invoke((Action)(() => {
                if (count == null)
                    canceledTxtBlock.setText("0");
                else
                    canceledTxtBlock.setText(count.booking_count.ToString());
            }));
        }

        public void setUpcoming(ModelBookingCount count)
        {
            this.Dispatcher.Invoke((Action)(() => {
                if (count == null)
                    upcomingTxtBlock.setText("0");
                else
                    upcomingTxtBlock.setText(count.booking_count.ToString());
            }));
        }

        public void setRevenue(ModelRevenueCount count)
        {
            this.Dispatcher.Invoke((Action)(() => {
                if (count == null)
                    revenueTxtBlock.setText("0");
                else
                    revenueTxtBlock.setText(count.revenue_count.ToString());
            }));
        }

        public void setUnpaidRevenue(ModelRevenueCount count)
        {
            this.Dispatcher.Invoke((Action)(() => {
                if (count == null)
                    unpaidTxtBlock.setText("0");
                else
                    unpaidTxtBlock.setText(count.revenue_count.ToString());
            }));
        }
        public void setPendingRevenue(ModelRevenueCount count)
        {
            this.Dispatcher.Invoke((Action)(() => {
                if (count == null)
                    pendingTxtBlock.setText("0");
                else
                    pendingTxtBlock.setText(count.revenue_count.ToString());
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
