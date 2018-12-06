using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapy.Util
{
    public class Env
    {
        public static class Settings
        {
            public static string DbUrl = GetAppSetting("dbUrl");
        }

        private static readonly ConcurrentDictionary<string, string> AppSttings = new ConcurrentDictionary<string, string>();

        private static string GetAppSetting(string key, Func<string, string> trans = null, bool forceLoad = true)
        {
            var value = AppSttings.GetOrAdd(key, x =>
            {
                var v = ConfigurationManager.AppSettings[x];
                if (trans != null)
                {
                    v = trans(v);
                }
                return v;
            });

            if (forceLoad && String.IsNullOrWhiteSpace(value))
            {
                var msg = String.Format(
                    "The app-setting value for key '{0}' is not configured.",
                    key);
                throw new ConfigurationErrorsException(msg);
            }

            return value;
        }
    }
}
