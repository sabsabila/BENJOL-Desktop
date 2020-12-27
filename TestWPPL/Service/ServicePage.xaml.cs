using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TestWPPL.Model;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Service
{
    public partial class ServicePage : MyPage
    {
        
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton buttonBuilder;
        private IMyTextBox searchTxtBox;
        private IMyButton addServiceButton_btn;
        private IMyButton searchServiceButton_btn;
        private CollectionView view;

        public ServicePage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new ServiceController(this));
            initUIBuilders();
            initUIElements();
            getService();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            searchTxtBox = txtBoxBuilder.activate(this, "searchTextBox");
            addServiceButton_btn = buttonBuilder
                .activate(this, "addServiceButton")
                .addOnClick(this, "onAddServiceClick");
            searchServiceButton_btn = buttonBuilder
                .activate(this, "searchServiceButton")
                .addOnClick(this, "onSearchServiceClick");
        }

        public void onAddServiceClick()
        {
            var addDialog = new ServiceAddDialog();
            if (addDialog.ShowDialog() == true)
            {
                this.NavigationService.Navigate(new ServicePage());
            }
        }

        public void onSearchServiceClick()
        {
            view.Filter = UserFilter;
        }

        public void getService()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("getService", token);
        }

        public void setService(List<ModelService> services)
        {
            int id = 1;
            foreach (ModelService service in services)
            {
                service.num = id;
                id++;
            }

            this.Dispatcher.Invoke((Action)(() => {
                serviceList.ItemsSource = services;
                view = (CollectionView)CollectionViewSource.GetDefaultView(serviceList.ItemsSource);
                view.Filter = UserFilter;
            }));
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(searchTxtBox.getText()))
                return true;
            else
                return ((item as ModelService).service_name.IndexOf(searchTxtBox.getText(), StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.serviceList.ItemsSource).Refresh();
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new ServicePage());
                        break;
                }
            });
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelService dataObject = button.DataContext as ModelService;
            String token = File.ReadAllText(@"userToken.txt");

            MessageBoxResult result = MessageBox.Show("Are you sure want to perform this action?", "Delete Service", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    getController().callMethod("deleteService", dataObject.service_id, token);
                    break;
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelService dataObject = button.DataContext as ModelService;
            var editDialog = new ServiceEditDialog(dataObject);
            if (editDialog.ShowDialog() == true) {
                this.NavigationService.Navigate(new ServicePage());
            }

        }
            
    }
}
