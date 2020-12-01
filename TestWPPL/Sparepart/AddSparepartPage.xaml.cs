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
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Sparepart
{
    /// <summary>
    /// Interaction logic for AddSparepartPage.xaml
    /// </summary>
    public partial class AddSparepartPage : MyPage
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private IMyButton saveButton;
        private IMyButton backButton;
        private IMyButton uploadButton;
        private IMyTextBox nameTxtBox;
        private IMyTextBox priceTxtBox;
        private IMyTextBox stockTxtBox;

        public AddSparepartPage()
        {
            InitializeComponent();
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            txtBoxBuilder = new BuilderTextBox();
            btnBuilder = new BuilderButton();
        }

        private void initUIElements()
        {
            saveButton = btnBuilder.activate(this, "backBtn")
                            .addOnClick(this, "onBackButtonClick");
            backButton = btnBuilder.activate(this, "saveBtn")
                        .addOnClick(this, "onSaveButtonClick");
            uploadButton = btnBuilder.activate(this, "uploadBtn")
                            .addOnClick(this, "onUploadButtonClick");
            nameTxtBox = txtBoxBuilder.activate(this, "nameTxt");
            priceTxtBox = txtBoxBuilder.activate(this, "priceTxt");
            stockTxtBox = txtBoxBuilder.activate(this, "stockTxt");
        }

        public void onBackButtonClick()
        {
            this.NavigationService.Navigate(new SparepartPage());
        }

        public void onSaveButtonClick()
        {
            Console.WriteLine("ini buat save");
        }

        public void onUploadButtonClick()
        {
            Console.WriteLine("ini buat upload");
        }
    }
}
