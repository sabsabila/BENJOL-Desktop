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
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Sparepart
{
    /// <summary>
    /// Interaction logic for EditSparepartPage.xaml
    /// </summary>
     
    public partial class EditSparepartPage : MyPage
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private IMyButton saveButton;
        private IMyButton backButton;
        private IMyButton uploadButton;
        private IMyTextBox nameTxtBox;
        private IMyTextBox priceTxtBox;
        private IMyTextBox stockTxtBox;
        private int sparepartId;

        public EditSparepartPage(int sparepartId)
        {
            InitializeComponent();
            this.sparepartId = sparepartId;
            Console.WriteLine("id yang dikirim : " + sparepartId);
            setController(new SparepartController(this));
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
            ObjectSparepart newSparepart = new ObjectSparepart(nameTxtBox.getText(), Int32.Parse(priceTxtBox.getText()), Int16.Parse(stockTxtBox.getText()));
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("editSparepart", newSparepart, sparepartId, token);
        }

        public void onUploadButtonClick()
        {
            Console.WriteLine("ini buat upload");
        }

        private void getEditedItem()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("showSparepart", sparepartId, token);
        }

        public void setItem(ModelSparepart sparepart)
        {
            this.Dispatcher.Invoke(() => {
                nameTxtBox.setText(sparepart.name);
                priceTxtBox.setText(sparepart.price.ToString());
                stockTxtBox.setText(sparepart.stock.ToString());
            });
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Edit Item", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new SparepartPage());
                        break;
                }
            });
        }
    }
}
