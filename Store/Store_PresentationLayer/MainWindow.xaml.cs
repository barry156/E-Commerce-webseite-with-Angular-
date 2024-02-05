using Store_ApplicationLayer.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using static Store_PresentationLayer.MainWindow;

namespace Store_PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string email = string.Empty;
        private int userId = -1;
        private KindOfWindow kindOfWindow = MainWindow.KindOfWindow.LOGIN;


        public MainWindow()
        {
            InitializeComponent();
            EMail = string.Empty;
        }

        public void switchToStore()
        {
            kindOfWindow = KindOfWindow.STORE;
            List<Model_Product> products = Requests.getProductsRequest();
            Store_Page storePage = new(this, products);
            content_control.Content = storePage;
        }

        public void switchToCart()
        {
            kindOfWindow = KindOfWindow.CART;
            Model_Cart cart = Requests.getOrderRequest(userId);
            Cart_Page cartPage = new(this, cart);
            content_control.Content = cartPage;
        }

        public void addProductToCart(int productId)
        {
            Requests.addProductRequest(userId, productId);
            if (kindOfWindow == KindOfWindow.CART)
            {
                Cart_Page cartPage = (Cart_Page)content_control.Content;
                cartPage.initFill(Requests.getOrderRequest(userId));
            }
        }

        public void removeProductToCart(int productId)
        {
            Requests.deleteProductRequest(userId, productId);
            if (kindOfWindow == KindOfWindow.CART)
            {
                Cart_Page cartPage = (Cart_Page)content_control.Content;
                cartPage.initFill(Requests.getOrderRequest(userId));
            }
        }

        public void removeWholeProductToCart(int productId)
        {
            Requests.deleteWholeProductRequest(userId, productId);
            if (kindOfWindow == KindOfWindow.CART)
            {
                Cart_Page cartPage = (Cart_Page)content_control.Content;
                cartPage.initFill(Requests.getOrderRequest(userId));
            }
        }

        public string EMail
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                if (email != string.Empty)
                {
                    lbl_email.Content = email;
                    btn_cart.IsEnabled = true;
                    btn_logout.IsEnabled = true;
                    btn_shop.IsEnabled = true;
                }
                else
                {
                    btn_cart.IsEnabled = false;
                    btn_logout.IsEnabled = false;
                    btn_shop.IsEnabled = false;

                    lbl_email.Content = "";
                    Login_Page loginPage = new(this);
                    content_control.Content = loginPage;
                }
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        private void btn_shop_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switchToStore();
        }

        private void btn_cart_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switchToCart();
        }

        private void btn_logout_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EMail = string.Empty;
        }

        public enum KindOfWindow
        {
            LOGIN,
            STORE,
            CART
        }
    }
}