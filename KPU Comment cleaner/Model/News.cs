using SimpleJSON;
using System;
using System.Net;

namespace KPU_Comment_cleaner.Model
{
    public class News : Component
    {
        public string oid { get; private set; }
        public string aid { get; private set; }
        public string Id { get { return $"news{this.oid},{this.aid}"; } }
        public string OfficeName { get; private set; }
        public string Thumbnail { get; private set; }
        public string SectionName { get; private set; }

        public News(JSONNode json)
        {
            this.oid = json["oid"].Value;
            this.aid = json["aid"].Value;
            this.OfficeName = json["officeName"].Value;
            this.Title = WebUtility.HtmlDecode(json["title"].Value);
            this.Content = json["subContent"].Value;
            this.Thumbnail = json["thumbnail"].Value;
            this.DateTime = DateTime.Parse(json["datetime"].Value);
            this.URL = $"https://sports.news.naver.com/{json["type"].Value}/news/read.nhn?oid={this.oid}&aid={this.aid}";
            this.SectionName = json["sectionName"].Value;
        }
    }
}
