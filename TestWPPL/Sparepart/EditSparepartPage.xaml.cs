using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private MyList<MyFile> uploadImage = new MyList<MyFile>();

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
            getController().callMethod("editSparepart",uploadImage, newSparepart, sparepartId, token);
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
                if (sparepart.picture != null)
                    picture.Source = new BitmapImage(new Uri(ApiConstant.BASE_URL + sparepart.picture));
            });
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void priceTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void stockTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as IMyTextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
