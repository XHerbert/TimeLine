﻿using Newtonsoft.Json;
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
            
            ViewBag.models = model;
            ViewBag.json = JsonConvert.SerializeObject(model);
            return View();
        }
    }
}