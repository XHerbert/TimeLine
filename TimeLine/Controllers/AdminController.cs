using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeLine.Models;

namespace TimeLine.Controllers
{
    
    public class AdminController : Controller
    {
       
        // GET: Admin
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Create(TimeLineModel model)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            //编写创建model逻辑


            result.Code = 200;
            result.Data = true;
            result.IsSuccess = true;

            return Json(result,JsonRequestBehavior.AllowGet);
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