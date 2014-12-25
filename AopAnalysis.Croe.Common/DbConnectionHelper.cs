using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

using System.Text;
using System.IO;

namespace AopAnalysis.Croe.Common
{
    public static class DbConnectionHelper
    {
        static DbConnectionHelper()
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            DocumentPath = string.Format(@"{0}\AOP\", documentPath);
            var dBConfigPath = DocumentPath + @"DBConfig.xml";
             var dBPath = DocumentPath + @"Aop_Data";
            DBConfig dBConfig;

            if (File.Exists(dBConfigPath))
            {
                dBConfig = DBConfig.LoadData(dBConfigPath);

                DbType = dBConfig.DbType;
                if (DbType == "sqlite")
                {
                    ConnectString = string.Format(@"Data Source= {0};UTF8Encoding=True;Version=3;Pooling=True",dBPath);
                }
                else
                {
                    ConnectString = dBConfig.ConnectString;
                }
                
            }
            else
            {
                throw new Exception("配置文件路径错误！");
            }

        }

        public static DbConnection GetConnection()
        {
            //return new SqlConnection(ConnectString);
            if (DbType == "sqlite")
            {
                return new SQLiteConnection (ConnectString);
            }
            else
            {
                return new SqlConnection(ConnectString);
            }
            
        }

        

        private static string DocumentPath { get; set; }
        public static string DbType { get; set; }
        private static string ConnectString { get; set; }
       

      
    }
}
