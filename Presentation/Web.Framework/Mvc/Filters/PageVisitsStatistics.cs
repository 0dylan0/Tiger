using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Web.Framework.Mvc.Filters
{
    public class PageVisitsStatistics
    {
        public static void PageVisitSum(ActionExecutingContext actionContext)
        {
            if (actionContext.HttpContext.Request.HttpMethod == "GET" && !actionContext.HttpContext.Request.IsAjaxRequest())
            {
                string path = MapPath("~");
                path += "\\PageVisitsStatistics.txt";
                if (!File.Exists(path))
                {
                    FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);
                    fs1.Close();
                }
                string name = actionContext.HttpContext.Request.Path + " ";
                StreamReader sr = new StreamReader(path, Encoding.Default);
                var srRead = sr.ReadToEnd();
                List<string> list = srRead.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                sr.Close();
                string str = name + 1 + Environment.NewLine;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                foreach (var item in list)
                {
                    if (item.Contains(name))
                    {
                        var it = item.Split(' ');
                        var sum = it[1];
                        int i = int.Parse(sum) + 1;
                        var newName = name + i.ToString();
                        srRead = srRead.Replace(item, newName);
                        sw.Write(srRead);
                        sw.Close();
                        fs.Close();
                        break;
                    }
                }
                sw.Close();
                fs.Close();
                if (!srRead.Contains(name))
                {
                    File.AppendAllText(path, str);
                }

            }
        }
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }
    }
}

