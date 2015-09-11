using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyDotNet
{
    public class ApiBase
    {
        #region Properties and Accessors
        protected string AuthToken { get; private set; }
        protected string AuthShopId { get; private set; }

        private static string _userAgent;
        private static string UserAgent
        {
            get
            {
                if (_userAgent == null)
                {
                    _userAgent = String.Format("ShopifyDotNet Client v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                }
                return _userAgent;
            }
        }
        #endregion

        #region Constructor
        protected ApiBase(string authToken, string userId)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new ArgumentException("Token is required. Details can be found at https://docs.shopify.com/api", "authToken");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Shop identifier is required. Details can be found at https://docs.shopify.com/api", "userId");

            this.AuthToken = authToken;
            this.AuthShopId = userId;
        }
        #endregion

        protected T GetRequest<T>(string path, params object[] args) where T : new()
        {
            if (string.IsNullOrEmpty(this.AuthShopId) || this.AuthShopId.Replace("\0", "").Trim().Length <= 0) { throw new ArgumentNullException("AuthShopId"); }

            var client = new RestClient(string.Format("https://{0}.myshopify.com", this.AuthShopId));
            client.AddDefaultHeader("X-Shopify-Access-Token", this.AuthToken);
            client.UserAgent = ApiBase.UserAgent;
            var request = new RestRequest(BuildUrl(path, args));
            request.RequestFormat = DataFormat.Xml;
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            var response = client.Execute<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ShopifyException("Not Found");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new ShopifyException("Internal Server Error");
            }

            return response.Data;
        }

        protected string GetRequest(string path, params object[] args)
        {
            var client = new RestClient(string.Format("https://{0}.myshopify.com", this.AuthShopId));
            client.AddDefaultHeader("X-Shopify-Access-Token", this.AuthToken);
            client.UserAgent = ApiBase.UserAgent;
            var request = new RestRequest(BuildUrl(path, args));
            request.RequestFormat = DataFormat.Xml;
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ShopifyException("Not Found");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new ShopifyException("Internal Server Error");
            }

            return response.Content;
        }

        protected TOutput PutRequest<TOutput, TInput>(TInput obj, string path, params object[] args) where TOutput : new()
        {
            return Request<TOutput, TInput>(Method.PUT, obj, path, args);
        }

        protected TOutput PostRequest<TOutput, TInput>(TInput obj, string path, params object[] args) where TOutput : new()
        {
            return Request<TOutput, TInput>(Method.POST, obj, path, args);
        }

        protected void DeleteRequest(string path, params object[] args)
        {
            Request<List<object>, object>(Method.DELETE, null, path, args);
        }

        private TOutput Request<TOutput, TInput>(Method method, TInput obj, string path, params object[] args) where TOutput : new()
        {
            var client = new RestClient(string.Format("https://{0}.myshopify.com", this.AuthShopId));
            client.AddDefaultHeader("X-Shopify-Access-Token", this.AuthToken);
            client.UserAgent = ApiBase.UserAgent;
            var request = new RestRequest(BuildUrl(path, args), method);
            request.RequestFormat = DataFormat.Xml;
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            if (obj != null)
            {
                request.AddBody(obj);
            }

            var response = client.Execute<TOutput>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ShopifyException("Not Found");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new ShopifyException("Internal Server Error");
            }

            return response.Data;
        }

        protected string BuildUrl(string path, params object[] args)
        {
            // Assume for now that no querystring is added
            path = string.Format(path, args);

            return path;
        }
    }
}
