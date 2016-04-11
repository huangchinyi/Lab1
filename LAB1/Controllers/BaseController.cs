using LAB1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAB1.Controllers
{

    [計算Action的執行時間Attribute]
     [HandleError(ExceptionType = typeof(InvalidOperationException), View = "Error2")]
     [HandleError(ExceptionType = typeof(Exception), View = "Error1")]
    public class BaseController : Controller
    {
        protected 客戶資料Repository repo客戶資料
                   = RepositoryHelper.Get客戶資料Repository();

        protected 客戶清單資訊Repository repo客戶清單資訊
           = RepositoryHelper.Get客戶清單資訊Repository();

        protected 客戶銀行資訊Repository repo客戶銀行資訊
           = RepositoryHelper.Get客戶銀行資訊Repository();

        protected 客戶聯絡人Repository rep客戶聯絡人
           = RepositoryHelper.Get客戶聯絡人Repository();


        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction("Index", "Home")
                .ExecuteResult(this.ControllerContext);
        }
    }
}