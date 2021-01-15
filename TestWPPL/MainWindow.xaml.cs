using System;
using System.IO;
using System.Windows;
using TestWPPL.Dashboard;
using TestWPPL.Login;
using Velacro.UIElements.Basic;
using Newtonsoft.Json;

namespace TestWPPL {
    public partial class MainWindow : MyWindow {
        private MyPage loginPage;

        public MainWindow() {
            InitializeComponent();
            String token = File.ReadAllText(@"userToken.txt");
            if (!token.Equals(""))
            {
                new BenjolWindow().Show();
                this.Close();
            }
            else
            {
                loginPage = new LoginPage();
                mainFrame.Navigate(loginPage);
            }            
        }

        

    }
}
