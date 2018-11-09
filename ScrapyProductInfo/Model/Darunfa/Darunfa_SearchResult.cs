using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model.Darunfa
{
    public class Darunfa_SearchResult<T>
    {
        public int errorCode;
        public string errorDesc;
        public T body;
    }
}