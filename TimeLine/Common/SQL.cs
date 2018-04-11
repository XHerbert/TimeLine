using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLine.Common;
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
                return Config.GetConfig("Table");
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
        /// 获取数据源
        /// </summary>
        /// <param name="cited"></param>
        /// <returns></returns>
        public static List<TimeLineModel> GetModels()
        {
            List<TimeLineModel> lines = new List<TimeLineModel>();
            using (SqlConnection conn = SQL.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(string.Format(RunSql.SQL_CITEDLIST, "select * from dbo.Table_History"), conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    images img = new images();
                    TimeLineModel model = new TimeLineModel();
                    model.Id = Convert.ToInt32(reader[0]);
                    model.TitleYear = reader[1].ToString();
                    model.TitleMonth = reader[2].ToString();
                    model.TitleDay = reader[3].ToString();
                    model.Copy = reader[4].ToString();
                    img.src = reader[5].ToString();
                    model.CreateTime = Convert.ToDateTime(reader[6]);
                    model.Images = img;
                    model.UpdateTime = Convert.ToDateTime(reader[7]);
                    model.IsDeleted = Convert.ToBoolean(reader[8]);
                    lines.Add(model);
                }
            }
            return lines;
        }

        /// <summary>
        /// 创建计算结果
        /// </summary>
        /// <param name="stroredCited"></param>
        public static void AddCitedModel(TimeLineModel timeLineModel)
        {
            using (SqlConnection conn = SQL.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(RunSql.SQL_INSERT_HISTORY, conn);
                SqlParameter[] _para = {
                                           
                                           new SqlParameter("@TitleYear",timeLineModel.TitleYear),
                                           new SqlParameter("@TitleMonth",timeLineModel.TitleMonth),
                                           new SqlParameter("@TitleDay",timeLineModel.TitleDay),
                                           new SqlParameter("@Copy",timeLineModel.Copy),
                                           new SqlParameter("@Images",timeLineModel.Images),
                                           new SqlParameter("@CreateTime",timeLineModel.CreateTime),
                                           new SqlParameter("@UpdateTime",timeLineModel.UpdateTime),
                                           new SqlParameter("@IsDeleted",timeLineModel.IsDeleted),
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
