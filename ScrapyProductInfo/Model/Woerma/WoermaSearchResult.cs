using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model.Woerma
{
    public class WoermaSearchResult<T>
    {
        public int code;
        public string msg;
        public T result;
        public bool success;
    }
}