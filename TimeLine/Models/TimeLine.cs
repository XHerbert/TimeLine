using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeLine.Models
{
    public class TimeLineModel
    {
        public int id { get; set; }
        public string titleYear { get; set; }
        public string titleMonth { get; set; }
        public string titleDay { get; set; }
        public string copy { get; set; }
        public Img img { get; set; }
        public DateTime createTime { get; set; }

    }

    public class Img
    {
        public string src { get; set; }
    }
}