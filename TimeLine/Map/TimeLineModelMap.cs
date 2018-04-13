using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TimeLine.Models;

namespace TimeLine.Map
{
    public class TimeLineModelMap: EntityTypeConfiguration<TimeLineModel>
    {
        public TimeLineModelMap()
        {
            this.HasKey(p => p.Id);
            this.ToTable("Table_History", "dbo");
            this.Property(p => p.IsDeleted).IsRequired();
        }
    }
}