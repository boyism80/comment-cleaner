using System;
using System.Windows;

namespace KPU_Comment_cleaner.Dialog
{
    /// <summary>
    /// Interaction logic for Logoutdialog.xaml
    /// </summary>
    public partial class Logoutdialog : Window
    {
        public event EventHandler OnLogout;
        public Logoutdialog()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.OnLogout?.Invoke(this, e);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
