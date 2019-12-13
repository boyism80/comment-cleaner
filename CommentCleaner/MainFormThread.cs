using SimpleJSON;
using System;
using System.Windows.Forms;

namespace WebAccessor
{
    public partial class MainForm : Form
    {
        public delegate void addCommentThreadRoutineDelegate(JSONNode comment);
        private void addComment(JSONNode comment)
        {
            if(this.commentTable.InvokeRequired)
            {
                this.commentTable.Invoke(new addCommentThreadRoutineDelegate(this.addComment), new object[] { comment } );
            }
            else
            {
                var index = this.commentTable.Rows.Add(comment["commentNo"].AsInt, comment["contents"].Value, comment["objectTitle"].Value, comment["regTime"].Value);
                this.commentTable.Rows[index].Tag = comment;

                this.setCommentsProgressBar(this.commentTable.Rows.Count);
            }
        }

        private delegate void initProgressBarDelegate();
        private void initProgressBar()
        {
            if(this.commentsProgressBar.InvokeRequired)
            {
                this.commentsProgressBar.Invoke(new initProgressBarDelegate(this.initProgressBar));
            }
            else
            {
                this.commentsProgressBar.Value = 0;
                this.commentsProgressBar.Maximum = 100;
            }
        }

        private delegate void setCommentsProgressBarDelegate(int value);
        private void setCommentsProgressBar(int value)
        {
            if(this.commentsProgressBar.InvokeRequired)
            {
                this.commentsProgressBar.Invoke(new setCommentsProgressBarDelegate(this.setCommentsProgressBar), new object[] { value } );
            }
            else
            {
                this.commentsProgressBar.Value = Math.Min(this.commentsProgressBar.Maximum, value);
                this.setCurrentCommentsCountLabel(value);

                var percent = (value / (float)this.commentsProgressBar.Maximum * 100.0f).ToString("0.00");
                this.CommentState = string.Format("{0}% 진행중..", percent);
            }
        }

        private delegate void setMaximumCommentsProgressBarDelegate(int maximum);
        private void setMaximumCommentsProgressBar(int maximum)
        {
            if(this.commentsProgressBar.InvokeRequired)
            {
                this.commentsProgressBar.Invoke(new setMaximumCommentsProgressBarDelegate(this.setMaximumCommentsProgressBar), new object[] { maximum } );
            }
            else
            {
                this.commentsProgressBar.Maximum = maximum;
                this.setTotalCommentsCountLabel(maximum);
            }
        }

        private delegate void setTotalCommentsCountLabelDelegate(int total);
        private void setTotalCommentsCountLabel(int total)
        {
            if(this.totalCommentsLabel.InvokeRequired)
            {
                this.totalCommentsLabel.Invoke(new setTotalCommentsCountLabelDelegate(this.setTotalCommentsCountLabel), new object[] { total });
            }
            else
            {
                this.totalCommentsLabel.Text = string.Format("{0}개", total);
            }
        }

        private delegate void setCurrentCommentsCountLabelDelegate(int count);
        private void setCurrentCommentsCountLabel(int count)
        {
            if(this.currentCommentsLabel.InvokeRequired)
            {
                this.currentCommentsLabel.Invoke(new setCurrentCommentsCountLabelDelegate(this.setCurrentCommentsCountLabel), new object[] { count });
            }
            else
            {
                this.currentCommentsLabel.Text = string.Format("{0}개", count);
            }
        }

        private delegate void setCurrentStateLabelDelegate(string text);
        private void setCommentState(string text)
        {
            if(this.commentStateLabel.InvokeRequired)
            {
                this.commentStateLabel.Invoke(new setCurrentStateLabelDelegate(this.setCommentState), new object[] { text });
            }
            else
            {
                this.commentStateLabel.Text = text;
            }
        }

        private delegate void setVisibilityProgressBarDelegate(bool isVisible);
        private void setVisibilityProgressBar(bool isVisible)
        {
            if(this.commentsProgressBar.InvokeRequired)
            {
                this.commentsProgressBar.Invoke(new setVisibilityProgressBarDelegate(this.setVisibilityProgressBar), new object[] { isVisible });
            }
            else
            {
                this.commentsProgressBar.Visible = isVisible;
            }
        }

        private delegate void setEnableUpdateButtonDelegate(bool enable);
        private void setEnableUpdateButton(bool enabled)
        {
            if(this.updateButton.InvokeRequired)
            {
                this.updateButton.Invoke(new setEnableUpdateButtonDelegate(this.setEnableUpdateButton), new object[] { enabled });
            }
            else
            {
                this.updateButton.Enabled = enabled;
            }
        }

        private delegate void setCommentLogBoxDelegate(string text);
        private void setCommentLogBox(string text)
        {
            if(this.commentLogBox.InvokeRequired)
            {
                this.commentLogBox.Invoke(new setCommentLogBoxDelegate(this.setCommentLogBox), new object[] { text });
            }
            else
            {
                this.commentLogBox.AppendText(text + "\r\n");
            }
        }

        private delegate void clearTableDelegate();
        private void clearCommentTable()
        {
            if(this.commentTable.InvokeRequired)
            {
                this.commentTable.Invoke(new clearTableDelegate(this.clearCommentTable));
            }
            else
            {
                this.commentTable.Rows.Clear();
            }
        }

        private delegate void deleteCommentDelegate(DataGridViewRow row);
        private void deleteComment(DataGridViewRow row)
        {
            if(this.commentTable.InvokeRequired)
            {
                this.commentTable.Invoke(new deleteCommentDelegate(this.deleteComment), new object[] { row });
            }
            else
            {
                this.commentTable.Rows.Remove(row);
            }
        }

        private void clearNewsTable()
        {
            if(this.newsTable.InvokeRequired)
            {
                this.newsTable.Invoke(new clearTableDelegate(this.clearNewsTable));
            }
            else
            {
                this.newsTable.Rows.Clear();
            }
        }

        private delegate void addNewsDelegate(JSONNode news);
        private void addNews(JSONNode news)
        {
            if(this.newsTable.InvokeRequired)
            {
                this.newsTable.Invoke(new addNewsDelegate(this.addNews), new object[] { news });
            }
            else
            {
                var index = this.newsTable.Rows.Add(news["title"].Value, news["datetime"].Value);
                this.newsTable.Rows[index].Tag = news;
            }
        }

        private delegate void enableRequestNewsDelegate(bool enabled);
        private void enableRequestNews(bool enabled)
        {
            if(this.updateNewsButton.InvokeRequired)
            {
                this.updateNewsButton.Invoke(new enableRequestNewsDelegate(this.enableRequestNews), new object[] { enabled });
            }
            else
            {
                this.updateNewsButton.Enabled = enabled;
            }
        }

        private delegate void setMaximumNewsProgressBarDelegate(int maximum);
        private void setMaximumNewsProgressBar(int maximum)
        {
            if(this.newsProgressBar.InvokeRequired)
            {
                this.newsProgressBar.Invoke(new setMaximumNewsProgressBarDelegate(this.setMaximumNewsProgressBar), new object[] { maximum });
            }
            else
            {
                this.newsProgressBar.Maximum = maximum;
            }
        }

        private delegate void addNewsProgressBarDelegate(int value);
        private void addNewsProgressBar(int value)
        {
            if(this.newsProgressBar.InvokeRequired)
            {
                this.newsProgressBar.Invoke(new addNewsProgressBarDelegate(this.addNewsProgressBar), new object[] { value });
            }
            else
            {
                this.newsProgressBar.Value = Math.Min(this.newsProgressBar.Maximum, this.newsProgressBar.Value + value);
            }
        }

        private delegate void setNewsProgressBarDelegate(int value);
        private void setNewsProgressBar(int value)
        {
            if(this.newsProgressBar.InvokeRequired)
            {
                this.newsProgressBar.Invoke(new setNewsProgressBarDelegate(this.setNewsProgressBar), new object[] { value });
            }
            else
            {
                this.newsProgressBar.Value = Math.Min(this.newsProgressBar.Maximum, value);
            }
        }

        private delegate void setNewsLogBoxDelegate(string text);
        private void setNewsLogBox(string text)
        {
            if(this.newsLogBox.InvokeRequired)
            {
                this.newsLogBox.Invoke(new setNewsLogBoxDelegate(this.setNewsLogBox), new object[] { text });
            }
            else
            {
                this.newsLogBox.AppendText(text + "\r\n");
            }
        }

        private delegate void setNewsStateDelegate(string text);
        private void setNewsState(string text)
        {
            if(this.newsStateLabel.InvokeRequired)
            {
                this.newsStateLabel.Invoke(new setNewsStateDelegate(this.setNewsState), new object[] { text });
            }
            else
            {
                this.newsStateLabel.Text = text;
            }
        }

        private delegate void enableNewsSettingPanelDeleagte(bool enabled);
        private void enableNewsSettingPanel(bool enabled)
        {
            if(this.newsSettingPanel.InvokeRequired)
            {
                this.newsSettingPanel.Invoke(new enableNewsSettingPanelDeleagte(this.enableNewsSettingPanel), new object[] { enabled });
            }
            else
            {
                this.newsSettingPanel.Enabled = enabled;
            }
        }

        private delegate void enableProcessCommentsDelegate(bool isRun);
        private void enableProcessComments(bool isRun)
        {
            if(this.processCommentsPanel.InvokeRequired)
            {
                this.processCommentsPanel.Invoke(new enableProcessCommentsDelegate(this.enableProcessComments), new object[] { isRun });
            }
            else
            {
                this.processCommentsPanel.Enabled = isRun;
            }
        }

        private delegate void enableUnprocessCommentsDelegate(bool isRun);
        private void enableUnprocessComments(bool isRun)
        {
            if(this.unprocessCommentsPanel.InvokeRequired)
            {
                this.unprocessCommentsPanel.Invoke(new enableUnprocessCommentsDelegate(this.enableUnprocessComments), new object[] { isRun });
            }
            else
            {
                this.unprocessCommentsPanel.Enabled = isRun;
            }
        }

        private delegate void enableProcessNewsDelegate(bool isRun);
        private void enableProcessNews(bool isRun)
        {
            if(this.processNewsPanel.InvokeRequired)
            {
                this.processNewsPanel.Invoke(new enableProcessNewsDelegate(this.enableProcessNews), new object[] { isRun });
            }
            else
            {
                this.processNewsPanel.Enabled = isRun;
            }
        }

        private delegate void enableUnprocessNewsDelegate(bool isRun);
        private void enableUnprocessNews(bool isRun)
        {
            if(this.unprocessNewsPanel.InvokeRequired)
            {
                this.unprocessNewsPanel.Invoke(new enableUnprocessNewsDelegate(this.enableUnprocessNews), new object[] { isRun });
            }
            else
            {
                this.unprocessNewsPanel.Enabled = isRun;
            }
        }
    }
}
