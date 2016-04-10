using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAB1.Models
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }
        public string 客戶名稱 { get; set; }
        public string 統一編號 { get; set; }
        public string 電話 { get; set; }
        public string 傳真 { get; set; }
        public string 地址 { get; set; }
        public string Email { get; set; }

        public string 客戶分類 { get; set; }
        public string 帳號 { get; set; }
        public string 密碼 { get; set; }
    }
}