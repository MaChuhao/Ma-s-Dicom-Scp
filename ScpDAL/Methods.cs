using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScpModel;
using System.Data.SqlClient;
using System.Data;

namespace MCHDAL
{
    public class Methods
    {
        TransToModel ttm = new TransToModel();

        public Int32 Save<T>(T model, string tableName) where T : new()
        {
            string sqlStr = CreateSQLStr.SaveSQLStr<T>(model, tableName);

            using (SqlCommand sc = new SqlCommand(sqlStr, DBHelper.conn))
            {
                DBHelper.conn.Open();
                int result = sc.ExecuteNonQuery();
                DBHelper.conn.Close();
                return result;
            }
        }

        public int Update<T>(T model, string tableName) where T : new()
        {
            string sqlStr = CreateSQLStr.UpdateSQLStr<T>(model, tableName);
            if (sqlStr.Equals("false"))
            {
                return 0;
            }
            else
            {
                using (SqlCommand sc = new SqlCommand(sqlStr, DBHelper.conn))
                {
                    DBHelper.conn.Open();
                    int result = sc.ExecuteNonQuery();
                    DBHelper.conn.Close();
                    return result;
                }
            }
            
        }

        public int Delete<T>(T model, string tableName) where T : new()
        {
            string sqlStr = CreateSQLStr.DeleteSQLStr<T>(model, tableName);
            if (sqlStr.Equals("false"))
            {
                return 0;
            }
            else
            {
                using (SqlCommand sc = new SqlCommand(sqlStr, DBHelper.conn))
                {
                    DBHelper.conn.Open();
                    int result = sc.ExecuteNonQuery();
                    DBHelper.conn.Close();
                    return result;
                }
            }

        }

        public List<T> GetList<T>(T model, string tableName) where T : new()
        {
            string sqlStr = CreateSQLStr.SelectSQLStr<T>(model, tableName);

            return ttm.GetList<T>(sqlStr);
        }
    }
}
