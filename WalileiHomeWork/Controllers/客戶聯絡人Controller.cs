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
    public class 客戶聯絡人Controller : BaseController
    {
        public ActionResult Index(string txtSearch)
        {
            var list = repo客戶聯絡人.QueryKeyWord(txtSearch);
            ViewBag.DropDownList = GetDropDownList();
            return View(list.ToList());
        }

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        public ActionResult Create()
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,PHONE")] 客戶聯絡人 客戶聯絡人)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (ModelState.IsValid)
            {
                try
                {
                    repo客戶聯絡人.Add(客戶聯絡人);
                }
                catch (EmailRepeatException)
                {
                    ViewBag.Err = "email重複";
                    return RedirectToAction("Create");
                }
                repo客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,PHONE")] 客戶聯絡人 客戶聯絡人)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            客戶資料Entities db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id);
            repo客戶聯絡人.Delete(客戶聯絡人);
            repo客戶聯絡人.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo客戶聯絡人.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
        private SelectList GetDropDownList()
        {
            var 職稱s = repo客戶聯絡人.All().Select(p => p.職稱).Distinct().ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in 職稱s)
            {
                list.Add(new SelectListItem() { Value = item, Text = item });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}
