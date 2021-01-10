
using KPU_Comment_cleaner.Command;
using KPU_Comment_cleaner.Dialog;
using KPU_Comment_cleaner.ViewModel.Base;
using System;
using System.Windows.Input;

namespace KPU_Comment_cleaner.ViewModel
{
    public class MainWindowViewModel : BaseViewModel, IDisposable
    {
        private LoginDialog _loginDialog;
        private Logoutdialog _logoutDialog;

        public MainWindow MainWindow { get; private set; }
        public ConnectionViewModel ConnectionViewModel { get; private set; }
        public CommentCleanerViewModel CommentCleanerViewModel { get; private set; }
        public CommentWriterViewModel CommentWriterViewModel { get; private set; }
        public int TabSelected { get; set; } = 0;
        public bool DarkBackgroundVisibility { get; private set; }

        public ICommand SetMinimizeCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            //this.ConnectionViewModel.Login("boyism93", "tmdgus12");
            //this.ConnectionViewModel.ReadComments("sports", 0, 10);

            this.MainWindow = mainWindow;

            this.ConnectionViewModel = new ConnectionViewModel(this);
            this.ConnectionViewModel.CommentItems.Response += this.CommentItems_Response;
            this.ConnectionViewModel.NewsItems.Response += this.CommentItems_Response;
            this.ConnectionViewModel.Response += this.CommentItems_Response;
            this.CommentCleanerViewModel = new CommentCleanerViewModel(this.ConnectionViewModel);
            this.CommentWriterViewModel = new CommentWriterViewModel(this.ConnectionViewModel);

            this.SetMinimizeCommand = new RelayCommand(this.OnSetMinimize);
            this.CloseCommand = new RelayCommand(this.OnClose);
            this.LoginCommand = new RelayCommand(this.OnLogin);
        }

        ~MainWindowViewModel()
        {
            this.ConnectionViewModel.CommentItems.Response -= this.CommentItems_Response;
            this.ConnectionViewModel.NewsItems.Response -= this.CommentItems_Response;
            this.ConnectionViewModel.Response -= this.CommentItems_Response;
        }

        private void NewsItems_Response(object sender, bool result, string message)
        {
            
        }

        private void CommentItems_Response(object sender, bool result, string message)
        {
            if (sender == this.ConnectionViewModel.NewsItems)
            {
                this.CommentWriterViewModel.ResponseText = message;
            }
            else
            {
                this.CommentCleanerViewModel.ResponseText = message;
            }
        }

        private void OnLogin(object obj)
        {
            if (this.ConnectionViewModel.IsLogin)
            {
                if (this._logoutDialog != null)
                    return;

                
                //this.ConnectionViewModel.Logout();

                this.DarkBackgroundVisibility = true;
                this._logoutDialog = new Logoutdialog()
                {
                    Owner = this.MainWindow,
                };
                this._logoutDialog.OnLogout += this._logoutDialog_OnLogout;
                this._logoutDialog.Closed += this.LogoutDialog_Closed;
                this._logoutDialog.Show();
            }
            else
            {
                if(this._loginDialog != null)
                    return;

                this.DarkBackgroundVisibility = true;
                this._loginDialog = new LoginDialog()
                {
                    Owner = this.MainWindow,
                };
                this._loginDialog.RequestLogin += this._loginDialog_RequestLogin;
                this._loginDialog.Closed += this.LoginDialog_Closed;
                this._loginDialog.Show();
            }
        }

        private void _logoutDialog_OnLogout(object sender, EventArgs e)
        {
            this.ConnectionViewModel.Logout();
        }

        private void _loginDialog_RequestLogin(string id, string pw)
        {
            if (this.ConnectionViewModel.Login(id, pw) == false)
                throw new Exception("Invalid ID or password.");
        }

        private void LogoutDialog_Closed(object sender, EventArgs e)
        {
            this.DarkBackgroundVisibility = false;
            this._logoutDialog = null;
        }

        private void LoginDialog_Closed(object sender, EventArgs e)
        {
            this.DarkBackgroundVisibility = false;
            this._loginDialog = null;
        }

        public void OnSetMinimize(object parameter)
        {
            this.MainWindow.WindowState = System.Windows.WindowState.Minimized;
        }

        public void OnClose(object parameter)
        {
            this.MainWindow.Close();
        }

        public void Dispose()
        {
            this.CommentCleanerViewModel.IsRunning = false;
            this.CommentWriterViewModel.IsRunning = false;
        }
    }
}
