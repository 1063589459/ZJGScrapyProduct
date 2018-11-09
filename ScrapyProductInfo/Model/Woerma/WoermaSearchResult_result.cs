using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model.Woerma
{
    public class WoermaSearchResult_result
    {
        public int count;
        public int page;
        public List<Woerma_ProductInfo> searchResultVOList;
    }
}