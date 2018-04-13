using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeLine.Common;
using TimeLine.Models;

namespace TimeLine.Controllers
{
    public class TimeLineController : Controller
    {
        // GET: TimeLine
        public ActionResult Index()
        {
            List<TimeLineModel> model = new List<TimeLineModel>();
            using (var db = new TimeLineDb())
            {
               model =  db.timeLineModels.ToList<TimeLine.Models.TimeLineModel>();
            }
            //List<TimeLineModel> model = new List<TimeLineModel>()
            //{
            //    new TimeLineModel()
            //    {
            //        Id=0,
            //        TitleYear = "2009",
            //        TitleMonth="10",
            //        TitleDay="2",
            //        Copy="2009年，我们......",
            //        Images=GetImgsrc("1942-1000x1670.jpg")
            //    },
            //     new TimeLineModel()
            //    {
            //        Id=0,
            //        TitleYear = "2010",
            //        TitleMonth="10",
            //        TitleDay="2",
            //        Copy="2010年，我们......",
            //        Images=GetImgsrc("1942-1000x1670.jpg")
            //    },
            //     new TimeLineModel()
            //    {
            //        Id=0,
            //        TitleYear = "2011",
            //        TitleMonth="12",
            //        TitleDay="2",
            //        Copy="2011年，我们......",
            //        Images=GetImgsrc("1963.jpg")
            //    },
                
            //     new TimeLineModel()
            //    {
            //        Id=0,
            //        TitleYear = "2012",
            //        TitleMonth="12",
            //        TitleDay="2",
            //        Copy="2012年，我们......",
            //        Images=GetImgsrc("1963.jpg")
            //    }
            //};
            ViewBag.models = model;
            ViewBag.json = JsonConvert.SerializeObject(model);
            return View();
            //return Json(model,JsonRequestBehavior.AllowGet);
        }

        private string GetImgsrc(string path)
        {
            return "/images/" +path;
        }
    }
}