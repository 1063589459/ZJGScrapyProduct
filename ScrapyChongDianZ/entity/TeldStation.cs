using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapyChongDianZ.entity
{
    public class TeldStation
    {
        /// <summary>
        /// 直流
        /// </summary>
        public int STAPILLZLCOUNT;

        /// <summary>
        /// 交流
        /// </summary>
        public int STAPILLJLDXCOUNT;

        public string ID;

        public string CODE;

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME="";

        public string STATYPE="";

        public string STATYPENAME="";

        public string VISIBLE="";

        public string STAOPSTATE="";

        public string STAOPSTATENAME="";

        /// <summary>
        /// 经度
        /// </summary>
        public double WGS84LNG;

        /// <summary>
        /// 纬度
        /// </summary>
        public double WGS84LAT;

        /// <summary>
        /// 地址
        /// </summary>
        public string STAADDR="";

        public int CDCS;

    }
}