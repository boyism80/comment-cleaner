using Jint;
using KPU_Comment_cleaner.Model;
using KPU_Comment_cleaner.ViewModel.Base;
using SimpleJSON;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KPU_Comment_cleaner.ViewModel
{
    public class ComponentItems : ObservableCollection<ComponentViewModel>
    {
        public ConnectionViewModel Parent { get; private set; }

        protected ComponentItems(ConnectionViewModel owner)
        {
            this.Parent = owner;
        }
    }

    public class CommentItems : ComponentItems
    {
        public event ComponentViewModel.ResponseEventHandler Response;

        public CommentItems(ConnectionViewModel owner) : base(owner)
        { }

        public bool Delete(CommentViewModel comment)
        {
            try
            {
                var template = comment.Template;

                if (template.Equals("default"))
                    template = string.Empty;

                var token = this.Parent.Token(comment.Ticket, template, comment.Id);
                if (token == null)
                    return false;

                var url = $"https://apis.naver.com/commentBox/cbox/web_naver_delete_json.json?ticket={comment.Ticket}&templateId={template}&pool={comment.Pool}";
                var referer = $"https://news.naver.com/main/read.nhn?mode=LSD&mid=sec&sid1={comment.oid}&oid={comment.oid}&aid={comment.aid}";
                var param = $"lang=ko&country=KR&objectId={WebUtility.UrlEncode(comment.Id)}&categoryId=&pageSize=10&indexSize=10&groupId=&listType=OBJECT&pageType=more&commentNo={comment.Index}&userType=&resultType=COMMENT&clientType=web-pc&cbox_token={token}";


                var response = this.Parent.Request("POST", url, referer, "application/x-www-form-urlencoded;", param);
                var json = JSON.Parse(response);
                if (json["success"].AsBool == false)
                    throw new Exception(json["message"].Value);

                base.Remove(comment);
                this.Response?.Invoke(this, true, json["message"].Value);
                return json["success"].AsBool;
            }
            catch (Exception e)
            {
                this.Response?.Invoke(this, false, e.Message);
                return false;
            }
        }

        public new void Clear()
        {
            var items = this.Items.ToArray();
            foreach (var item in items)
                this.Remove(item);
        }
    }

    public class NewsItems : ComponentItems
    {
        public event ComponentViewModel.ResponseEventHandler Response;

        public NewsItems(ConnectionViewModel owner) : base(owner)
        { }

        public void Add(NewsViewModel vmodel)
        {
            vmodel.Response += this.Item_Response;
            base.Add(vmodel);
        }

        private void Item_Response(object sender, bool result, string message)
        {
            this.Response?.Invoke(this, result, message);
        }

        public new void Clear()
        {
            var items = this.Items.ToArray();
            foreach (var item in items)
            {
                (item as NewsViewModel).Response -= this.Item_Response;
                this.Remove(item);
            }
        }
    }

    public class ConnectionViewModel : BaseViewModel
    {
        public event ComponentViewModel.ResponseEventHandler Response;

        public MainWindowViewModel Parent { get; private set; }
        public CookieContainer Cookie { get; private set; } = new CookieContainer();
        public CommentItems CommentItems { get; private set; }
        public NewsItems NewsItems { get; private set; }

        private string _id = string.Empty;
        public string Id
        {
            get
            {
                return this._id;
            }
            private set
            {
                this._id = value;
                this.OnPropertyChanged(nameof(this.IsLogin));
                this.OnPropertyChanged(nameof(this.IdText));
            }
        }
        public string IdText
        {
            get
            {
                return this.IsLogin ? $"{this.Id}@naver.com" : "Login";
            }
        }

        public bool IsLogin
        {
            get
            {
                return !string.IsNullOrEmpty(this.Id);
            }
        }

        public int TotalCommentCount { get; private set; }

        public ConnectionViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.Parent = mainWindowViewModel;
            this.CommentItems = new CommentItems(this);
            this.NewsItems = new NewsItems(this);
        }

        ~ConnectionViewModel()
        {
            this.Logout();
        }

        private JSONNode Extract(string value)
        {
            var regex = new Regex(@"_callback\((?<json>.+)\)");
            var match = regex.Match(value);
            if (match.Groups.Count == 0)
                return null;

            return JSON.Parse(match.Groups["json"].Value);
        }

        public string Token(string ticket, string template, string objectId)
        {
            if (ticket == "sports")
                template = string.Empty;

            var oid = string.Empty;
            var aid = string.Empty;
            if (Comment.SplitId(objectId, out oid, out aid) == false)
                return null;

            var url = $"https://apis.naver.com/commentBox/cbox/web_naver_token_jsonp.json?ticket={ticket}&templateId={template}&pool={Comment.GetPool(ticket)}&_callback=&lang=ko&country=KR&objectId={WebUtility.UrlEncode(objectId)}&categoryId=&pageSize=10&indexSize=10&groupId=&listType=OBJECT&pageType=more&_=";
            var referer = $"https://news.naver.com/main/read.nhn?mode=LPOD&mid=sec&oid={oid}&aid={aid}&rc=N";

            var response = this.Request("GET", url, referer, null, null);

            var json = null as JSONNode;
            if (ticket == "news")
                json = JSON.Parse(response);
            else
                json = this.Extract(response);

            if (json == null || json["success"].AsBool == false)
                return null;

            return json["result"]["cbox_token"].Value;
        }

        public string Request(string method, string url, string referer, string contentType, string streamContent)
        {
            var request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = method;
            request.Referer = referer;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
            if (contentType != null)
                request.ContentType = contentType;
            request.CookieContainer = this.Cookie;

            if (streamContent != null)
            {
                var streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(streamContent);
                streamWriter.Close();
            }

            var response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var responseStream = response.GetResponseStream();
            var responseStreamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = responseStreamReader.ReadToEnd();
            response.Close();
            responseStream.Close();
            responseStreamReader.Close();

            return result;
        }

        public async Task<string> PostRequest(string method, string url, string referer, string contentType, string streamContent)
        {
            var request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = method;
            request.Referer = referer;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
            if (contentType != null)
                request.ContentType = contentType;
            request.CookieContainer = this.Cookie;

            if (streamContent != null)
            {
                using (var stream = await request.GetRequestStreamAsync())
                {
                    var bytes = Encoding.UTF8.GetBytes(streamContent);
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            var response = await request.GetResponseAsync() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK)
                return null;


            var result = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public int ReadComments(string ticket, int page, int size)
        {
            try
            {
                var response = this.Request("GET",
                    ticket == "news" ? $"https://apis.naver.com/commentBox/cbox/web_naver_list_per_user_jsonp.json?ticket={ticket}&templateId=view_society&pool=cbox5&_callback=&lang=ko&country=KR&objectId=&categoryId=&pageSize={size}&indexSize=10&groupId=&listType=user&pageType=more&page={page}&sort=NEW&current=&prev=&commentNo=&_=" : $"http://apis.naver.com/commentBox/cbox2/web_naver_user_info_jsonp.json?lang=ko&ticket={ticket}&page={page}&pageSize={size}&sCommentNo=",
                    ticket == "news" ? "http://news.naver.com/main/comment/usercomment.nhn" : "http://sports.news.naver.com/comment/user.nhn?page=1",
                    ticket == "news" ? null : "application/javascript, */*;q=0.8",
                    null);

                var regex = new Regex(@"_callback\((?<json>.+)\)");
                var match = regex.Match(response);
                if (match.Length == 0)
                    throw new Exception();

                var data = JSON.Parse(match.Groups["json"].Value);
                if (data["success"].AsBool == false)
                    throw new Exception(data["message"].Value);

                var comments = data["result"]["commentList"].AsArray;
                foreach (JSONNode comment in comments)
                    this.CommentItems.Add(new CommentViewModel(this, comment));

                this.TotalCommentCount = data["result"]["count"]["total"].AsInt;

                this.Response?.Invoke(this.CommentItems, true, data["message"].Value);
                return comments.Count;
            }
            catch (Exception e)
            {
                this.Response?.Invoke(this.CommentItems, false, e.Message);
                return -1;
            }
        }

        public int ReadNews(string ticket, string type, DateTime date, int page = 1)
        {
            try
            {
                if (ticket != "sports")
                    throw new Exception($"ticket value cannot be {ticket}");

                var response = this.Request("GET",
                    $"https://sports.news.naver.com/{type}/news/list.nhn?date={date.ToString("yyyyMMdd")}&page={page}&isphoto=N",
                    "https://sports.news.naver.com/lol/news/index.nhn",
                    "application/json, text/javascript, */*; q=0.01",
                    null);

                var json = JSON.Parse(response);
                if (json["page"].AsInt > json["totalPages"].AsInt)
                    throw new Exception();

                var newsList = json["list"].AsArray;
                foreach (JSONNode news in newsList)
                {
                    news.Add("type", type);
                    this.NewsItems.Add(new NewsViewModel(this, news));
                }

                this.Response?.Invoke(this.NewsItems, true, "요청을 성공적으로 수행했습니다.");

                if (json["totalPages"].AsInt == json["page"].AsInt)
                    return 0;

                return 1;
            }
            catch (Exception e)
            {
                this.Response?.Invoke(this.NewsItems, false, e.Message);
                return -1;
            }
        }

        private string RequestKExportKeys()
        {
            var response = this.Request("GET", "https://nid.naver.com/login/ext/keys.nhn", "https://nid.naver.com/nidlogin.login", null, null);
            return response;
        }

        private string EncryptRSA(string evalue, string nvalue, string data)
        {
            string result = String.Empty;

            // 공개키 생성
            var publicKey = new RSAParameters()
            {
                Modulus = Enumerable.Range(0, evalue.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(evalue.Substring(x, 2), 16))
                .ToArray()
                ,
                Exponent = Enumerable.Range(0, nvalue.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(nvalue.Substring(x, 2), 16))
                .ToArray()
            };

            try
            {
                var rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(publicKey);

                // 암호화 및 Byte => String 변환
                var enc = rsa.Encrypt(Encoding.UTF8.GetBytes(data), false);
                result = BitConverter.ToString(enc).Replace("-", "").ToLower();
            }
            catch (CryptographicException)
            {
                result = String.Empty;
            }



            return result;
        }

        public bool Login(string id, string pw)
        {
            if (this.IsLogin)
                return false;

            this.Cookie = new CookieContainer();

            // 키 긁어옴
            var sessionKeys = this.RequestKExportKeys();
            if (string.IsNullOrWhiteSpace(sessionKeys))
                return false;

            // 키 나눔
            var splitedKeys = sessionKeys.Split(',');
            var sessionKey = splitedKeys[0];
            var keyName = splitedKeys[1];
            var evalue = splitedKeys[2];
            var nvalue = splitedKeys[3];

            // 3. 문자 암호화
            //var encpw = EncryptRSA(evalue, nvalue, CombinedText(sessionKey, id, pw));
            var encpw = EncryptRSA(evalue, nvalue, $"{Convert.ToChar(sessionKey.Length)}{sessionKey}{Convert.ToChar(id.Length)}{id}{Convert.ToChar(pw.Length)}{pw}");

            var engine = new Engine();
            var bvsd = engine.Execute(File.ReadAllText("naver.js")).GetValue("bvsd").Invoke(id, pw);

            var content = $"bvsd={bvsd}&enctp=1&encpw={encpw}&encnm={keyName}&svctype=0&svc=&viewtype=0&locale=ko_KR&postDataKey=&smart_LEVEL=-1&logintp=&url=http%3A%2F%2Fwww.naver.com&localechange=&ls=&xid=&pre_id=&resp=&ru=&id=&pw=";
            var response = this.Request("POST", "https://nid.naver.com/nidlogin.login", "https://nid.naver.com/nidlogin.login", "application/x-www-form-urlencoded", content);

#if (LEGACY)
            var regex = new Regex("location.replace\\(\\\"(?<sso>.+)\\\"\\)");
            var match = regex.Match(response);
            if (match.Length == 0)
                return false;


            var nextURL = match.Groups["sso"].Value;
            response = this.request("GET", nextURL, "https://nid.naver.com/nidlogin.login", "text/html, application/xhtml+xml, image/jxr, */*", null);
            var header = @"<html><script language=javascript>window.location.replace(""";
            if (response.IndexOf(header) < 0)
                return false;

            return true;
#else
            var success = response.Contains("isLogin");
            if (success)
                this.Id = id;

            return this.IsLogin;
#endif
        }

        public bool Logout()
        {
            if (this.Cookie == null)
                return false;

            var response = this.Request("GET", "https://nid.naver.com/nidlogin.logout?returl=https://www.naver.com/", "https://my.naver.com/new?svgless=true", null, null);
            this.Cookie = null;
            this.CommentItems.Clear();

            this.Id = string.Empty;
            this.TotalCommentCount = 0;
            return true;
        }
    }
}
