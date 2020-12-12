using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TestWPPL.Model;
using Velacro.Basic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Sparepart
{
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
        private MyList<MyFile> uploadImage = new MyList<MyFile>();

        public AddSparepartPage()
        {
            InitializeComponent();
            setController(new SparepartController(this));
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
            ObjectSparepart newSparepart = null;
            if (!nameTxtBox.getText().Equals("") && !priceTxtBox.getText().Equals("") && !stockTxtBox.getText().Equals(""))
            {
                newSparepart = new ObjectSparepart(nameTxtBox.getText(), Int32.Parse(priceTxtBox.getText()), Int16.Parse(stockTxtBox.getText()));
                String token = File.ReadAllText(@"userToken.txt");
                getController().callMethod("addSparepart", uploadImage, newSparepart, token);
            }
            else
                MessageBox.Show("Please fill in all fields before saving", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void onUploadButtonClick()
        {
            uploadImage.Clear();
            OpenFile openFile = new OpenFile();
            uploadImage.Add(openFile.openFile(false)[0]);
            picture.Source = new BitmapImage(new Uri(uploadImage[0].fullPath));
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Add Item", MessageBoxButton.OK, MessageBoxImage.Information);
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
