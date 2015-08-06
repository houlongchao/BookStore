using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace System.Data
{
    public static class SqlHelper
    {
        //连接字符串
        private static readonly string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        //封装常用方法
        
        public static int ExecuteNonQuery(string sql,CommandType cmdType,params SqlParameter[] pms)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd  = new SqlCommand(sql,conn))
                {
                    cmd.CommandType = cmdType;
                    if (pms!=null)
                    {
                        cmd.Parameters.AddRange(pms);  
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql,CommandType cmdType, params SqlParameter[] pms)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql,conn))
                {
                    cmd.CommandType = cmdType;
                    if (pms!=null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType , params SqlParameter[] pms)
        {
            SqlConnection conn = new SqlConnection(connStr);
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = cmdType;
                    if (pms!=null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch
                    {
                        conn.Close();
                        conn.Dispose();
                        throw;
                    }
                }
        }

        //返回DataTable
        public static DataTable ExecuteDatatable(string sql, CommandType cmdtype, params SqlParameter[] pms)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql,connStr))
            {
                adapter.SelectCommand.CommandType = cmdtype;
                if (pms!=null)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                adapter.Fill(dt);
                return dt;
            }
        }

    }
}
