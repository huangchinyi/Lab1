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
    [Authorize(Roles = "admin")]
    public class BankController : BaseController
    {
      //  private 客戶資料Entities db = new 客戶資料Entities();

        

        // GET: Bank
        public ActionResult Index()
        {
            var 客戶銀行資訊 = repo客戶銀行資訊.All();
            return View(客戶銀行資訊.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Keyword)
        {
            var 客戶銀行資訊 = repo客戶銀行資訊.All(Keyword);

            return View(客戶銀行資訊.ToList());
        }


        public ActionResult Download()
        {

            IWorkbook workbook = new XSSFWorkbook();
            XSSFSheet u_sheet = (XSSFSheet)workbook.CreateSheet("銀行帳戶");
            //   u_sheet.


            List<客戶銀行資訊> list = repo客戶銀行資訊.All().ToList();

            u_sheet.CreateRow(0).CreateCell(0).SetCellValue("銀行名稱");
            u_sheet.GetRow(0).CreateCell(1).SetCellValue("銀行代碼");
            u_sheet.GetRow(0).CreateCell(2).SetCellValue("分行代碼");
            u_sheet.GetRow(0).CreateCell(3).SetCellValue("帳戶名稱");
            u_sheet.GetRow(0).CreateCell(4).SetCellValue("帳戶號碼");
            u_sheet.GetRow(0).CreateCell(5).SetCellValue("客戶名稱");
           // u_sheet.GetRow(0).CreateCell(6).SetCellValue("客戶分類");

            for (int i = 0; i < list.Count; i++)
            {
                u_sheet.CreateRow(i + 1).CreateCell(0).SetCellValue(list[i].銀行名稱);
                u_sheet.GetRow(i + 1).CreateCell(1).SetCellValue(list[i].銀行代碼);
                u_sheet.GetRow(i + 1).CreateCell(2).SetCellValue(list[i].分行代碼.Value);
                u_sheet.GetRow(i + 1).CreateCell(3).SetCellValue(list[i].帳戶名稱);
                u_sheet.GetRow(i + 1).CreateCell(4).SetCellValue(list[i].帳戶號碼);
                u_sheet.GetRow(i + 1).CreateCell(5).SetCellValue(list[i].客戶資料.客戶名稱);
         
            }


            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.ms-excel", "銀行資料.xlsx");
        }


        //

        // GET: Bank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: Bank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Bank/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo客戶銀行資訊.Add(客戶銀行資訊);
                repo客戶銀行資訊.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Bank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View(客戶銀行資訊);
        }

        // POST: Bank/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = (客戶資料Entities)repo客戶銀行資訊.UnitOfWork.Context;
                dbProduct.Entry(客戶銀行資訊).State = EntityState.Modified;
                repo客戶銀行資訊.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Bank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id);
            客戶銀行資訊.是否已刪除 = true;

            repo客戶銀行資訊.Delete(客戶銀行資訊);
            repo客戶銀行資訊.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo客戶銀行資訊.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
