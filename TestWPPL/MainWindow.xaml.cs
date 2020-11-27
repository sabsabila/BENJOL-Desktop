using System.Windows;
using TestWPPL.Login;
using Velacro.UIElements.Basic;

namespace TestWPPL {
    public partial class MainWindow : MyWindow {
        private MyPage loginPage;

        public MainWindow() {
            InitializeComponent();
            loginPage = new LoginPage();
            mainFrame.Navigate(loginPage);            
        }

    }
}
