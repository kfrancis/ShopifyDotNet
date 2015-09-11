using ShopifyDotNet.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyDotNet
{
    public class Client
    {
        public ProductService Products { get; private set; }

        public Client(string authToken, string shopId)
        {
            this.Products = new ProductService(authToken, shopId);
        }
    }
}
