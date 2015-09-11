using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyDotNet.Products
{
    public class ProductService : ApiBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="shopId"></param>
        public ProductService(string authToken, string shopId)
            : base(authToken, shopId)
        { }

        public IEnumerable<Product> All()
        {
            return GetRequest<List<Product>>("/admin/products.json");
        }
    }
}
