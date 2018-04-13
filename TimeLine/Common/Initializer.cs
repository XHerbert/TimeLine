using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TimeLine.Common
{
    public class Initializer: CreateDatabaseIfNotExists<TimeLineDb>
    {
        public override void InitializeDatabase(TimeLineDb context)
        {
            base.InitializeDatabase(context);
        }
    }
}