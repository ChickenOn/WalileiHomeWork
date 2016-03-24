using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WalileiHomeWork.Models;

namespace WalileiHomeWork.Controllers
{
    public class View_CustomerDetailController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: View_CustomerDetail
        public ActionResult Index()
        {
            return View(db.View_CustomerDetail.ToList());
        }

        // GET: View_CustomerDetail/Details/5
        
    }
}
