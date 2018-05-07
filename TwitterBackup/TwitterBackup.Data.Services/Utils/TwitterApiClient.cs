using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TwitterBackup.Data.Services.Utils
{
    public class TwitterApiClient : ITwitterAPIClient
    {
        public const string OauthVersion = "1.0";
        public const string OauthSignatureMethod = "HMAC-SHA1";
        private readonly string consumerKey;
        private readonly string consumerKeySecret;
        private readonly string accessToken;
        private readonly string accessTokenSecret;
        private readonly Random rnd;

        public TwitterApiClient
          (string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerKeySecret = consumerKeySecret;
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;
            this.rnd = new Random();
        }

        public async Task<string> GetTweets(string screenName)
        {
            string resourceUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json";

			var requestParameters = new SortedDictionary<string, string>
			{
				{ "screen_name", screenName }
			};

			var response = await GetResponse(resourceUrl, Method.GET, requestParameters);

            return response;
        }

        private async Task<string> GetResponse
        (string resourceUrl, Method method, SortedDictionary<string, string> requestParameters)
        {
            WebRequest request = null;
            string resultString = string.Empty;

            if (method == Method.POST)
            {
                var postBody = requestParameters.ToWebString();

                request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.Method = method.ToString();
                request.ContentType = "application/x-www-form-urlencoded";

                using (var stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.ASCII.GetBytes(postBody);
                    stream.Write(content, 0, content.Length);
                }
            }
            else if (method == Method.GET)
            {
                request = (HttpWebRequest)WebRequest.Create(resourceUrl + "?"
                    + requestParameters.ToWebString());
                request.Method = method.ToString();
            }

            if (request != null)
            {
                var authHeader = CreateHeader(resourceUrl, method, requestParameters);
                request.Headers.Add("Authorization", authHeader);
                var response = await request.GetResponseAsync();

                using (var sd = new StreamReader(response.GetResponseStream()))
                {
                    resultString = sd.ReadToEnd();
                    response.Close();
                }
            }

            return resultString;
        }

        private string CreateOauthNonce()
        {
            return this.rnd.Next(2222222, 9999999).ToString();
        }

        private string CreateHeader(string resourceUrl, Method method,
                                    SortedDictionary<string, string> requestParameters)
        {
            var oauthNonce = CreateOauthNonce();
            // Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var oauthTimestamp = CreateOAuthTimestamp();
            var oauthSignature = CreateOauthSignature
            (resourceUrl, method, oauthNonce, oauthTimestamp, requestParameters);

            //The oAuth signature is then used to generate the Authentication header.
            const string headerFormat = "OAuth oauth_nonce=\"{0}\",oauth_signature_method=\"{1}\", " +
                                        "oauth_timestamp=\"{2}\", oauth_consumer_key =\"{3}\", " +
                                        "oauth_token=\"{4}\", oauth_signature =\"{5}\", " +
                                        "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                           Uri.EscapeDataString(oauthNonce),
                                           Uri.EscapeDataString(OauthSignatureMethod),
                                           Uri.EscapeDataString(oauthTimestamp),
                                           Uri.EscapeDataString(consumerKey),
                                           Uri.EscapeDataString(accessToken),
                                           Uri.EscapeDataString(oauthSignature),
                                           Uri.EscapeDataString(OauthVersion)
                );

            return authHeader;
        }

        private string CreateOauthSignature
        (string resourceUrl, Method method, string oauthNonce, string oauthTimestamp,
                                            SortedDictionary<string, string> requestParameters)
        {
            //firstly we need to add the standard oauth parameters to the sorted list
            requestParameters.Add("oauth_consumer_key", consumerKey);
            requestParameters.Add("oauth_nonce", oauthNonce);
            requestParameters.Add("oauth_signature_method", OauthSignatureMethod);
            requestParameters.Add("oauth_timestamp", oauthTimestamp);
            requestParameters.Add("oauth_token", accessToken);
            requestParameters.Add("oauth_version", OauthVersion);

            var sigBaseString = requestParameters.ToWebString();

            var signatureBaseString = string.Concat
            (method.ToString(), "&", Uri.EscapeDataString(resourceUrl), "&",
                                Uri.EscapeDataString(sigBaseString.ToString()));

            //Using this base string, we then encrypt the data using a composite of the 
            //secret keys and the HMAC-SHA1 algorithm.
            var compositeKey = string.Concat(Uri.EscapeDataString(consumerKeySecret), "&",
                                             Uri.EscapeDataString(accessTokenSecret));

            string oauthSignature;
            using (var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauthSignature = Convert.ToBase64String(
                    hasher.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString)));
            }

            return oauthSignature;
        }

        private static string CreateOAuthTimestamp()
        {

            var nowUtc = DateTime.UtcNow;
            var timeSpan = nowUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            return timestamp;
        }
    }

    public enum Method
    {
        POST,
        GET
    }

    public static class Extensions
    {
        public static string ToWebString(this SortedDictionary<string, string> source)
        {
            var body = new StringBuilder();

            foreach (var requestParameter in source)
            {
                body.Append(requestParameter.Key);
                body.Append("=");
                body.Append(Uri.EscapeDataString(requestParameter.Value));
                body.Append("&");
            }

            //remove trailing '&'
            body.Remove(body.Length - 1, 1);

            return body.ToString();
        }
    }
}