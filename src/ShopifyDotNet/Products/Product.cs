#region License, Terms and Conditions
//
// Product.cs
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

namespace ShopifyDotNet.Products
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// A product variant is a different version of a product, such as differing sizes or differing colors.
    /// </summary>
    public class Variant
    {
        /// <summary>
        /// The unique numeric identifier for the product variant.
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// The unique numeric identifier for the product.
        /// </summary>
        public long product_id { get; set; }

        /// <summary>
        /// The title of the product variant.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// A unique identifier for the product in the shop.
        /// </summary>
        public string sku { get; set; }

        /// <summary>
        /// The order of the product variant in the list of product variants. 1 is the first position.
        /// </summary>
        public int position { get; set; }

        /// <summary>
        /// The weight of the product variant in grams.
        /// </summary>
        public int grams { get; set; }

        /// <summary>
        /// Specifies whether or not customers are allowed to place an order for a product variant when it's out of stock. Valid values are: "deny" (default) or "continue"
        /// </summary>
        public string inventory_policy { get; set; }

        /// <summary>
        /// Service who is doing the fulfillment. Valid values are: manual,
        /// </summary>
        public string fulfillment_service { get; set; }

        /// <summary>
        /// Specifies whether or not Shopify tracks the number of items in stock for this product variant. Valid values are: "blank" or "shopify"
        /// </summary>
        public string inventory_management { get; set; }

        /// <summary>
        /// The price of the product variant.
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// The competitors prices for the same item.
        /// </summary>
        public object compare_at_price { get; set; }

        /// <summary>
        /// Custom properties that a shop owner can use to define product variants. Multiple options can exist.
        /// </summary>
        public string option1 { get; set; }

        /// <summary>
        /// Custom properties that a shop owner can use to define product variants. Multiple options can exist.
        /// </summary>
        public object option2 { get; set; }

        /// <summary>
        /// Custom properties that a shop owner can use to define product variants. Multiple options can exist.
        /// </summary>
        public object option3 { get; set; }

        /// <summary>
        /// The date and time when the product variant was created. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime created_at { get; set; }

        /// <summary>
        /// The date and time when the product variant was last modified. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime updated_at { get; set; }

        /// <summary>
        /// Specifies whether or not a tax is charged when the product variant is sold.
        /// </summary>
        public bool taxable { get; set; }

        /// <summary>
        /// Specifies whether or not a customer needs to provide a shipping address when placing an order for this product variant.
        /// </summary>
        public bool requires_shipping { get; set; }

        /// <summary>
        /// The barcode, UPC or ISBN number for the product.
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// The number of items in stock for this product variant.
        /// </summary>
        public int inventory_quantity { get; set; }

        /// <summary>
        /// The original stock level the client believes the product variant has. This should be sent to avoid a race condition when the item being adjusted is simultaneously sold online.
        /// </summary>
        public int old_inventory_quantity { get; set; }

        /// <summary>
        /// The identifier of the image
        /// </summary>
        public long? image_id { get; set; }

        /// <summary>
        /// The weight of the product variant in the unit system specified with weight_unit.
        /// </summary>
        public double weight { get; set; }

        /// <summary>
        /// The unit system that the product variant's weight is measure in. The weight_unit can be either "g", "kg, "oz", or "lb".
        /// </summary>
        public string weight_unit { get; set; }
    }

    /// <summary>
    /// The product option
    /// </summary>
    public class Option
    {
        /// <summary>
        /// The option identifier
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// The identifier of the product the option belongs to
        /// </summary>
        public long product_id { get; set; }

        /// <summary>
        /// The name of the option
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The option position
        /// </summary>
        public int position { get; set; }

        /// <summary>
        /// The values associated with the option
        /// </summary>
        public List<string> values { get; set; }
    }

    /// <summary>
    /// Products are easier to sell if customers can see pictures of them, which is why there are product images.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// A unique numeric identifier for the product image.
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// The id of the product associated with the image.
        /// </summary>
        public long product_id { get; set; }

        /// <summary>
        /// The order of the product image in the list. The first product image is at position 1 and is the "main" image for the product.
        /// </summary>
        public int position { get; set; }

        /// <summary>
        /// The date and time when the product image was created. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime created_at { get; set; }

        /// <summary>
        /// The date and time when the product image was last modified. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime updated_at { get; set; }

        /// <summary>
        /// Specifies the location of the product image.
        /// </summary>
        public string src { get; set; }

        /// <summary>
        /// An array of variant ids associated with the image
        /// </summary>
        public List<long> variant_ids { get; set; }
    }

    /// <summary>
    /// A list of products
    /// </summary>
    public class ProductList
    {
        /// <summary>
        /// The list of products
        /// </summary>
        public List<Product> products { get; set; }
    }

    /// <summary>
    /// A product is an individual item for sale in a Shopify shop. Products are often physical, but don't have to be; 
    /// a digital download (such as a movie, music or ebook file) also qualifies as a product, as do services (such as
    /// equipment rental, work for hire, customization of another product or an extended warranty). Simply put: if 
    /// it's something for sale in a shop, it's a Product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The unique numeric identifier for the product. Product ids are unique across the entire Shopify system; no two products will have the same id, even if they're from different shops.
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// The name of the product. In a shop's catalog, clicking on a product's title takes you to that product's page. On a product's page, the product's title typically appears in a large font.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// The description of the product, complete with HTML formatting.
        /// </summary>
        public string body_html { get; set; }

        /// <summary>
        /// The name of the vendor of the product.
        /// </summary>
        public string vendor { get; set; }

        /// <summary>
        /// A categorization that a product can be tagged with, commonly used for filtering and searching.
        /// </summary>
        public string product_type { get; set; }

        /// <summary>
        /// The date and time when the product was created. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime created_at { get; set; }

        /// <summary>
        /// A human-friendly unique string for the Product automatically generated from its title. They are used by the Liquid templating language to refer to objects.
        /// </summary>
        public string handle { get; set; }

        /// <summary>
        /// The date and time when the product was last modified. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime updated_at { get; set; }

        /// <summary>
        /// The date and time when the product was published. The API returns this value in ISO 8601 format.
        /// </summary>
        public DateTime published_at { get; set; }

        /// <summary>
        /// The suffix of the liquid template being used. By default, the original template is called product.liquid, without any suffix. Any additional templates will be: product.suffix.liquid.
        /// </summary>
        public object template_suffix { get; set; }

        /// <summary>
        /// The sales channels in which the product is visible.
        /// </summary>
        public string published_scope { get; set; }

        /// <summary>
        /// A categorization that a product can be tagged with, commonly used for filtering and searching. Each comma-separated tag has a character limit of 255.
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// A list of variant objects, each one representing a slightly different version of the product. For example, if a product comes in different sizes and colors, each size and color permutation (such as "small black", "medium black", "large blue"), would be a variant.
        /// </summary>
        public List<Variant> variants { get; set; }

        /// <summary>
        /// Custom product property names like "Size", "Color", and "Material". Products are based on permutations of these options. A product may have a maximum of 3 options. 255 characters limit each.
        /// </summary>
        public List<Option> options { get; set; }

        /// <summary>
        /// A list of image objects, each one representing an image associated with the product.
        /// </summary>
        public List<object> images { get; set; }

        /// <summary>
        /// The product image information
        /// </summary>
        public Image image { get; set; }
    }
}
