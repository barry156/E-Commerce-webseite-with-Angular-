using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string email = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            Login_Page loginPage = new();
            content_control.Content = loginPage;
        }

        public void switchToStore()
        {
            Store_Page storePage = new();
            content_control.Content = storePage;
        }

        public void switchToCart()
        {
            Cart_Page cartPage = new();
            content_control.Content = cartPage;
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
                }
                else
                {
                    lbl_email.Content = "";
                    Login_Page loginPage = new();
                    content_control.Content = loginPage;
                }
            }
        }
    }
}