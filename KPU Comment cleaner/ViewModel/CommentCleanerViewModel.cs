using KPU_Comment_cleaner.Command;
using KPU_Comment_cleaner.ViewModel.Base;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace KPU_Comment_cleaner.ViewModel
{
    public class CommentCleanerViewModel : BaseViewModel
    {
        public ConnectionViewModel ConnectionViewModel { get; private set; }

        public string NewsType { get; set; } = "news";
        public string LoadButtonText
        {
            get
            {
                return this.IsCommentLoading ? "Stop" : "Load";
            }
        }
        public string ClearButtonText
        {
            get
            {
                return this.IsCommentRemoving ? "Stop" : "Clear";
            }
        }

        private bool _isCommentLoading = false;
        public bool IsCommentLoading
        {
            get
            {
                return this._isCommentLoading;
            }
            private set
            {
                this._isCommentLoading = value;
                this.OnPropertyChanged(nameof(this.LoadButtonText));
            }
        }

        private bool _isCommentRemoving = false;
        public bool IsCommentRemoving
        {
            get
            {
                return this._isCommentRemoving;
            }
            private set
            {
                this._isCommentRemoving = value;
                this.OnPropertyChanged(nameof(this.ClearButtonText));
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.IsCommentLoading || this.IsCommentRemoving;
            }
            set
            {
                if (value)
                    return;

                this.IsCommentLoading = false;
                this.IsCommentRemoving = false;
            }
        }

        private bool _isCleanUpAfterLoading = false;
        public bool IsCleanUpAfterLoading
        {
            get
            {
                return this._isCleanUpAfterLoading;
            }
            private set
            {
                this._isCleanUpAfterLoading = value;
                this.OnPropertyChanged(nameof(this.CleanUpAfterLoadingText));
            }
        }
        public string CleanUpAfterLoadingText
        {
            get
            {
                return this.IsCleanUpAfterLoading ? "On" : "Off";
            }
        }

        public int RemoveCount { get; private set; } = 0;
        public double RemoveProgress
        {
            get
            {
                if (this.ConnectionViewModel.TotalCommentCount == 0)
                    return 0.0;

                return this.RemoveCount * 100 / (double)this.ConnectionViewModel.TotalCommentCount;
            }
        }
        public string ResponseText { get; set; }

        public ICommand UpdateCommentCommand { get; private set; }
        public ICommand ClearCommentCommand { get; private set; }
        public ICommand CleanUpAfterLoadingCommand { get; private set; }

        public CommentCleanerViewModel(ConnectionViewModel connectionViewModel)
        {
            this.ConnectionViewModel = connectionViewModel;

            this.UpdateCommentCommand = new RelayCommand(this.OnUpdateCommand);
            this.ClearCommentCommand = new RelayCommand(this.OnClearCommand);
            this.CleanUpAfterLoadingCommand = new RelayCommand(this.OnCleanUpAfterLoading);
        }

        private void OnUpdateCommand(object obj)
        {
            if (this.IsCommentLoading)
            {
                this.IsCommentLoading = false;
            }
            else
            {
                this.IsCommentLoading = true;
                var thread = new Thread(new ThreadStart(this.UpdateCommentThreadRoutine));
                thread.Start();
            }
        }

        private void UpdateCommentThreadRoutine()
        {
            var items = this.ConnectionViewModel.CommentItems.ToArray();
            foreach (var item in items)
            {
                if (this.IsCommentLoading == false)
                    return;

                this.ConnectionViewModel.CommentItems.Remove(item);
            }

            var page = 1;
            while (this.ConnectionViewModel.ReadComments(this.NewsType, page++, 10) > 0) { }
            this.IsCommentLoading = false;

            if (this.IsCleanUpAfterLoading)
                this.OnClearCommand(null);
        }

        private void OnClearCommand(object obj)
        {
            if (this.IsCommentRemoving)
            {
                this.IsCommentRemoving = false;
            }
            else
            {
                this.RemoveCount = 0;
                this.IsCommentRemoving = true;

                var thread = new Thread(new ThreadStart(this.DeleteCommentThreadRoutine));
                thread.Start();
            }
        }

        private void DeleteCommentThreadRoutine()
        {
            while (this.IsCommentRemoving && this.ConnectionViewModel.CommentItems.Count != 0)
            {
                var comment = this.ConnectionViewModel.CommentItems[0] as CommentViewModel;
                this.ConnectionViewModel.CommentItems.Remove(comment);

                this.ConnectionViewModel.CommentItems.Delete(comment as CommentViewModel);
                this.RemoveCount++;

                this.OnPropertyChanged(nameof(this.RemoveProgress));
            }
            this.IsCommentRemoving = false;

            //if (index == 0)
            //{
            //    this.MainWindow.Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        this.MainWindow.Close();
            //    }));
            //}
        }

        private void OnCleanUpAfterLoading(object obj)
        {
            this.IsCleanUpAfterLoading = !this.IsCleanUpAfterLoading;
        }
    }
}
