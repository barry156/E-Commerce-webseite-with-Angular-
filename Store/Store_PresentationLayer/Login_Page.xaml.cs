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
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class Login_Page : UserControl
    {
        private enum kindOfLogin
        {
            REGISTER,
            LOGIN
        }

        private kindOfLogin kindOfLogic = kindOfLogin.LOGIN;

        public Login_Page()
        {
            InitializeComponent();
        }

        private void btn_register_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (kindOfLogic)
            {
                case kindOfLogin.REGISTER:
                    switchToLogin();
                    break;
                case kindOfLogin.LOGIN:
                    switchToRegister();
                    break;
            }
        }

        private void btn_login_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txt_email.Text.Length > 0 && txt_password.Text.Length > 0)
            {
                bool success = false;
                switch (kindOfLogic)
                {
                    case kindOfLogin.REGISTER:
                        success = Requests.postRegister(txt_email.Text, txt_password.Text);
                        break;
                    case kindOfLogin.LOGIN:
                        success = Requests.postLogin(txt_email.Text, txt_password.Text);
                        break;
                }
                if (success)
                {

                }
            }
        }

        private void switchToLogin()
        {
            kindOfLogic = kindOfLogin.LOGIN;
            lbl_header.Content = "Einloggen";
            btn_login.Content = "Einloggen";
            btn_register.Content = "Registrieren";
        }

        private void switchToRegister()
        {
            kindOfLogic = kindOfLogin.REGISTER;
            lbl_header.Content = "Registrieren";
            btn_login.Content = "Registrieren";
            btn_register.Content = "Einloggen";
        }
    }
}
