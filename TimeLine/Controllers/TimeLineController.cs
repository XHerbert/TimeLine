using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeLine.Models;

namespace TimeLine.Controllers
{
    public class TimeLineController : Controller
    {
        // GET: TimeLine
        public ActionResult Index()
        {
            List<TimeLineModel> model = new List<TimeLineModel>()
            {
                new TimeLineModel()
                {
                    Id=0,
                    TitleYear = "2010",
                    TitleMonth="10",
                    TitleDay="2",
                    Copy="2010年，我们......",
                    Images=GetImgsrc("1942-1000x1670.jpg")
                },
                 new TimeLineModel()
                {
                    Id=0,
                    TitleYear = "2012",
                    TitleMonth="12",
                    TitleDay="2",
                    Copy="2012年，我们......",
                    Images=GetImgsrc("1963.jpg")
                }
            };
            ViewBag.models = model;
            ViewBag.json = JsonConvert.SerializeObject(model);
            return View();
            //return Json(model,JsonRequestBehavior.AllowGet);
        }

        private images GetImgsrc(string path)
        {
            images img = new Models.images();
            img.src = "/images/"+path;
            return img;
        }
    }
}