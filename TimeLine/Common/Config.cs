using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TimeLine.Common
{
    public class Config
    {
        public static string GetConfig(string key)
        {
            object ret = ConfigurationManager.AppSettings[key];
            if (ret == null || ( ret !=null && string.IsNullOrEmpty(ret.ToString())))
            {
                return $"请配置key为{key}的配置项";
            }else
            {
                return ret.ToString();
            }
        }
    }
}