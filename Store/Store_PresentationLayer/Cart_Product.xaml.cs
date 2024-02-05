using System.Windows.Controls;
using System.Windows.Input;

namespace Store_PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für Product.xaml
    /// </summary>
    public partial class Cart_Product : UserControl
    {

        MainWindow window;
        int productId;

        public Cart_Product(MainWindow window, string name, string price, int amount, int productId)
        {
            InitializeComponent();
            this.window = window;
            lbl_name.Content = name;
            lbl_price.Content = price;
            lbl_amount.Content = amount;
            this.productId = productId;
        }

        private void btn_add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window.addProductToCart(productId);
        }

        private void btn_removeWhole_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window.removeWholeProductToCart(productId);
        }

        private void btn_remove_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window.removeProductToCart(productId);
        }
    }
}
