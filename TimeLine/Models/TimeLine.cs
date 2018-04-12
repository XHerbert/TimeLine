using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeLine.Models
{
    [Table("Time.Table_History")]
    public class TimeLineModel
    {
        [Key]
        public int Id { get; set; }
        public string TitleYear { get; set; }
        public string TitleMonth { get; set; }
        public string TitleDay { get; set; }
        public string Copy { get; set; }
        public string Images { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class images
    {
        public string src { get; set; }
    }
}