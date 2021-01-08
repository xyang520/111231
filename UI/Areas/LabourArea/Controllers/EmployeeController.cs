﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFunModel;
using Newtonsoft.Json;

namespace UI.Areas.LabourArea.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: LabourArea/Employee
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<Announcer> anlist = new List<Announcer>();
            using (MyContext context = new MyContext())
            {
                anlist = context.Announcer.Where(x => x.State == 0).ToList();
            }
            var data = new
            {
                count = anlist.Count(),//数据总条数，用于分页
                code = 0,//code码是必须要的， 0 表述返回的数据没有问题
                data = anlist,//数据源
                msg = "人员信息"//描述
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddAnnouncer(Announcer info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                db.Announcer.Add(info);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }


        public ActionResult DelAnnouncer(Announcer info)
        {
            string result = "Fail";
            using (MyContext db = new MyContext())
            {
                Announcer aninfo = db.Announcer.Where(x => x.ID == info.ID).FirstOrDefault();
                db.Announcer.Remove(aninfo);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    result = "Success";
                }
            }
            return Content(result);
        }
    }
}