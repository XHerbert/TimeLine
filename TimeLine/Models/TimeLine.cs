using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeLine.Models
{
    public class TimeLineModel
    {
        public int Id { get; set; }
        public string TitleYear { get; set; }
        public string TitleMonth { get; set; }
        public string TitleDay { get; set; }
        public string Copy { get; set; }
        public images Images { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class images
    {
        public string src { get; set; }
    }
}