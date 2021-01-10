using SimpleJSON;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace KPU_Comment_cleaner.Model
{
    public class Comment : Component
    {
        public int Index { get; private set; }
        public string Ticket { get; private set; }
        public string Id { get; private set; }
        public string Template { get; private set; }
        public string Pool { get { return GetPool(this.Ticket); } }
        public string oid
        {
            get
            {
                var oid = string.Empty;
                var aid = string.Empty;
                this.SplitId(out oid, out aid);
                return oid;
            }
        }

        public string aid
        {
            get
            {
                var oid = string.Empty;
                var aid = string.Empty;
                this.SplitId(out oid, out aid);
                return aid;
            }
        }


        public Comment(JSONNode json)
        {
            this.Index = json["commentNo"].AsInt;
            this.Title = WebUtility.HtmlDecode(json["contents"].Value);
            this.Content = WebUtility.HtmlDecode(json["objectTitle"].Value);
            this.DateTime = DateTime.Parse(json["regTime"].Value);
            this.URL = json["objectUrl"].Value;
            this.Ticket = json["ticket"].Value;
            this.Id = json["objectId"].Value;
            this.Template = json["templateId"].Value;
        }

        private bool SplitId(out string oid, out string aid)
        {
            return SplitId(this.Id, out oid, out aid);
        }

        public static bool SplitId(string id, out string oid, out string aid)
        {
            oid = null;
            aid = null;

            var regex = new Regex(@"news(?<oid>.+),(?<aid>.+)");
            var match = regex.Match(id);
            if (match.Groups.Count == 0)
                return false;

            oid = match.Groups["oid"].Value;
            aid = match.Groups["aid"].Value;
            return true;
        }

        public static string GetPool(string ticket)
        {
            return ticket.Equals("news") ? "cbox5" : "cbox2";
        }
    }
}
