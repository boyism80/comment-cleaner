using KPU_Comment_cleaner.Model;
using KPU_Comment_cleaner.ViewModel.Base;
using SimpleJSON;
using System;
using System.Net;

namespace KPU_Comment_cleaner.ViewModel
{
    public class NewsViewModel : ComponentViewModel
    {
        public event ResponseEventHandler Response;

        public new News Model { get { return base.Model as News; } }

        public string oid { get { return this.Model.oid; } }
        public string aid { get { return this.Model.aid; } }
        public string Id { get { return this.Model.Id; } }
        public string OfficeName { get { return this.Model.OfficeName; } }
        public string Thumbnail { get { return this.Model.Thumbnail; } }
        public string SectionName { get { return this.Model.SectionName; } }

        public override string AnotherButtonText => "Write";

        public NewsViewModel(ConnectionViewModel owner, JSONNode json) : base(owner, json)
        {
        }

        protected override Component AllocateComponent(ConnectionViewModel owner, JSONNode json)
        {
            return new News(json);
        }

        protected override void OnAnotherWork(object obj)
        {
            var connection = this.Parent;
            var mainWindow = connection.Parent;
            var message = mainWindow.CommentWriterViewModel.RandomMessage;

            if (this.WriteComment(message))
                mainWindow.CommentWriterViewModel.LastestMessage = message;
        }

        public bool WriteComment(string ticket, string template, string content)
        {
            try
            {
                var token = this.Parent.Token(ticket, template, $"news{this.oid},{this.aid}");

                if (string.IsNullOrWhiteSpace(token))
                    throw new Exception("Wrong ticket.");

                var objectUrl = ticket == "news" ?
                    "http://news.naver.com/main/read.nhn" :
                    "http://sports.news.naver.com/index.nhn";

                var referer = ticket == "news" ?
                    "http://news.naver.com/main/read.nhn" :
                    "http://static.cbox.naver.net";

                var baseurl = ticket == "news" ?
                    "http://apis.naver.com/commentBox/cbox/web_naver_create_json.json" :
                    "http://apis.naver.com/commentBox/cbox/web_naver_create_jsonp.json";

                var templateId = ticket == "news" ? template : string.Empty;
                var likeItId = $"ne_{this.oid}_{this.aid}";
                var streamPost = $"ticket={ticket}&templateId={templateId}&pool={Comment.GetPool(ticket)}&lang=ko&country=KR&objectId={WebUtility.UrlEncode(this.Id)}&cbox_token={token}&contents={content}&objectUrl={objectUrl}&clientType=web-pc&groupId=&likeItId={likeItId}&indexSize=10&pageSize=10&userType=&categoryId=&listType=OBJECT&validateBanWords=true&sort=NEW&secret=false";

                var url = $"{baseurl}?{streamPost}";

                var response = this.Parent.Request("GET", url, referer, "application/javascript, */*;q=0.8", null);
                var json = JSON.Parse(response);
                if (json["success"].AsBool == false)
                    throw new Exception(json["message"].Value);

                this.Response?.Invoke(this, true, json["message"].Value);
                return json["success"].AsBool;
            }
            catch (Exception e)
            {
                this.Response?.Invoke(this, false, e.Message);
                return false;
            }
        }

        public bool WriteComment(string content)
        {
            return this.WriteComment("sports", string.Empty, content);
        }
    }
}
