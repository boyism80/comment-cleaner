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
using System.Windows.Shapes;

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
