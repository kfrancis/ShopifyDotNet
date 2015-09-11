#region License, Terms and Conditions
//
// ApiBase.cs
//
// Authors: Kori Francis <twitter.com/djbyter>
// Copyright (C) 2015 Kori Francis. All rights reserved.
// 
// THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//
#endregion

namespace ShopifyDotNet
{
    #region Imports
    using RestSharp;
    using System;
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// The service base class
    /// </summary>
    public class ApiBase
    {
        #region Properties and Accessors
        /// <summary>
        /// The permanent token retrieved from oAuth authentication
        /// </summary>
        protected string AuthToken { get; private set; }

        /// <summary>
        /// The subdomain part of a shopify store url, ie. if the url is https://test-store-1234.myshopify.com then the ShopId would be test-store-1234
        /// </summary>
        protected string AuthShopId { get; private set; }

        /// <summary>
        /// The user agent
        /// </summary>
        private static string _userAgent;

        /// <summary>
        /// The user agent identifier of this library, which includes the assembly version.
        /// </summary>
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
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authToken">The permanent authToken recieved from Shopify</param>
        /// <param name="shopId">The shopId (subdomain) identifier</param>
        protected ApiBase(string authToken, string shopId)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new ArgumentException("Token is required. Details can be found at https://docs.shopify.com/api", "authToken");

            if (string.IsNullOrEmpty(shopId))
                throw new ArgumentException("Shop identifier is required. Details can be found at https://docs.shopify.com/api", "shopId");

            this.AuthToken = authToken;
            this.AuthShopId = shopId;
        }
        #endregion

        #region Request Methods
        /// <summary>
        /// Get request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected T GetRequest<T>(string path, params object[] args) where T : new()
        {
            if (string.IsNullOrEmpty(this.AuthShopId) || this.AuthShopId.Replace("\0", "").Trim().Length <= 0) { throw new ArgumentNullException("AuthShopId"); }

            var client = new RestClient(string.Format("https://{0}.myshopify.com", this.AuthShopId));
            client.AddDefaultHeader("X-Shopify-Access-Token", this.AuthToken);
            client.UserAgent = ApiBase.UserAgent;
            var request = new RestRequest(BuildUrl(path, args));
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new RestSharp.Serializers.JsonSerializer();
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

        /// <summary>
        /// Get request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns>The string response</returns>
        protected string GetRequest(string path, params object[] args)
        {
            var client = new RestClient(string.Format("https://{0}.myshopify.com", this.AuthShopId));
            client.AddDefaultHeader("X-Shopify-Access-Token", this.AuthToken);
            client.UserAgent = ApiBase.UserAgent;
            var request = new RestRequest(BuildUrl(path, args));
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new RestSharp.Serializers.JsonSerializer();
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

        /// <summary>
        /// Put request
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected TOutput PutRequest<TOutput, TInput>(TInput obj, string path, params object[] args) where TOutput : new()
        {
            return Request<TOutput, TInput>(Method.PUT, obj, path, args);
        }

        /// <summary>
        /// Post request
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected TOutput PostRequest<TOutput, TInput>(TInput obj, string path, params object[] args) where TOutput : new()
        {
            return Request<TOutput, TInput>(Method.POST, obj, path, args);
        }

        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        protected void DeleteRequest(string path, params object[] args)
        {
            Request<List<object>, object>(Method.DELETE, null, path, args);
        }

        /// <summary>
        /// Generic request
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="method"></param>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private TOutput Request<TOutput, TInput>(Method method, TInput obj, string path, params object[] args) where TOutput : new()
        {
            var client = new RestClient(string.Format("https://{0}.myshopify.com", this.AuthShopId));
            client.AddDefaultHeader("X-Shopify-Access-Token", this.AuthToken);
            client.UserAgent = ApiBase.UserAgent;
            var request = new RestRequest(BuildUrl(path, args), method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new RestSharp.Serializers.JsonSerializer();
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
        #endregion

        #region Helpers
        /// <summary>
        /// Url formatter
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected string BuildUrl(string path, params object[] args)
        {
            // Assume for now that no querystring is added
            path = string.Format(path, args);

            return path;
        }
        #endregion
    }
}
