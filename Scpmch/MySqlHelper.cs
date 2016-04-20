using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom.Network;

namespace Scpmch
{
    class MySqlHelper
    {
        #region  建立MySql数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        public MySqlConnection getmysqlcon()
        {

            string M_str_sqlcon = "server='localhost';user id='root'; password=; port=3306; database=sad; pooling=false; charset=utf8"; //根据自己的设置
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion

        #region  执行MySqlCommand命令
        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void Getmysqlcom(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.getmysqlcon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
        }
        #endregion


        /// <summary>
        /// 创建一个MySqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public MySqlDataReader Getmysqlread(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.getmysqlcon();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }



        public List<PathModel> GetAllData()
        {
            List<PathModel> pathList = new List<PathModel>();
            MySqlDataReader reader = Getmysqlread("select * from tb_path");
            while (reader.Read())
            {
                PathModel path = new PathModel();
                path.Pid = reader.GetString(0);
                path.Path = reader.GetString(1);
                path.StudyInstanceUid = reader.GetString(2);
                pathList.Add(path);
                Console.WriteLine(reader.GetString(0));
                Console.WriteLine(reader.GetString(1));
                Console.WriteLine(reader.GetString(2));
                Console.WriteLine(reader.FieldCount);
            }
           
            return pathList;
        }

        public List<PathModel> GetDataByStudyInstanceUid(String SIUid)
        {
            List<PathModel> pathList = new List<PathModel>();
            MySqlDataReader reader = Getmysqlread("select * from tb_path where studyInstanceUid = '"+SIUid+"';");
            while (reader.Read())
            {
                PathModel path = new PathModel();
                path.Pid = reader.GetString(0);
                path.Path = reader.GetString(1);
                path.StudyInstanceUid = reader.GetString(2);
                pathList.Add(path);
                Console.WriteLine(reader.GetString(0));
                Console.WriteLine(reader.GetString(1));
                Console.WriteLine(reader.GetString(2));
                Console.WriteLine(reader.FieldCount);
            }
            return pathList;
        }

        internal List<PathModel> GetAllData(DicomQueryRetrieveLevel level)
        {
            throw new NotImplementedException();
        }

        public void SavePath(String path,String sUid)
        {
            Getmysqlcom("insert into tb_path (path,studyInstanceUid) values('" + path + "','"+sUid+"');");
        }

        internal List<PathModel> GetAllData(object level)
        {
            throw new NotImplementedException();
        }
    }
}
