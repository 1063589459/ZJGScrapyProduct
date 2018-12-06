using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrapy.Util;
using Server.Core;
using Server.Core.Data;

namespace Scrapy.DB
{
    public class Db
    {
        private static string con = Env.Settings.DbUrl;
        private static readonly string Host = Guid.NewGuid().ToString().Substring(0, 8);
        private static readonly ConcurrentDictionary<DbIdentity, IDbClient> KnownClients
            = new ConcurrentDictionary<DbIdentity, IDbClient>();

        public static IDbClient KuaiJieChong { get { return GetClient("kjc_config", DatabaseType.MySql, con); } }


        public static IDbClient GetClient(string dbName, DatabaseType databaseType, string connectionString = null)
        {
            ArgAssert.NotNullOrEmpty(dbName, "dbName");

            var id = new DbIdentity(dbName, databaseType);
            var client = KnownClients.GetOrAdd(id, x =>
            {
                var conn = connectionString ?? GetConnectionString(dbName, databaseType);
                return databaseType == DatabaseType.MySql
                    ? (IDbClient)new MysqlDbClient(conn)
                    : (IDbClient)new SqlDbClient(conn);
            });

            return client;
        }

        public static string GetConnectionString(string dbName, DatabaseType databaseType)
        {
            return "";
        }

        private struct DbIdentity : IEquatable<DbIdentity>
        {
            private readonly string _name;
            private readonly DatabaseType _type;

            public DbIdentity(string name, DatabaseType type)
            {
                _name = name;
                _type = type;
            }

            public bool Equals(DbIdentity other)
            {
                return _name == other._name && _type == other._type;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is DbIdentity))
                    return false;

                return Equals((DbIdentity)obj);
            }

            public override int GetHashCode()
            {
                return _name.GetHashCode() ^ _type.GetHashCode();
            }
        }
    }

    public enum DatabaseType
    {
        SqlServer = 1,
        MySql = 2,
        MySqlReadOnly = 21
    }
}
