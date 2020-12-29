using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Velacro.UIElements.Basic;
using TestWPPL.Model;
using System.IO;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using System.Globalization;

namespace TestWPPL.Sparepart
{
    public partial class SparepartPage : MyPage
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private IMyButton searchButton;
        private IMyButton addButton;
        private IMyTextBox searchTextBox;
        private CollectionView view;

        public SparepartPage()
        {
            InitializeComponent();
            setController(new SparepartController(this));
            initUIBuilders();
            initUIElements();
            getSparepart();
        }

        private void initUIElements()
        {
            searchButton = btnBuilder.activate(this, "searchBtn")
                            .addOnClick(this, "onSearchButtonClick");
            addButton = btnBuilder.activate(this, "addBtn")
                        .addOnClick(this, "onAddButtonClick");
            searchTextBox = txtBoxBuilder.activate(this, "searchBox");
        }

        public void onSearchButtonClick()
        {
            
        }

        public void onAddButtonClick()
        {
            this.NavigationService.Navigate(new AddSparepartPage());
        }

        private void initUIBuilders()
        {
            txtBoxBuilder = new BuilderTextBox();
            btnBuilder = new BuilderButton();
        }

        public void setSparepart(List<ModelSparepart> spareparts)
        {
            int id = 1;
            CultureInfo culture = new CultureInfo("id-ID");

            foreach (ModelSparepart sparepart in spareparts)
            {
                sparepart.num = id;
                sparepart.displayPrice = String.Format(culture, "Rp. {0:N}", sparepart.price);
                if (sparepart.picture == null)
                    sparepart.picture = "/image/image.png";
                else
                    sparepart.picture = ApiConstant.BASE_URL + sparepart.picture;
                id++;
            }
            
            this.Dispatcher.Invoke(() => {
                sparepartList.ItemsSource = spareparts;
                view = (CollectionView)CollectionViewSource.GetDefaultView(sparepartList.ItemsSource);
                view.Filter = UserFilter;
            });
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(searchTextBox.getText()))
                return true;
            else
                return ((item as ModelSparepart).name.IndexOf(searchTextBox.getText(), StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.sparepartList.ItemsSource).Refresh();
        }

        private void getSparepart()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestSpareparts", token);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            ModelSparepart dataObject = button.DataContext as ModelSparepart;
            this.NavigationService.Navigate(new EditSparepartPage(dataObject.sparepart_id));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
           
            Button button = sender as Button;
            ModelSparepart dataObject = button.DataContext as ModelSparepart;

            String token = File.ReadAllText(@"userToken.txt");

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item ?", "Delete Item", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    getController().callMethod("deleteSparepart", dataObject.sparepart_id, token);
                    break;
            }
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Delete Item", MessageBoxButton.OK, MessageBoxImage.Information);
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
