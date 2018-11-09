using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model
{
    public class ProductInfo
    {
        public string skuId { get; set; }
        public string skuName { get; set; }
        public string imgUrl { get; set; }
        public string storeId { get; set; }
        public string orgCode { get; set; }
        public string userActionSku { get; set; }
        public string realTimePrice { get; set; }
    }
}