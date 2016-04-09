using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WalileiHomeWork.Models;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace WalileiHomeWork.Controllers
{
    public class 客戶資料Controller : BaseController
    {

        // GET: 客戶資料
        public ActionResult Index(string txtSearch)
        {
            return View(repo客戶資料.QueryKeyWord(txtSearch));
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewBag.CUSCLASS = new SelectList(db.客戶資料, "Id", "CUSTOMER_CLASS");
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.客戶資料)
            {
                items.Add(new SelectListItem() { Text = item.CUSTOMER_CLASS, Value = item.CUSTOMER_CLASS});
            }
            ViewBag.CUSTOMER_CLASS = items;
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CUSTOMER_CLASS,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (ModelState.IsValid)
            {
                repo客戶資料.Add(客戶資料);
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.客戶資料)
            {
                bool slt = item.CUSTOMER_CLASS == 客戶資料.CUSTOMER_CLASS;
                items.Add(new SelectListItem() { Text = item.CUSTOMER_CLASS, Value = item.CUSTOMER_CLASS, Selected = slt });
            }
            ViewBag.CUSTOMER_CLASS = items;
            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.客戶資料)
            {
                bool slt = item.CUSTOMER_CLASS == 客戶資料.CUSTOMER_CLASS;
                items.Add(new SelectListItem() { Text = item.CUSTOMER_CLASS, Value = item.CUSTOMER_CLASS, Selected = slt });
            }

            ViewBag.CUSTOMER_CLASS = items;
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CUSTOMER_CLASS,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (ModelState.IsValid)
            {
                var context = (客戶資料Entities) repo客戶資料.UnitOfWork.Context;
                repo客戶資料.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.客戶資料)
            {
                bool slt = item.CUSTOMER_CLASS == 客戶資料.CUSTOMER_CLASS;
                items.Add(new SelectListItem() { Text = item.CUSTOMER_CLASS, Value = item.CUSTOMER_CLASS,Selected=slt });
            }

            ViewBag.CUSTOMER_CLASS = items;
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo客戶資料.Find(id);
            repo客戶資料.Delete(客戶資料);
            repo客戶資料.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo客戶資料.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Export(string txtSearch)
        {
            var result =repo客戶資料.QueryKeyWord(txtSearch);

            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet u_sheet = wb.CreateSheet("My Sheet_20方法二");

            int i = 0;
            foreach (客戶資料 item in result)
            {
                u_sheet.CreateRow(i);
                u_sheet.GetRow(i).CreateCell(0).SetCellValue(item.客戶名稱);
                u_sheet.GetRow(i).CreateCell(1).SetCellValue(item.電話);
                i++;
            }
            


            MemoryStream MS = new MemoryStream();   //==需要 System.IO命名空間
            wb.Write(MS);

            return File(MS.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
