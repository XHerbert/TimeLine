using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLine.Models;

namespace Cited
{
    public class SQL
    {
        //连接数据库
        public static string GetConnectionString
        {
            get
            {
                return "server = XUHONGBO;Database = TestDB;uid = sa;pwd = xsw2@wsx";
            }
        }

        public static string GetBaseTable
        {
            get
            {
                return "patsic06_mar09_ipc.dbo.Tmp_CitedYearCount";
            }
        }

        public static string GetDealTable
        {
            get
            {
                return "patsic06_mar09_ipc.dbo.dealFive";
            }
        }
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnect()
        {
            SqlConnection conn = SQL.GetConnection();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(SQL.GetConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 获取所有的cited
        /// </summary>
        /// <returns></returns>
        public static List<int> GetTargetCitedId(int recordsCount)
        {
            List<int> citeds = new List<int>();
            using (SqlConnection conn = SQL.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(string.Format(RunSql.SQL_CITEDID, recordsCount), conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    citeds.Add(Convert.ToInt32(reader[0]));
                }
            }
            return citeds;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="cited"></param>
        /// <returns></returns>
        public static List<TimeLineModel> GetModelsByCited()
        {
            List<TimeLineModel> citeds = new List<TimeLineModel>();
            using (SqlConnection conn = SQL.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(string.Format(RunSql.SQL_CITEDLIST, "select * from dbo.Table_History"), conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img img = new Img();
                    TimeLineModel model = new TimeLineModel();
                    model.id = Convert.ToInt32(reader[0]);
                    model.titleYear = reader[1].ToString();
                    model.titleMonth = reader[2].ToString();
                    model.titleDay = reader[3].ToString();
                    model.copy = reader[4].ToString();
                    img.src = reader[5].ToString();
                    model.createTime = Convert.ToDateTime(reader[6]);
                    model.img = img;
                    citeds.Add(model);
                }
            }
            return citeds;
        }

        /// <summary>
        /// 创建计算结果
        /// </summary>
        /// <param name="stroredCited"></param>
        public static void AddCitedModel(TimeLineModel storedCited)
        {
            using (SqlConnection conn = SQL.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(RunSql.SQL_INSERT_TARGET_CITED, conn);
                SqlParameter[] _para = {
                                           new SqlParameter("@id",storedCited.id),
                                           new SqlParameter("@titleYear",storedCited.titleYear),
                                           new SqlParameter("@titleMonth",storedCited.titleMonth),
                                           new SqlParameter("@titleDay",storedCited.titleDay),
                                           new SqlParameter("@copy",storedCited.copy),
                                           new SqlParameter("@createTime",storedCited.createTime)
                                       };
                if (null != _para)
                {
                    cmd.Parameters.AddRange(_para);
                }
                cmd.ExecuteNonQuery();
            }
        }
    }
}
