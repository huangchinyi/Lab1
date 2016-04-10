using LAB1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LAB1.Controllers
{
    [Authorize(Roles = "Cust")]
    public class EditProfileController : BaseController
    {
        // GET: EditProfile
        public ActionResult EditProfile(string account)
        {

            if (account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(account);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

   
            List<string> list = new List<string>();
            list.Add("好客戶");
            list.Add("壞客戶");

            ViewBag.客戶分類 = new SelectList(list);

            return View(客戶資料);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = (客戶資料Entities)repo客戶資料.UnitOfWork.Context;

                客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.帳號 + 客戶資料.密碼, "SHA1");


                dbProduct.Entry(客戶資料).State = EntityState.Modified;
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }
    }
}