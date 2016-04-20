using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScpModel;
using System.Data.SqlClient;

namespace MCHDAL
{
    public class Methods
    {
        TransToModel ttm = new TransToModel();

        public int Save<T>(T model, string tableName) where T : new()
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

        public List<Pathmodel> GetPath()
        {
            return ttm.GetList<Pathmodel>("Select * from tb_path");
        }
    }
}
