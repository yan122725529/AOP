using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using AopAnalysis.Croe.Common;

namespace AopAnalysis.Croe.DbHelpers
{
    public abstract class SqlHelper : IDisposable
    {
        private static DbConnection dbConnection { set; get; }
        private static DbProviderFactory DbProvider { set; get; }

        static SqlHelper()
        {
            dbConnection = DbConnectionHelper.GetConnection();
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();


            if (DbConnectionHelper.DbType == "sqlite")
            {
                DbProvider = System.Data.SQLite.SQLiteFactory.Instance;
            }
            else
            {
                DbProvider = System.Data.SqlClient.SqlClientFactory.Instance;
            }
        }

        /// </summary>
        /// 以事务执行sql语句列表，返回事务执行是否成功
        /// <param name="cmdTextes">sql语句列表</param>
        /// <param name="commandParameterses">sql语句列表对应的参数列表，参数列表必须与sql语句列表匹配</param>
        /// <returns>事务执行是否成功</returns>
        public static bool ExecuteTransaction( List<string> cmdTextes)
        {
            bool flag = false;

            DbTransaction sqlTran = dbConnection.BeginTransaction();
            try
            {
                for (int i = 0; i < cmdTextes.Count; i++)
                {
                    ExecuteNonQuery(sqlTran, CommandType.Text, cmdTextes[i]);
                }
                sqlTran.Commit();
                flag = true;
            }
            catch (Exception e)
            {
                sqlTran.Rollback();
            }
            return flag;
        }


        /// <summary>
        /// 构造SQLCommand
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="conn">SqlConnection数据库连接对象</param>
        /// <param name="trans">SQL事务</param>
        /// <param name="cmdType">Command命令类型</param>
        /// <param name="cmdText">SQL语句或存储过程名称</param>
        /// <param name="cmdParms">参数数组</param>
        private static void PrepareCommand(DbCommand cmd, DbTransaction trans, CommandType cmdType, string cmdText)
        {

            cmd.Connection = dbConnection;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

        }

        public static int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText)
        {
            DbCommand cmd = DbProvider.CreateCommand();
            PrepareCommand(cmd, trans, cmdType, cmdText);
            int val = cmd.ExecuteNonQuery();
            return val;
        }

        /// <summary>
        /// 执行SQL，返回DataTable
        /// 这里默认使用外面传入的conn对象，使用完成后不会对conn对象进行释放，需要自己在外面进行数据库连接释放
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">Command命令类型，SQL语句或存储过程</param>
        /// <param name="cmdText">SQL语句或存储过程名称</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable( CommandType cmdType, string cmdText)
        {
            DataTable dt = new DataTable();
            DbCommand cmd = DbProvider.CreateCommand();
            PrepareCommand(cmd, null, cmdType, cmdText);

            using (DbDataAdapter sda = DbProvider.CreateDataAdapter())
            {
                sda.SelectCommand = cmd;
                sda.Fill(dt);
            }
            return dt;
        }
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                    dbConnection = null;
                }
        }

        ~SqlHelper()
        {
            Dispose(false);
        }
    }
}
