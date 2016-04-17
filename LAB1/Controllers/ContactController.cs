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
using PagedList;

namespace LAB1.Controllers
{
  



    [Authorize(Roles = "admin")]
    public class ContactController : BaseController
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

       
        // GET: Contact
        public ActionResult Index(string keyword, string sortColumn, string sortOrder, string 職稱_1, int page = 1)

        {


            if (sortOrder == null)
            {
                keyword = "";
                sortColumn = "職稱";
                sortOrder = "ASC";
                職稱_1 = "";
            }
         


         
            var 客戶聯絡人 = rep客戶聯絡人.All(keyword) ;

            if (職稱_1 != "")
            {
               客戶聯絡人= 客戶聯絡人.Where(p => p.職稱 == 職稱_1);
            }
            ViewBag.職稱列表 = rep客戶聯絡人.getTitle();
            //ViewBag.SortingPagingInfo = info;

            switch (sortColumn)
            {
                case "職稱":
                    if (sortOrder == "DESC")
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.職稱);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.職稱);
                        sortOrder = "DESC";
                    }
                    break;
                case "姓名":
                    if (sortOrder == "DESC")
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.姓名);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.姓名);
                        sortOrder = "DESC";
                    }
                    break;
                case "Email":
                    if (sortOrder == "DESC")
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.Email);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.Email);
                        sortOrder = "DESC";
                    }
                    break;
                case "手機":
                    if (sortOrder == "DESC")
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.手機);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.手機);
                        sortOrder = "DESC";
                    }
                    break;
                case "電話":
                    if (sortOrder == "DESC")
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.電話);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.電話);
                        sortOrder = "DESC";
                    }
                    break;
                case "客戶名稱":
                    if (sortOrder == "DESC")
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.客戶資料.客戶名稱);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.客戶資料.客戶名稱);
                        sortOrder = "DESC";
                    }
                    break;
            
              
            }

            ViewBag.sort = sortOrder;
            ViewBag.page = page;

            var data = 客戶聯絡人.ToPagedList(page, 5);
            return View(data);
        }

    


        public ActionResult Download()
        {

            IWorkbook workbook = new XSSFWorkbook();
            XSSFSheet u_sheet = (XSSFSheet)workbook.CreateSheet("客戶聯絡人");
            //   u_sheet.


            List<客戶聯絡人> list = rep客戶聯絡人.All().ToList();

            u_sheet.CreateRow(0).CreateCell(0).SetCellValue("職稱");
            u_sheet.GetRow(0).CreateCell(1).SetCellValue("姓名");
            u_sheet.GetRow(0).CreateCell(2).SetCellValue("Email");
            u_sheet.GetRow(0).CreateCell(3).SetCellValue("手機");
            u_sheet.GetRow(0).CreateCell(4).SetCellValue("電話");
            u_sheet.GetRow(0).CreateCell(5).SetCellValue("客戶名稱");


            for (int i = 0; i < list.Count; i++)
            {
                u_sheet.CreateRow(i + 1).CreateCell(0).SetCellValue(list[i].職稱);
                u_sheet.GetRow(i + 1).CreateCell(1).SetCellValue(list[i].姓名);
                u_sheet.GetRow(i + 1).CreateCell(2).SetCellValue(list[i].Email);
                u_sheet.GetRow(i + 1).CreateCell(3).SetCellValue(list[i].手機);
                u_sheet.GetRow(i + 1).CreateCell(4).SetCellValue(list[i].電話);
                u_sheet.GetRow(i + 1).CreateCell(5).SetCellValue(list[i].客戶資料.客戶名稱);
           
            }


            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.ms-excel", "客戶聯終人.xlsx");
        }

        
        

        

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = rep客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                rep客戶聯絡人.Add(客戶聯絡人);
                rep客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = rep客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = (客戶資料Entities)rep客戶聯絡人.UnitOfWork.Context;
                dbProduct.Entry(客戶聯絡人).State = EntityState.Modified;
                rep客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = rep客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = rep客戶聯絡人.Find(id);
            客戶聯絡人.是否已刪除 = true;
            rep客戶聯絡人.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rep客戶聯絡人.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
