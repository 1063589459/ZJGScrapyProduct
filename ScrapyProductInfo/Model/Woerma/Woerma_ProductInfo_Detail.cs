using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model.Woerma
{
    public class Woerma_ProductInfo_Detail
    {
        public string skuId;
        public string name;
        public List<Woerma_ProductInfo_Detail_Img> image;
    }
}