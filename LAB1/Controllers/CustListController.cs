using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LAB1.Models;
using System.IO;
using NPOI.XSSF.UserModel;   //-- XSSF 用來產生Excel 2007檔案（.xlsx）
using NPOI.SS.UserModel;    //-- v.1.2.4起 新增的。

namespace LAB1.Controllers
{
    public class CustListController : BaseController
    {
      //  private 客戶資料Entities db = new 客戶資料Entities();

        // GET: CustList
        public ActionResult Index()
        {
            return View(repo客戶清單資訊.All().ToList());
        }

        public ActionResult Download()
        {

            IWorkbook workbook = new XSSFWorkbook();
            XSSFSheet u_sheet = (XSSFSheet)workbook.CreateSheet("客戶清單");
            //   u_sheet.


            List<客戶清單資訊> list = repo客戶清單資訊.All().ToList();

            u_sheet.CreateRow(0).CreateCell(0).SetCellValue("客戶名稱");
            u_sheet.GetRow(0).CreateCell(1).SetCellValue("聯絡人數量");
            u_sheet.GetRow(0).CreateCell(2).SetCellValue("銀行帳戶數量");

            for (int i = 0; i < list.Count; i++)
            {
                u_sheet.CreateRow(i + 1).CreateCell(0).SetCellValue(list[i].客戶名稱);
                u_sheet.GetRow(i + 1).CreateCell(1).SetCellValue(list[i].聯絡人數量.Value);
                u_sheet.GetRow(i + 1).CreateCell(2).SetCellValue(list[i].銀行帳戶數量.Value);
            }


            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.ms-excel", "客戶清單.xlsx");
        }


        //// GET: CustList/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    客戶清單資訊 客戶清單資訊 = db.客戶清單資訊.Find(id);
        //    if (客戶清單資訊 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(客戶清單資訊);
        //}

        //// GET: CustList/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CustList/Create
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] 客戶清單資訊 客戶清單資訊)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.客戶清單資訊.Add(客戶清單資訊);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(客戶清單資訊);
        //}

        //// GET: CustList/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    客戶清單資訊 客戶清單資訊 = db.客戶清單資訊.Find(id);
        //    if (客戶清單資訊 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(客戶清單資訊);
        //}

        //// POST: CustList/Edit/5
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] 客戶清單資訊 客戶清單資訊)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(客戶清單資訊).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(客戶清單資訊);
        //}

        //// GET: CustList/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    客戶清單資訊 客戶清單資訊 = db.客戶清單資訊.Find(id);
        //    if (客戶清單資訊 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(客戶清單資訊);
        //}

        //// POST: CustList/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    客戶清單資訊 客戶清單資訊 = db.客戶清單資訊.Find(id);
        //    db.客戶清單資訊.Remove(客戶清單資訊);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
