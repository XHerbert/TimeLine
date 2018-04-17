using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeLine.Common;
using TimeLine.Models;

namespace TimeLine.Controllers
{
    public partial class AdminController : Controller
    {
        public ActionResult AjaxLineList()
        {
            var list = new List<TimeLineModel>();
            LayUIResult<TimeLineModel> result = null;
            using (var db = new TimeLineDb())
            {

                list = db.timeLineModels.Where(i=>i.IsDeleted==false).OrderByDescending(p => p.CreateTime).ToList();
                if (list != null && list.Count > 0)
                {
                    result = new LayUIResult<TimeLineModel>();
                    result.code = 0;
                    result.msg = "请求成功";
                    result.count = list.Count;
                    result.data = list;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
        public ActionResult UploadImage()
        {
            ApiResult<string> result = new ApiResult<string>();
            string imgUrl = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (files == null || files.Count == 0)
            {
                imgUrl = string.Empty;
                result.Code = 404;
                result.Data = imgUrl;
                result.IsSuccess = false;
                return Json(result);
            }
            //编写创建model逻辑
            var fileBase = files[0];
            string imgPath = Path.GetFileName(fileBase.FileName);
            int index = imgPath.LastIndexOf('.');
            string suffix = imgPath.Substring(index).ToLower();
            if (suffix == ".jpg" || suffix == ".jpeg" || suffix == ".png" || suffix == ".gif" || suffix == ".bmp")
            {
                string pictureName = DateTime.Now.Ticks.ToString() + suffix;
                string savePath = Server.MapPath("/images/");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                imgUrl = "http://" + Request.Url.Authority + "/images/" + pictureName;
                fileBase.SaveAs(savePath + pictureName);
                result.Code = 200;
                result.Data = imgUrl;
                result.IsSuccess = true;
            }
            else
            {
                imgUrl = string.Empty;
                result.Code = 500;
                result.Data = imgUrl;
                result.IsSuccess = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            int ret = 0;
            using (var db = new TimeLineDb())
            {
                var model = db.timeLineModels.Where(p=>p.Id == id).First();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.UpdateTime = DateTime.Now;
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    ret = db.SaveChanges();
                }
            }
            if (ret > 0)
            {
                result.Code = 200;
                result.Data = true;
                result.IsSuccess = true;
            }else
            {
                result.Code = 500;
                result.Data = false;
                result.IsSuccess = false;
            }
            return Json(result);
        }
    }
}