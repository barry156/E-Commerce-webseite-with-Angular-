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

namespace Store_PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für Product.xaml
    /// </summary>
    public partial class Product : UserControl
    {

        MainWindow window;
        int productId;

        public Product(MainWindow window, string name, string price, string url, int productId)
        {
            InitializeComponent();
            this.window = window;
            lbl_name.Content = name;
            lbl_price.Content = price + " €";
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            string parentPath = directory.Name;
            string path = $"C:\\Users\\Maxim\\Desktop\\GitHub\\PIM-SAR_WiSe23_Gruppe14\\Images\\product-{productId}.jpg";
            img_product.Source = new BitmapImage(new Uri(path));
            this.productId = productId;
        }

        private void btn_add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window.addProductToCart(productId);
        }
    }
}
