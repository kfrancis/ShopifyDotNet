#region License, Terms and Conditions
//
// ShopifyException.cs
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
    using System;
    using System.Runtime.Serialization;
    #endregion

    /// <summary>
    /// Shopify exception
    /// </summary>
    [Serializable]
    internal class ShopifyException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ShopifyException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ShopifyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ShopifyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ShopifyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}