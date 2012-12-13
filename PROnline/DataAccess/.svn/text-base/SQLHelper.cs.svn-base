using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Collections;
using System.Configuration;

namespace PROnline.DataAccess
{
    /// <summary>
    /// 连接数据库并提供访问数据库的方法
    /// </summary>
    public class SqlHelper
    {
        public MySqlConnection conn;
        MySqlDataAdapter adap;
//        public static string ConnectionString = "workstation id=\"127.0.0.1\";integrated security=SSPI;data source=\"" +
//                ".\";persist security info=False;Initial Catalog=db_03";

        //public static string ConnectionString="Password=591891520;User ID=root;datasource=192.168.1.6;database=openmeetings;charset=utf8;Allow Zero Datetime=true";

        //public static string ConnectionString="Password=591891520;User ID=root;datasource=192.168.1.7;database=openmeetings;charset=utf8;Allow Zero Datetime=true";

//        public static string ConnectionString = "database=db_SoftDb;Server=10.0.0.35,1433;User ID=sa;Password=;Persist Security Info=True";

        //        public static string ConnectionString = "workstation id=\"127.0.0.1\";integrated security=SSPI;data source=\"" +
        //                ".\";persist security info=False;Initial Catalog=db_03";
        static string connStr = ConfigurationSettings.AppSettings["mysqlConnectionString"];
        public static string ConnectionString =connStr;

      //  public static string ConnectionString = "Password=591891520;User ID=root;datasource=localhost;database=openmeetings4;charset=utf8;Allow Zero Datetime=true";
        //        public static string ConnectionString = "database=db_SoftDb;Server=10.0.0.35,1433;User ID=sa;Password=;Persist Security Info=True";

        public SqlHelper()
        {
            //string  saConnectionString="server=.;database=GroggeryDB;uid=sa;pwd=aptech";
            try
            {
                this.conn = new MySqlConnection(ConnectionString);
            }
            catch
            {
                throw new ApplicationException("数据库连接操作失败!");
            }
        }

        /// <summary>
        /// 双向操作
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <param name="dataset">数据集</param>
        public void Select(string cmdText, DataTable datatable)
        {
            datatable.Clear();
            try
            {
                adap = new MySqlDataAdapter(cmdText, this.conn);
                adap.Fill(datatable);
            }
            catch
            {
                throw new ApplicationException("数据库select操作失败!");
            }
            finally
            {
                if (this.conn != null)
                    this.conn.Close();
            }
        }

        /// <summary>
        /// 返回select语句是否存在影响行的操作
        /// </summary>
        /// <param name="cmdText">更新语句</param>
        /// <returns></returns>
        public bool sel_num(string cmdText)
        {
            bool bl = false;
            try
            {
                MySqlCommand command = new MySqlCommand(cmdText, this.conn);
                this.conn.Open();
                MySqlDataReader sdr = command.ExecuteReader();
                if (sdr.HasRows == true)
                {
                    bl = true;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("数据库select操作失败!");
            }
            finally
            {
                if (this.conn != null)
                    this.conn.Close();
            }
            return bl;
        }

        /// <summary>
        /// 返回select语句的所有行
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>记录数</returns>
        public int Select_num(string cmdText)
        {
            int count = 0;
            try
            {
                MySqlCommand command = new MySqlCommand(cmdText, this.conn);
                this.conn.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw new ApplicationException("数据库select操作失败!");
            }
            finally
            {
                if (this.conn != null)
                    this.conn.Close();
            }
            return count;
        }
        /// <summary>
        /// 返回select语句是否存在影响行
        /// </summary>
        /// <param name="cmdText">更新语句</param>
        /// <returns></returns>
        public int sel_number(string cmdText)
        {
            int num = 0;
            try
            {
                MySqlCommand command = new MySqlCommand(cmdText, this.conn);
                this.conn.Open();
                MySqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    num++;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("数据库select操作失败!", ex);
            }
            finally
            {
                if (this.conn != null)
                    this.conn.Close();
            }
            return num;
        }
        /// <summary>
        /// 双向操作
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <return name="DataSet">数据集</return>
        public DataSet GetDataSet(string sql)
        {
            if (sql == "" || sql == null)
            {
                throw new Exception("输入参数错误！");
            }
            DataSet ds = new DataSet();
            try
            {
                adap = new MySqlDataAdapter(sql, conn);
                adap.Fill(ds);
            }
            catch (Exception)
            {
                throw new ApplicationException("数据库操作失败!");

            }
            finally
            {
                if (this.conn != null)
                    this.conn.Close();
            }
            return ds;
        }
        /// <summary>
        /// 返回DataSet类型的数据集对象
        /// </summary>
        /// <param name="cmdText">更新语句</param>
        /// <returns></returns>
        public DataSet GetDs(string sql, string table)
        {
            if (sql == "" || sql == null)
            {
                throw new Exception("输入参数错误！");
            }
            DataSet ds = new DataSet();
            try
            {
                adap = new MySqlDataAdapter(sql, conn);
                adap.Fill(ds, table);

            }
            catch (Exception)
            {
                throw new ApplicationException("数据库操作失败!");
            }
            finally
            {
                if (this.conn != null)
                    this.conn.Close();
            }
            return ds;
        }
        /// <summary>
        /// 返回DataReader类对象
        /// </summary>
        /// <param name="cmdText">更新语句</param>
        /// <returns></returns>
        public MySqlDataReader GetRead(string sql)
        {
            if (sql == "" || sql == null)
            {
                throw new Exception("输入参数错误！");
            }

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader read;
            try
            {
                read = cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (Exception)
            {
                throw new ApplicationException("数据库操作失败!");
            }
            return read;
        }


        /// <summary>
        /// 单向操作
        /// </summary>
        /// <param name="cmdText">查询是否存在语句</param>
        /// <returns></returns>
        public bool IsExist(string cmdText)
        {
            bool result = false;
            if (cmdText == null || cmdText == "")
            {
                throw new Exception("输入参数错误！");

            }
            try
            {
                MySqlCommand command = new MySqlCommand(cmdText, this.conn);
                conn.Open();
                MySqlDataReader dt = command.ExecuteReader();
                if (dt.Read())
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("数据库操作失败!");
            }

            if (conn != null)
                conn.Close();
            return result;
        }
        /// <summary>
        /// 批量更新 整表数据提交
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public bool UpdateAll(DataTable dt, string strSql)
        {
            bool ret = false;
            DataTable dtUpdate = new DataTable();
            Select(strSql, dtUpdate);
            dtUpdate.Rows.Clear();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtUpdate.ImportRow(dt.Rows[i]);
            }


            MySqlDataAdapter adp = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(strSql, conn);
            adp.SelectCommand = cmd;
            conn.Open();
            MySqlTransaction trans = conn.BeginTransaction();
            cmd.Transaction = trans;
            MySqlCommandBuilder comb = new MySqlCommandBuilder(adp);
            try
            {
                adp.Update(dtUpdate);
                trans.Commit();
                ret = true;
            }
            catch (Exception)
            {
                trans.Rollback();
                throw new ApplicationException("数据库操作失败!");
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }
        /// <summary>
        /// 单向操作
        /// </summary>
        /// <param name="cmdText">更新语句</param>
        /// <returns></returns>
        public bool Update(string cmdText)
        {
            bool bl = false;
            conn.Open();
            MySqlTransaction myTrans = conn.BeginTransaction();//new生成一个新事务
            MySqlCommand command = new MySqlCommand(cmdText, conn);
            try
            {
                command.Transaction = myTrans;
                if (command.ExecuteNonQuery() > 0)
                {
                    myTrans.Commit();
                    bl = true;
                }
            }
            catch (Exception)
            {
                myTrans.Rollback();
                throw new ApplicationException("数据库操作失败!");
            }
            finally
            {
                conn.Close();
                myTrans.Dispose();
            }
            return bl;
        }
        #region IDisposable 成员

        public void Dispose()
        {
            if (this.conn != null)
                this.conn.Dispose();
            if (adap != null)
                adap.Dispose();
        }

        #endregion
    }
}