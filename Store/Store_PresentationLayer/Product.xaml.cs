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
            lbl_price.Content = price;
            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(url, UriKind.Relative);
            bimage.EndInit();
            img_product.Source = bimage;
            this.productId = productId;
        }

        private void btn_add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window.addProductToCart(productId);
        }
    }
}
