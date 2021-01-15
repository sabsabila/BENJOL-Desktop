using System;
using System.Collections.Generic;
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
using Velacro.UIElements.Basic;


namespace TestWPPL.AboutUs
{
    /// <summary>
    /// Interaction logic for AboutUsPage.xaml
    /// </summary>
    public partial class AboutUsPage : MyPage
    {
        public AboutUsPage()
        {
            InitializeComponent();
            initUIBuilders();
            initUIElements();
        }


        private void initUIBuilders()
        {
            builderButton = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            refreshButton = builderButton
                .activate(this, "refreshBtn")
                .addOnClick(this, "onRefreshButtonClick");
            searchTextBox = txtBoxBuilder.activate(this, "searchBox");
        }

    }
}
