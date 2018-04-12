
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TimeLine.Common
{
    public class TimeLineDb:DbContext
    {
        public TimeLineDb():base(SQL.ConnectionString)
        {
            
        }

        public DbSet<TimeLine.Models.TimeLineModel> timeLineModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeLine.Models.TimeLineModel>().Property(e => e.Id).IsRequired();

        }
    }
}