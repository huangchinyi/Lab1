using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LAB1.Models
{
    public class BatchUpdateContact
    {

        [Required]
        public int Id { get; set; }


        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }


      
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "手機格式範例EX:0912-345678")]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }


        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

    }

  

}