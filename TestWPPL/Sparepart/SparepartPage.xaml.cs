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
using TestWPPL.Model;
using System.IO;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Sparepart
{
    /// <summary>
    /// Interaction logic for SparepartPage.xaml
    /// </summary>
    public partial class SparepartPage : MyPage
    {
        private BuilderTextBox txtBoxBuilder;
        private BuilderButton btnBuilder;
        private IMyButton searchButton;
        private IMyButton addButton;
        private IMyTextBox searchTextBox;

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
            Console.WriteLine("ini buat search");
        }

        public void onAddButtonClick()
        {
            Console.WriteLine("ini buat add");
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
            foreach (ModelSparepart sparepart in spareparts)
            {
                sparepart.sparepart_id = id;
                id++;
            }
            this.Dispatcher.Invoke(() => {
                sparepartList.ItemsSource = spareparts;
            });
        }

        private void getSparepart()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestSpareparts", token);
        }
    }
}
