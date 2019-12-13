using SimpleJSON;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WebAccessor
{
    public partial class MainForm : Form
    {
        private int _currentDeletedCount;
        private int _destDeleteCount;
        private int _beginDeleteTime;

        private Thread _updateCommentsThread;
        private Thread[] _deleteCommentsThreads;
        private Thread _updateNewsThread, _autoCommentsThread;
        private Semaphore _semaphore = new Semaphore(1, 1);

        private bool _isProcessingComments = false;
        private bool _isProcessingNews = false;

        public bool IsProcessingComments
        {
            get
            {
                return this._isProcessingComments;
            }
            set
            {
                if(value == true)
                    this.commentLogBox.Text = string.Empty;

                this._isProcessingComments = value;
                this.enableProcessComments(!value);
                this.enableUnprocessComments(value);
            }
        }

        public bool IsProcessingNews
        {
            get
            {
                return this._isProcessingNews;
            }
            set
            {
                this._isProcessingNews = value;
                this.enableProcessNews(!value);
                this.enableUnprocessNews(value);
            }
        }

        public string CommentState
        {
            get
            {
                return this.commentStateLabel.Text;
            }
            set
            {
                this.setCommentState(value);
            }
        }

        public string NewsState
        {
            get
            {
                return this.newsStateLabel.Text;
            }
            set
            {
                this.setNewsState(value);
            }
        }

        public string CommentLog
        {
            set
            {
                this.setCommentLogBox(value);
            }
        }

        public string NewsLog
        {
            set
            {
                this.setNewsLogBox(value);
            }
        }

        public MainForm()
        {
            InitializeComponent();

            var signinForm = new SigninForm();
            signinForm.ShowDialog(this);
            if(signinForm.DialogResult != DialogResult.OK)
                Environment.Exit(0);


            // Initialize combo box
            this.newsTypeComboBox.DisplayMember = "Text";
            this.newsTypeComboBox.ValueMember = "Value";
            this.newsTypeComboBox.Items.Add(new { Text = "일반", Value="news" });
            this.newsTypeComboBox.Items.Add(new { Text = "스포츠", Value="sports" });
            this.newsTypeComboBox.SelectedIndex = 0;

            this.loadNewsTypeComboBox.DisplayMember = "Text";
            this.loadNewsTypeComboBox.ValueMember = "Value";
            this.loadNewsTypeComboBox.Items.Add(new { Text = "야구", Value="kbaseball" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "해외야구", Value="wbaseball" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "축구", Value="kfootball" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "해외축구", Value="wfootball" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "농구", Value="basketball" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "배구", Value="volleyball" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "골프", Value="golf" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "일반", Value="general" });
            this.loadNewsTypeComboBox.Items.Add(new { Text = "e스포츠&게임", Value="esports" });
            this.loadNewsTypeComboBox.SelectedIndex = 0;
        }

        public void updateNews(object param)
        {
            var current = this.newsBeginDatePicker.Value;
            var end = this.newsEndDatePicker.Value;
            var maximumPage = 0;
            var count = 0;
            var type = param as string;

            if(DateTime.Compare(current, end) > 0)
                return;

            this.enableNewsSettingPanel(false);
            this.clearNewsTable();
            this.setNewsProgressBar(0);
            this.NewsLog = "전체 페이지 계산중..";

            while(this.IsProcessingNews && DateTime.Compare(current, end) <= 0)
            {
                var date = current.ToString("yyyyMMdd");
                this.NewsState = string.Format("{0} 날짜 뉴스 페이지 계산중..", date);
                maximumPage += Naver.Instance.page("sports", type, date);

                current = current.AddDays(1);
            }
            this.setMaximumNewsProgressBar(maximumPage);


            this.NewsLog = "뉴스를 읽어들이는 중..";
            current = this.newsBeginDatePicker.Value;
            while(this.IsProcessingNews && DateTime.Compare(current, end) <= 0)
            {
                var date = current.ToString("yyyyMMdd");
                var page = Naver.Instance.page("sports", type, date);

                for(int i = 1; i <= page; i++)
                {
                    var news = Naver.Instance.news("sports", type, date, i);
                    count += news["list"].AsArray.Count;
                    foreach(JSONNode newsElement in news["list"].AsArray)
                    {
                        this.NewsState = string.Format("{0}개) {1}를 불러왔습니다.", count, newsElement["title"].Value);
                        this.addNews(newsElement);
                    }

                    this.addNewsProgressBar(1);
                }

                current = current.AddDays(1);
            }

            this.NewsLog = this.NewsState = string.Format("작업을 완료했습니다. (총 {0}개)", count);
            this.enableNewsSettingPanel(true);
            this.IsProcessingNews = false;
            this._updateNewsThread = null;
        }

        public void autoComment()
        {
            if(this.newsTable.Rows.Count == 0)
                return;

            this.enableNewsSettingPanel(false);
            this.setNewsProgressBar(0);
            this.setMaximumNewsProgressBar(this.newsTable.Rows.Count);

            var commentContents = this.commentContentTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);

            foreach(DataGridViewRow row in this.newsTable.Rows)
            {
                if(this.IsProcessingNews == false)
                    break;

                var news = row.Tag as JSONNode;
                var randomComment = commentContents[random.Next(commentContents.Length)];

                var response = Naver.Instance.addComment("sports", string.Empty, news, randomComment);
                this.NewsLog = string.Format("{0}에 댓글 작성 : {1}", news["title"], randomComment);

                this.addNewsProgressBar(1);
                for(var i = 0; (this.newsProgressBar.Value != this.newsProgressBar.Maximum) && i < 60; i++)
                {
                    if(this.IsProcessingNews == false)
                        break;

                    this.NewsState = string.Format("{0}초 후에 댓글을 달 수 있습니다. (댓글 등록 수 : {1})", 60 - i, this.newsProgressBar.Value);
                    Thread.Sleep(1000);
                }
            }

            this.NewsState = "작업을 완료했습니다.";
            this.enableNewsSettingPanel(true);
            this.IsProcessingNews = false;
            this._autoCommentsThread = null;
        }

        private void deleteCompleteRoutine()
        {
            this.setVisibilityProgressBar(true);

            foreach(var deleteThread in this._deleteCommentsThreads)
                deleteThread.Join();

            this.IsProcessingComments = false;
            this._deleteCommentsThreads = null;

            var elapsedTime = (Environment.TickCount - this._beginDeleteTime) / 1000.0f;
            this.CommentState = string.Format("작업을 완료했습니다. ({0})초", elapsedTime.ToString("0.00"));

            this.setVisibilityProgressBar(false);
        }

        public void deleteComments()
        {
            var elapsedTime = 0.0f;
            while (this._isProcessingComments)
            {
                try
                {

this._semaphore.WaitOne();
                    if(this.commentTable.Rows.Count == 0 || (this._destDeleteCount != -1 && this._currentDeletedCount >= this._destDeleteCount))
                    {
this._semaphore.Release();
                        break;
                    }

                    var target = this.commentTable.Rows[0];
                    var comment = target.Tag as JSONNode;

                    var commentNo = comment["commentNo"].AsInt;
                    this.CommentLog = string.Format("Thread id : {0} 댓글 삭제중..(no : {1}, count : {2})", Thread.CurrentThread.ManagedThreadId, commentNo, this._currentDeletedCount);

                    this._currentDeletedCount++;
                    this.deleteComment(target);
                    this.setCommentsProgressBar(this._currentDeletedCount);
this._semaphore.Release();

                    if (Naver.Instance.deleteComment(comment) == false)
                        throw new Exception(string.Format("{0} 댓글을 삭제할 수 없습니다.", comment["contents"].Value));

                    elapsedTime = (Environment.TickCount - this._beginDeleteTime) / 1000.0f;
                    //this.CommentState = string.Format("{0} ) {1} : 삭제완료", elapsedTime.ToString("0.00"), comment["contents"].Value);
                }
                catch (Exception exc)
                {
                    this.CommentLog = exc.Message;
                }
            }
        }

        public void updateComments(object param)
        {
            var ticket = param as string;

            this.clearCommentTable();
            this.setVisibilityProgressBar(true);
            this.initProgressBar();
            this.setMaximumCommentsProgressBar(Naver.Instance.count(ticket));

            var page = 1;
            var readSize = 10;

            while (this._isProcessingComments)
            {
                try
                {
                    var json = Naver.Instance.comments(ticket, page++, readSize);
                    if (json == null)
                        throw new Exception(string.Format("서버로부터 {0} 페이지를 얻어오지 못했습니다.", page - 1));

                    if (json["success"].AsBool == false)
                        throw new Exception(json["message"].Value);

                    if(json["result"]["count"]["total"].AsInt == 0)
                        break;

                    var commentList = json["result"]["commentList"].AsArray;
                    foreach (JSONNode comment in commentList)
                        this.addComment(comment);

                    if (json["result"]["pageModel"]["endRow"].AsInt == json["result"]["pageModel"]["totalRows"].AsInt)
                        break;
                }
                catch(Exception e)
                {
                    this.CommentLog = e.Message;
                    continue;
                }
            }

            this.setVisibilityProgressBar(false);
            this.CommentState = "작업을 완료했습니다.";
            this.setEnableUpdateButton(true);
            
            this.IsProcessingComments = false;
            this._updateCommentsThread = null;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if(this.IsProcessingComments)
                return;

            this.totalCommentsLabel.Text = "0개";
            this.currentCommentsLabel.Text = "0개";

            var ticket = this.newsTypeComboBox.SelectedItem.GetType().GetProperty("Value").GetValue(this.newsTypeComboBox.SelectedItem) as string;

            this.IsProcessingComments = true;
            this._updateCommentsThread = new Thread(new ParameterizedThreadStart(this.updateComments));
            this._updateCommentsThread.Start(ticket);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsProcessingComments)
                    return;
                
                var threadCount = int.Parse(this.threadCountTextBox.Text);
                this._currentDeletedCount = 0;
                this._destDeleteCount = this.deleteCountTextBox.Text.Length == 0 ? -1 : int.Parse(this.deleteCountTextBox.Text);
                this._beginDeleteTime = Environment.TickCount;

                if (this._deleteCommentsThreads != null)
                {
                    foreach (var deleteThread in this._deleteCommentsThreads)
                        deleteThread.Join();
                }


                this.commentsProgressBar.Maximum = this._destDeleteCount != -1 ? this._destDeleteCount : this.commentTable.Rows.Count;
                this.IsProcessingComments = true;
                this._deleteCommentsThreads = new Thread[threadCount];
                for (var i = 0; i < threadCount; i++)
                {
                    this._deleteCommentsThreads[i] = new Thread(this.deleteComments);
                    this._deleteCommentsThreads[i].Start();
                }

                new Thread(this.deleteCompleteRoutine).Start();
            }
            catch(Exception exc)
            {
                MessageBox.Show(this, exc.Message);
            }
        }

        private void updateNewsButton_Click(object sender, EventArgs e)
        {
            if(this.IsProcessingNews)
                return;

            try
            {
                var current = this.newsBeginDatePicker.Value;
                var end = this.newsEndDatePicker.Value;
                if(DateTime.Compare(current, end) >= 0)
                    throw new Exception("시작일과 종료일이 올바르지 않습니다.");

                var type = this.loadNewsTypeComboBox.SelectedItem.GetType().GetProperty("Value").GetValue(this.loadNewsTypeComboBox.SelectedItem) as string;

                if(this._updateNewsThread != null)
                    this._updateNewsThread.Join();

                this.IsProcessingNews = true;
                this._updateNewsThread = new Thread(new ParameterizedThreadStart(this.updateNews));
                this._updateNewsThread.Start(type);
            }
            catch(Exception exc)
            {
                MessageBox.Show(this, exc.Message);
            }
        }

        private void autoCommentButton_Click(object sender, EventArgs e)
        {
            if(this.IsProcessingNews)
                return;

            if(this._autoCommentsThread != null)
                this._autoCommentsThread.Join();

            this.IsProcessingNews = true;
            this._autoCommentsThread = new Thread(this.autoComment);
            this._autoCommentsThread.Start();
        }

        private void newsTable_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var news = this.newsTable.Rows[e.RowIndex].Tag as JSONNode;
            System.Diagnostics.Process.Start(string.Format("http://sports.news.naver.com/home/news/read.nhn?oid={0}&aid={1}", news["oid"].Value, news["aid"].Value));
        }

        private void commentTable_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var comment = this.commentTable.Rows[e.RowIndex].Tag as JSONNode;
                var url = comment["objectUrl"].Value;
                if (url.Equals("null"))
                    throw new Exception("해당 기사는 삭제되었습니다.");

                System.Diagnostics.Process.Start(url);
            }
            catch(Exception exc)
            {
                MessageBox.Show(this, exc.Message);
            }
        }

        private void stopCommentsButton_Click(object sender, EventArgs e)
        {
            this.IsProcessingComments = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsProcessingComments = false;
            this.IsProcessingNews = false;
        }

        private void stopNewsButton_Click(object sender, EventArgs e)
        {
            this.IsProcessingNews = false;
        }
    }
}