using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLine
{
    public class RunSql
    {
        /// <summary>
        /// 查询目标专利
        /// </summary>
        public const string SQL_CITEDID = "SELECT  top {0} cited FROM dbo.dealFive ORDER BY cited  ASC ";
        /// <summary>
        /// 查询目标专利对应的年份以及引用量
        /// </summary>
        public const string SQL_CITEDLIST = "SELECT  cited ,citingyear , Counts FROM dbo.Tmp_CitedYearCount WHERE cited = {0} ORDER BY citingyear ASC  ";
        /// <summary>
        /// 创建目标记录
        /// </summary>
        public const string SQL_INSERT_TARGET_CITED = "INSERT INTO dbo.Table_CitedCrisis ( cited ,crisis_year ,top_crisis_avg ,after_crisis_avg ,after_crisis_total,flag ,create_time) VALUES  ( @cited ,@crisis_year ,@top_crisis_avg ,@after_crisis_avg , @after_crisis_total,@flag ,GETDATE())";

        public const string SQL_INSERT_HISTORY = "INSERT INTO dbo.Table_History (TitleYear, TitleMonth, TitleDay, Copy, Image, CreateTime ,UpdateTime,IsDeleted) VALUES (@TitleYear, @TitleMonth, @TitleDay, @Copy, @Image, @CreateTime ,@UpdateTime,@IsDeleted)";
    }
}
