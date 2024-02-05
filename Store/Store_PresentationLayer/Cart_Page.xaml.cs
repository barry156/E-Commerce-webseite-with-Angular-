using Store_ApplicationLayer.Models;
using System.Windows.Controls;
using System.Windows.Input;

namespace Store_PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class Cart_Page : UserControl
    {

        MainWindow main;

        public Cart_Page(MainWindow main, Model_Cart cart)
        {
            InitializeComponent();
            this.main = main;
            initFill(cart);
        }

        public void initFill(Model_Cart cart)
        {
            foreach (Model_Product product in cart.products)
            {
                if (product != null)
                {
                    stackPanel.Children.Add(new Cart_Product(main, product.name, product.price.ToString(), product.amount, product.id));
                }
            }
            lbl_wholePrice.Content = $"Insgesamt: {cart.total_price}€";
        }
    }
}
