using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TimeLine.Common;
using TimeLine.Models;

namespace TimeLine.Controllers
{

    public partial class AdminController : Controller
    {
        static AdminController()
        {
            //using (var db = new TimeLineDb())
            //{
            //    var success = db.Database.CreateIfNotExists();
            //    Debug.WriteLine(success);
            //};
            //Database.SetInitializer(new Initializer());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LineList()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            TimeLineModel model = new TimeLineModel();
            using (var db = new TimeLineDb())
            {
                var findModel = db.timeLineModels.Where(p => p.Id == id).First();
                if (findModel != null)
                {
                    model = findModel;
                }
            }
            return View(model);
        }
    }

    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public int Code { get; set; }

        public ApiResult()
        {
            this.Code = 500;
            this.IsSuccess = false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class LayUIResult<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }
}