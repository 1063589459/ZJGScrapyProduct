using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyProductInfo.Model
{
    public class CommonResult<T>
    {
        public string code { get; set; }
        public string msg { get; set; }

        public T result { get; set; }
    }
}