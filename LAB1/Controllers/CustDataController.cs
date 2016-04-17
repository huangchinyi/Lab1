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
using NPOI.SS.UserModel;
using System.Web.Security;    //-- v.1.2.4起 新增的。
using PagedList;

namespace LAB1.Controllers
{
     [Authorize (Roles="admin")]
    public class CustDataController : BaseController
    {
       // private 客戶資料Entities db = new 客戶資料Entities();

        // GET: CustData
        public ActionResult Index(string Keyword, string sortColumn, string sortOrder, int page=1)
         {

             if (sortOrder == null)
             {
                 Keyword = "";
                 sortColumn = "客戶名稱";
                 sortOrder = "ASC";
             }



             var 客戶資料 = repo客戶資料.All(Keyword);
             switch (sortColumn)
             {
                 case "客戶名稱":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.客戶名稱);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.客戶名稱);
                         sortOrder = "DESC";
                     }
                     break;
                 case "Email":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.Email);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.Email);
                         sortOrder = "DESC";
                     }
                     break;
                 case "地址":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.地址);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.地址);
                         sortOrder = "DESC";
                     }
                     break;
                 case "客戶分類":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.客戶分類);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.客戶分類);
                         sortOrder = "DESC";
                     }
                     break;
                 case "統一編號":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.統一編號);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.統一編號);
                         sortOrder = "DESC";
                     }
                     break;
                 case "電話":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.電話);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.電話);
                         sortOrder = "DESC";
                     }
                     break;
                 case "傳真":
                     if (sortOrder == "DESC")
                     {
                         客戶資料 = 客戶資料.OrderByDescending(s => s.傳真);
                         sortOrder = "ASC";
                     }
                     else
                     {
                         客戶資料 = 客戶資料.OrderBy(s => s.傳真);
                         sortOrder = "DESC";
                     }
                     break;


             }

             ViewBag.sort = sortOrder;
             ViewBag.page = page;

             var data = 客戶資料.ToPagedList(page, 5);
             return View(data);

             
         }

        // GET: CustData/Details/5
        public ActionResult Details(int? id)
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


        [HttpPost]
        public ActionResult Details(IList<BatchUpdateContact> data, int id)
        {

            if (ModelState.IsValid && data.Count>0)
            {
                foreach (var item in data)
                {
                    var product = rep客戶聯絡人.Find(item.Id);

                    product.職稱 = item.職稱;
                    product.手機 = item.手機;
                    product.電話 = item.電話;
                }

                rep客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            客戶資料 客戶資料 = repo客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);

         
        }


        // GET: CustData/Create
        public ActionResult Create()
        {

            var 客戶分類 = new List<SelectListItem>();
            客戶分類.Add(new SelectListItem() { Value = "好客戶", Text = "好客戶" });
            客戶分類.Add(new SelectListItem() { Value = "壞客戶", Text = "壞客戶" });
            ViewBag.客戶分類 = new SelectList(客戶分類, "Value", "Text", "客戶分類");

          

            return View();
        }

        // POST: CustData/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.帳號 + 客戶資料.密碼, "SHA1");
                repo客戶資料.Add(客戶資料);
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            var 客戶分類 = new List<SelectListItem>();
            客戶分類.Add(new SelectListItem() { Value = "好客戶", Text = "好客戶" });
            客戶分類.Add(new SelectListItem() { Value = "壞客戶", Text = "壞客戶" });
            ViewBag.客戶分類 = new SelectList(客戶分類, "Value", "Text", "客戶分類");

            return View(客戶資料);
        }


        public ActionResult Download()
        {

            IWorkbook workbook = new XSSFWorkbook();   
            XSSFSheet u_sheet = (XSSFSheet)workbook.CreateSheet("客戶資料");
         //   u_sheet.


            List<客戶資料> list = repo客戶資料.All().ToList();
            
            u_sheet.CreateRow(0).CreateCell(0).SetCellValue("客戶名稱");
            u_sheet.GetRow(0).CreateCell(1).SetCellValue("統一編號");
            u_sheet.GetRow(0).CreateCell(2).SetCellValue("電話");
            u_sheet.GetRow(0).CreateCell(3).SetCellValue("傳真");
            u_sheet.GetRow(0).CreateCell(4).SetCellValue("地址");
            u_sheet.GetRow(0).CreateCell(5).SetCellValue("Email");
            u_sheet.GetRow(0).CreateCell(6).SetCellValue("客戶分類");

            for (int i = 0; i < list.Count; i++)
            {
                u_sheet.CreateRow(i+1).CreateCell(0).SetCellValue(list[i].客戶名稱);
                u_sheet.GetRow(i + 1).CreateCell(1).SetCellValue(list[i].統一編號);
                u_sheet.GetRow(i + 1).CreateCell(2).SetCellValue(list[i].電話);
                u_sheet.GetRow(i + 1).CreateCell(3).SetCellValue(list[i].傳真);
                u_sheet.GetRow(i + 1).CreateCell(4).SetCellValue(list[i].地址);
                u_sheet.GetRow(i + 1).CreateCell(5).SetCellValue(list[i].Email);
                u_sheet.GetRow(i + 1).CreateCell(6).SetCellValue(list[i].客戶分類);
            }

          
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.ms-excel","客戶.xlsx");
        }

   

        // GET: CustData/Edit/5
        public ActionResult Edit(int? id)
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


            var 客戶分類 = new List<SelectListItem>();
            客戶分類.Add(new SelectListItem() { Value = "好客戶", Text = "好客戶" });
            客戶分類.Add(new SelectListItem() { Value = "壞客戶", Text = "壞客戶" });
            ViewBag.客戶分類 = new SelectList(客戶分類, "Value", "Text", "客戶分類");
            return View(客戶資料);
        }

        // POST: CustData/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = (客戶資料Entities)repo客戶資料.UnitOfWork.Context;

                客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.帳號+  客戶資料.密碼, "SHA1");


                dbProduct.Entry(客戶資料).State = EntityState.Modified;
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            var 客戶分類 = new List<SelectListItem>();
            客戶分類.Add(new SelectListItem() { Value = "好客戶", Text = "好客戶" });
            客戶分類.Add(new SelectListItem() { Value = "壞客戶", Text = "壞客戶" });
            ViewBag.客戶分類 = new SelectList(客戶分類, "Value", "Text", "客戶分類");

            return View(客戶資料);
        }

        // GET: CustData/Delete/5
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

        // POST: CustData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo客戶資料.Find(id);
            客戶資料.是否已刪除 = true;
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
    }
}
