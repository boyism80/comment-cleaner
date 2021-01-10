using System;
using System.Windows;

namespace KPU_Comment_cleaner.Dialog
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public delegate void RequestLoginEventHandler(string id, string pw);

        public event RequestLoginEventHandler RequestLogin;

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.id.Text))
                    throw new Exception("ID is empty");

                if (string.IsNullOrWhiteSpace(this.pw.Password))
                    throw new Exception("Password is empty");

                this.RequestLogin?.Invoke(this.id.Text, this.pw.Password);
                this.Close();
            }
            catch (Exception exc)
            {
                this.exception.Text = exc.Message;
            }
        }
    }
}
