
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TimeLine.Map;
using TimeLine.Models;

namespace TimeLine.Common
{
    public class TimeLineDb:DbContext
    {
        public string ConnectionString { get; set; } = string.Empty;
        public TimeLineDb():base(SQL.ConnectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;//自动跟踪对象的属性变化
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        static TimeLineDb()
        {
            
        }
        public DbSet<TimeLineModel> timeLineModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TimeLineModelMap());
        }
    }
}