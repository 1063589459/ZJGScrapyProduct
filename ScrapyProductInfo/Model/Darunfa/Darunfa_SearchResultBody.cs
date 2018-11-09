using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model.Darunfa
{
    public class Darunfa_SearchResultBody
    {
        public int total;
        public float totalPageCount;
        public string delivery_tips;
        public List<Darunfa_ProductInfo> MerchandiseList;
    }
}