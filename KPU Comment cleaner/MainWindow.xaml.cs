using KPU_Comment_cleaner.ViewModel;
using System;
using System.Windows;

namespace KPU_Comment_cleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            this.MainWindowViewModel = new MainWindowViewModel(this);
            this.DataContext = this.MainWindowViewModel;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.MainWindowViewModel.Dispose();
        }
    }
}
