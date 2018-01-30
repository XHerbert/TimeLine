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
                    id=0,
                    titleYear = "2010",
                    titleMonth="1",
                    titleDay="2",
                    copy="2010年，我们......",
                    img=GetImgsrc("1.jpg")
                }
            };
          
            
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        private Img GetImgsrc(string path)
        {
            Img img = new Models.Img();
            img.src = path;
            return img;
        }
    }
}