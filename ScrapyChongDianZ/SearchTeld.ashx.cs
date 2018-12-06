using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Scrapy.DB;
using ScrapyChongDianZ.entity;
using Server.Core.Data ;

namespace ScrapyChongDianZ
{
    /// <summary>
    /// SearchTeld 的摘要说明
    /// </summary>
    public class SearchTeld : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AnalayseTeld();
            context.Response.Write("Hello World");
        }


        private void AnalayseTeld()
        {
            var strJson = "";
            using (var sr = new StreamReader(@"F:\MyWork\GitWork\ZJGScrapyProduct\ScrapyChongDianZ\source\teldZhuHai.txt", Encoding.UTF8))
            {
                strJson = sr.ReadToEnd();
            }

            try
            {
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.NullValueHandling = NullValueHandling.Ignore;
                //Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented, jsetting));

                var teldResult = JsonConvert.DeserializeObject<TeldStaList>(strJson, jsetting);



                if (teldResult != null)
                {
                    var lstStation = teldResult.staList;
                    foreach (TeldStation itemStation in lstStation)
                    {
                        var sql =
                            @" insert ignore into station_info(staZLCount,staJLCount,teldID,name,staType,staTypeName,visible,state,stateName,lng,lat,staAddress,cdcs) 
                                 values(@staZLCount,@staJLCount,@teldID,@name,@staType,@staTypeName,@visible,@state,@stateName,@lng,@lat,@staAddress,@cdcs) ";
                        var i = Db.KuaiJieChong.Execute(sql, new
                        {
                            staZLCount = itemStation.STAPILLZLCOUNT,
                            staJLCount = itemStation.STAPILLJLDXCOUNT,
                            teldID = itemStation.ID,
                            name = itemStation.NAME,
                            staType = itemStation.STATYPE,
                            staTypeName = itemStation.STATYPENAME,
                            visible = itemStation.VISIBLE,
                            state = itemStation.STAOPSTATE,
                            stateName = itemStation.STAOPSTATENAME,
                            lng = itemStation.WGS84LNG,
                            lat = itemStation.WGS84LAT,
                            staAddress = itemStation.STAADDR,
                            cdcs = itemStation.CDCS
                        });
                    }
                }
            }
            catch (Exception e)
            {
              Console.WriteLine(e.ToString());
            }
            Console.WriteLine("end...");



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}