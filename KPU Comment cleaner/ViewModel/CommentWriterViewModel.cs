using KPU_Comment_cleaner.Command;
using KPU_Comment_cleaner.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace KPU_Comment_cleaner.ViewModel
{
    public class CommentWriterViewModel : BaseViewModel
    {
        public ConnectionViewModel ConnectionViewModel { get; private set; }

        public string NewsType { get; set; } = "kbaseball";

        private bool _isNewsLoading = false;
        public bool IsNewsLoading
        {
            get
            {
                return this._isNewsLoading;
            }
            private set
            {
                this._isNewsLoading = value;
                this.OnPropertyChanged(nameof(this.LoadButtonText));
            }
        }

        private bool _isCommentWriting = false;
        public bool IsCommentWriting
        {
            get
            {
                return this._isCommentWriting;
            }
            private set
            {
                this._isCommentWriting = value;
                this.OnPropertyChanged(nameof(this.WriteButtonText));
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.IsNewsLoading || this.IsCommentWriting;
            }
            set
            {
                if (value)
                    return;

                this.IsNewsLoading = false;
                this.IsCommentWriting = false;
            }
        }

        public string LoadButtonText
        {
            get
            {
                return this.IsNewsLoading ? "Stop" : "Load";
            }
        }

        public string WriteButtonText
        {
            get
            {
                return this.IsCommentWriting ? "Stop" : "Write";
            }
        }

        public DateTime StartDateTime { get; set; } = DateTime.Parse("2016-1-1");
        public DateTime EndDateTime { get; set; } = DateTime.Now;

        private bool _isWriteAfterLoading = false;
        public bool IsWriteAfterLoading
        {
            get
            {
                return this._isWriteAfterLoading;
            }
            private set
            {
                this._isWriteAfterLoading = value;
                this.OnPropertyChanged(nameof(this.WriteAfterLoadingText));
            }
        }
        public string WriteAfterLoadingText
        {
            get
            {
                return this.IsWriteAfterLoading ? "On" : "Off";
            }
        }

        public int WritingCount { get; private set; }
        public double WritingProgress
        {
            get
            {
                if (this.ConnectionViewModel.NewsItems.Count == 0)
                    return 0.0;

                return this.WritingCount * 100 / this.ConnectionViewModel.NewsItems.Count;
            }
        }
        public int RemainingSeconds { get; private set; }
        public string ResponseText { get; set; }

        public string CurrentMessage { get; set; }
        public ObservableCollection<string> MessageList { get; private set; } = new ObservableCollection<string>();

        public string RandomMessage
        {
            get
            {
                try
                {
                    var random = new Random();
                    var index = random.Next(this.MessageList.Count);

                    return this.MessageList[index];
                }
                catch (Exception)
                {
                    return "Hello World!";
                }
            }
        }
        public string LastestMessage { get; set; } = string.Empty;


        public ICommand UpdateNewsCommand { get; private set; }
        public ICommand WriteCommentCommand { get; private set; }
        public ICommand AddMessageCommand { get; private set; }
        public ICommand RemoveMessageCommand { get; private set; }

        public CommentWriterViewModel(ConnectionViewModel connectionViewModel)
        {
            this.ConnectionViewModel = connectionViewModel;

            this.MessageList.Add("안녕하세요 ^_^");
            this.MessageList.Add("한국산업기술대학교 화이팅!!");
            this.MessageList.Add("이 기사 너무 좋네요 잘 보고 갑니다.");
            this.MessageList.Add("아 배고파 죽겠다");
            this.MessageList.Add("나는 커서 뭐가 될까? 내 인생은 망했어.");
            this.MessageList.Add("평범한 보통 노멀사람이 되고 싶다.");

            this.UpdateNewsCommand = new RelayCommand(this.OnUpdateNews);
            this.WriteCommentCommand = new RelayCommand(this.OnWriteComment);
            this.AddMessageCommand = new RelayCommand(this.OnAddMessage);
            this.RemoveMessageCommand = new RelayCommand(this.OnRemoveMessage);
        }

        private void OnRemoveMessage(object obj)
        {
            var message = obj as string;
            this.MessageList.Remove(message);
        }

        private void OnAddMessage(object obj)
        {
            if (string.IsNullOrWhiteSpace(this.CurrentMessage))
                return;

            this.MessageList.Add(this.CurrentMessage);
            this.CurrentMessage = string.Empty;
        }

        private void OnWriteComment(object obj)
        {
            if (this.IsCommentWriting)
            {
                this.IsCommentWriting = false;
            }
            else
            {
                this.IsCommentWriting = true;
                this.WritingCount = 0;
                this.RemainingSeconds = 0;
                var thread = new Thread(new ThreadStart(this.CommentWritingThreadRoutine));
                thread.Start();
            }
        }

        private void CommentWritingThreadRoutine()
        {
            var index = 0;
            while (this.IsCommentWriting && this.ConnectionViewModel.NewsItems.Count > index)
            {
                var news = this.ConnectionViewModel.NewsItems[index++] as NewsViewModel;
                var message = this.RandomMessage;
                if (news.WriteComment(message) == false)
                    break;

                this.LastestMessage = message;
                this.WritingCount++;

                for (var i = 0; this.IsCommentWriting && i < 60 * 100; i++)
                {
                    this.RemainingSeconds = (60 * 100 - i) / 100;
                    Thread.Sleep(1000 / 100);
                }
            }

            this.IsCommentWriting = false;
        }

        private void OnUpdateNews(object obj)
        {
            if (this.IsNewsLoading)
            {
                this.IsNewsLoading = false;
            }
            else
            {
                this.IsNewsLoading = true;
                var thread = new Thread(new ThreadStart(this.UpdateNewsThreadRoutine));
                thread.Start();
            }
        }

        private void UpdateNewsThreadRoutine()
        {
            var datetime = this.StartDateTime;
            var now = DateTime.Now;

            this.ConnectionViewModel.NewsItems.Clear();

            while (this.IsNewsLoading)
            {
                var page = 1;
                while (this.IsNewsLoading && this.ConnectionViewModel.ReadNews("sports", this.NewsType, datetime, page++) > 0)
                { }

                datetime = datetime.AddDays(1);
                var difference = now - datetime;
                if (difference.TotalDays <= 0)
                    break;
            }

            this.IsNewsLoading = false;
            if (this.IsWriteAfterLoading)
                this.OnWriteComment(null);
        }
    }
}
