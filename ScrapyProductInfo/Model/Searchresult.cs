using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model
{
    public class Searchresult
    {
        public string solution;
        public string templeType;
        public bool showSearch;
        public bool showCart;
        public List<ProductInfo> searchResultVOList;
    }
}