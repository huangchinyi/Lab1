namespace LAB1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 :IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
          
              客戶資料Entities dd = new 客戶資料Entities();
              客戶聯絡人 data = dd.客戶聯絡人.Where(p => p.Email == this.Email && p.客戶Id == this.客戶Id && (p.是否已刪除 == false || p.是否已刪除 == null)).Where(p => p.Id != this.Id).FirstOrDefault();
            if (data != null)
            {
                yield return new ValidationResult("Email已有人使用", new string[] { "Email" });
            }

            yield return ValidationResult.Success;
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "手機格式範例EX:0912-345678")]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        public Nullable<bool> 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
