using System.Windows.Controls;
using System.Windows.Input;

namespace Store_PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class Login_Page : UserControl
    {

        MainWindow main;

        private enum kindOfLogin
        {
            REGISTER,
            LOGIN
        }

        private kindOfLogin kindOfLogic = kindOfLogin.LOGIN;

        public Login_Page(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
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
                int id = -1;
                switch (kindOfLogic)
                {
                    case kindOfLogin.REGISTER:
                        id = Requests.postRegisterRequest(txt_email.Text, txt_password.Text);
                        break;
                    case kindOfLogin.LOGIN:
                        id = Requests.postLoginRequest(txt_email.Text, txt_password.Text);
                        break;
                }
                if (id >= 0)
                {
                    main.EMail = txt_email.Text;
                    main.UserId = id;
                    main.switchToStore();
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
