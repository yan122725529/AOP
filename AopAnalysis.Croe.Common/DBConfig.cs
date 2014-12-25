using System.IO;
using System.Xml.Serialization;

namespace AopAnalysis.Croe.Common
{
    public  class DBConfig
    {
        public string ConnectString { get; set; }
        public string DbType { get; set; }
        public static DBConfig LoadData(string file)
        {
            var xs = new XmlSerializer(typeof(DBConfig));
            var sr = new StreamReader(file);
            var entity = xs.Deserialize(sr) as DBConfig;
            sr.Close();
            return entity;
        }

        public static DBConfig New()
        {
            var siteSetting = new DBConfig();
            return siteSetting;
        }
        
    }
}
