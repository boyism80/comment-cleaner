using Jint;
using SimpleJSON;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebAccessor
{
    public abstract class PortalSite
    {
        public CookieContainer Cookie { get; protected set; }

        protected PortalSite()
        {
            //this.Cookie = new CookieContainer();
        }

        public abstract bool login(string id, string pw);
    }

    public class Naver : PortalSite
    {
        public enum NewsType { Default, Sports }

        private static Naver _instance = new Naver();

        public static Naver Instance { get { return Naver._instance; } }

        private Naver()
        {
        }

        public JSONNode comments(string ticket, int page, int size)
        {
            try
            {
                var response = this.request("GET",
                                        ticket == "news" ? $"https://apis.naver.com/commentBox/cbox/web_naver_list_per_user_jsonp.json?ticket={ticket}&templateId=view_society&pool=cbox5&_callback=&lang=ko&country=KR&objectId=&categoryId=&pageSize={size}&indexSize=10&groupId=&listType=user&pageType=more&page={page}&sort=NEW&current=&prev=&commentNo=&_=" : 
                                                           $"http://apis.naver.com/commentBox/cbox2/web_naver_user_info_jsonp.json?lang=ko&ticket={ticket}&page={page}&pageSize={size}&sCommentNo=",
                                        
                                        ticket == "news" ? "http://news.naver.com/main/comment/usercomment.nhn" : 
                                                           "http://sports.news.naver.com/comment/user.nhn?page=1",
                                        
                                        ticket == "news" ? null : 
                                                  "application/javascript, */*;q=0.8",
                                        null);


                var regex = new Regex(@"_callback\((?<json>.+)\)");
                var match = regex.Match(response);
                if (match.Length == 0)
                    return null;

                return JSON.Parse(match.Groups["json"].Value);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public int count(string ticket)
        {
            var def = this.comments(ticket, 1, 10);
            if(def == null)
                return 0;

            return def["result"]["count"]["total"].AsInt;
        }

        private bool split(string objectId, out string oid, out string aid)
        {
            oid = null;
            aid = null;

            var regex = new Regex(@"news(?<oid>.+),(?<aid>.+)");
            var match = regex.Match(objectId);
            if (match.Groups.Count == 0)
                return false;

            oid = match.Groups["oid"].Value;
            aid = match.Groups["aid"].Value;
            return true;
        }

        private string combine(string oid, string aid)
        {
            return $"news{oid},{aid}";
        }

        private string request(string method, string url, string referer, string contentType, string streamContent)
        {
            var request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = method;
            request.Referer = referer;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
            if(contentType != null)
                request.ContentType = contentType;
            request.CookieContainer = this.Cookie;

            if(streamContent != null)
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

        private string pool(string ticket)
        {
            return ticket == "news" ? "cbox5" : "cbox2";
        }

        private JSONNode extractJson(string value)
        {
            var regex = new Regex(@"_callback\((?<json>.+)\)");
            var match = regex.Match(value);
            if (match.Groups.Count == 0)
                return null;

            return JSON.Parse(match.Groups["json"].Value);
        }

        // templateId : view_politics
        private string token(string ticket, string template, string objectId)
        {
            var oid = string.Empty;
            var aid = string.Empty;
            if (this.split(objectId, out oid, out aid) == false)
                return null;

            var pool = this.pool(ticket);

            var url = $"https://apis.naver.com/commentBox/cbox/web_naver_token_jsonp.json?ticket={ticket}&templateId={template}&pool={pool}&_callback=&lang=ko&country=KR&objectId={WebUtility.UrlEncode(objectId)}&categoryId=&pageSize=10&indexSize=10&groupId=&listType=OBJECT&pageType=more&_=";
            var referer = $"https://news.naver.com/main/read.nhn?mode=LPOD&mid=sec&oid={oid}&aid={aid}&rc=N";

            var response = this.request("GET", url, referer, null, null);

            var json = null as JSONNode;
            if(ticket == "news")
                json = JSON.Parse(response);
            else
                json = this.extractJson(response);

            if(json == null || json["success"].AsBool == false)
                return null;

            return json["result"]["cbox_token"].Value;
        }

        public bool deleteComment(JSONNode comment)
        {
            var ticket = comment["ticket"].Value;
            var template = comment["templateId"].Value;
            var id = comment["objectId"].Value;
            var commentNo = comment["commentNo"].AsInt;
            var pool = this.pool(ticket);

            if(template.Equals("default"))
                template = string.Empty;

            var oid = string.Empty;
            var aid = string.Empty;
            if (this.split(id, out oid, out aid) == false)
                return false;

            var token = this.token(ticket, template, id);
            if (token == null)
                return false;

            var url = $"https://apis.naver.com/commentBox/cbox/web_naver_delete_json.json?ticket={ticket}&templateId={template}&pool={pool}";
            var referer = $"https://news.naver.com/main/read.nhn?mode=LSD&mid=sec&sid1={oid}&oid={oid}&aid={aid}";
            var param = $"lang=ko&country=KR&objectId={WebUtility.UrlEncode(id)}&categoryId=&pageSize=10&indexSize=10&groupId=&listType=OBJECT&pageType=more&commentNo={commentNo}&userType=&resultType=COMMENT&clientType=web-pc&cbox_token={token}";
            

            var response = this.request("POST", url, referer, "application/x-www-form-urlencoded;", param);
            var json = JSON.Parse(response);
            return json["success"].AsBool;
        }

        // template : view_politics(일반뉴스), string.empty(스포츠뉴스)
        public JSONNode addComment(string ticket, string template, string objectId, string content)
        {
            var token = this.token(ticket, template, objectId);
            if(token == null)
                return null;

            var oid = string.Empty;
            var aid = string.Empty;
            if (this.split(objectId, out oid, out aid) == false)
                return null;

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
            var likeItId = $"ne_{oid}_{aid}";
            var streamPost = $"ticket={ticket}&templateId={templateId}&pool={this.pool(ticket)}&lang=ko&country=KR&objectId={WebUtility.UrlEncode(objectId)}&cbox_token={token}&contents={content}&objectUrl={objectUrl}&clientType=web-pc&groupId=&likeItId={likeItId}&indexSize=10&pageSize=10&userType=&categoryId=&listType=OBJECT&validateBanWords=true&sort=NEW&secret=false"; 
            
            var url = $"{baseurl}?{streamPost}";

            var response = this.request("GET", url, referer, 
                                        "application/javascript, */*;q=0.8",
                                        null);

            var json = JSON.Parse(response);
            return json;
        }

        public JSONNode addComment(string ticket, string template, string oid, string aid, string content)
        {
            return this.addComment(ticket, template, this.combine(oid, aid), content);
        }

        public JSONNode addComment(string ticket, string template, JSONNode news, string content)
        {
            return this.addComment(ticket, template, news["oid"].Value, news["aid"].Value, content);
        }

        // type : kbaseball, wbaseball, kfootball, wfootball, basketball, volleyball, golf, general, esports
        public JSONNode news(string ticket, string type, string date, int page =  1)
        {
            if(ticket != "sports")
                return null;

            var response = this.request("GET",
                $"https://sports.news.naver.com/{type}/news/list.nhn?date={date}&page={page}&isphoto=N",
                "https://sports.news.naver.com/lol/news/index.nhn",
                "application/json, text/javascript, */*; q=0.01",
                null);

            return JSON.Parse(response);
        }

        public int page(string ticket, string type, string date)
        {
            var json = this.news(ticket, type, date);
            if(json == null)
                return 0;

            return json["totalPages"].AsInt;
        }

        private string RequestKExportKeys()
        {
            var response = this.request("GET", "https://nid.naver.com/login/ext/keys.nhn", "https://nid.naver.com/nidlogin.login", null, null);
            return response;
        }

        private string CombinedText(string key, string id, string pw)
        {
            var ret = String.Empty;

            ret += Convert.ToChar(key.Length).ToString();
            ret += key;
            ret += Convert.ToChar(id.Length).ToString();
            ret += id;
            ret += Convert.ToChar(pw.Length).ToString();
            ret += pw;

            return ret;
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

        private bool TryLogin(string keyName, string encpw, string bvsd)
        {
            var content = $"bvsd={bvsd}&enctp=1&encpw={encpw}&encnm={keyName}&svctype=0&svc=&viewtype=0&locale=ko_KR&postDataKey=&smart_LEVEL=-1&logintp=&url=http%3A%2F%2Fwww.naver.com&localechange=&ls=&xid=&pre_id=&resp=&ru=&id=&pw=";
            var response = this.request("POST", "https://nid.naver.com/nidlogin.login", "https://nid.naver.com/nidlogin.login", "application/x-www-form-urlencoded", content);

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
            return response.Contains("isLogin");
#endif
        }

        public override bool login(string id, string pw)
        {
            this.Cookie = new CookieContainer();

            // 키 긁어옴
            var sessionKeys = this.RequestKExportKeys();
            if (String.IsNullOrWhiteSpace(sessionKeys))
                return false;

            // 키 나눔
            var splitedKeys = sessionKeys.Split(',');
            var sessionKey = splitedKeys[0];
            var keyName = splitedKeys[1];
            var evalue = splitedKeys[2];
            var nvalue = splitedKeys[3];

            // 3. 문자 암호화
            var encpw = EncryptRSA(evalue, nvalue, CombinedText(sessionKey, id, pw));

            var engine = new Engine();
            var bvsd = engine.Execute(File.ReadAllText("naver.js")).GetValue("bvsd").Invoke(id, pw);

            var content = $"bvsd={bvsd}&enctp=1&encpw={encpw}&encnm={keyName}&svctype=0&svc=&viewtype=0&locale=ko_KR&postDataKey=&smart_LEVEL=-1&logintp=&url=http%3A%2F%2Fwww.naver.com&localechange=&ls=&xid=&pre_id=&resp=&ru=&id=&pw=";
            var response = this.request("POST", "https://nid.naver.com/nidlogin.login", "https://nid.naver.com/nidlogin.login", "application/x-www-form-urlencoded", content);

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
            return response.Contains("isLogin");
#endif
        }

        public bool logout()
        {
            if(this.Cookie == null)
                return false;

            var response = this.request("GET", "https://nid.naver.com/nidlogin.logout?returl=https://www.naver.com/", "https://my.naver.com/new?svgless=true", null, null);
            this.Cookie = null;
            return true;
        }
    }

    public class Daum : PortalSite
    {
        private static Daum _instance = new Daum();

        public static Daum Instance { get { return Daum._instance; } }

        private Daum()
        {
        }

        public override bool login(string id, string pw)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://logins.daum.net/accounts/login.do?slogin=2");
            request.Method = "POST";
            request.Referer = "http://login.daum.net/accounts/loginform.do";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = this.Cookie;

            var streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write($"id={id}&pw={pw}");
            streamWriter.Close();

            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var responseStream = response.GetResponseStream();
            var responseStreamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = responseStreamReader.ReadToEnd();
            response.Close();
            responseStream.Close();
            responseStreamReader.Close();

            return result.Contains("document.location.replace(\"http://www.daum.net/\");");
        }
    }
}