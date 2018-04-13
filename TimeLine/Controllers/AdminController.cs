using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeLine.Common;
using TimeLine.Models;

namespace TimeLine.Controllers
{

    public class AdminController : Controller
    {
        static AdminController()
        {
            using (var db = new TimeLineDb())
            {
                var success = db.Database.CreateIfNotExists();
                Debug.WriteLine(success);
            };
            //Database.SetInitializer(new Initializer());
        }

        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create(TimeLineModel model)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            //编写创建model逻辑
            using (var dbContext = new TimeLineDb())
            {
                TimeLineModel newModel = model;
                try
                {
                    newModel.CreateTime = DateTime.Now;
                    newModel.UpdateTime = DateTime.Now;
                    newModel.IsDeleted = false;
                    newModel.Images = "1234.jpg";
                    dbContext.timeLineModels.Add(newModel);                
                    int ret = dbContext.SaveChanges();
                    if (ret > 0)
                    {
                        result.Code = 10000;
                        result.Data = true;
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.Code = 20000;
                        result.Data = false;
                        result.IsSuccess = false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImage(string src)
        {
            ApiResult<string> result = new ApiResult<string>();
            //编写创建model逻辑


            result.Code = 200;
            result.Data = src;
            result.IsSuccess = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public int Code { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}