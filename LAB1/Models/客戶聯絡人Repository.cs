using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
	
namespace LAB1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include("客戶資料").Where(p => p.是否已刪除 == false || p.是否已刪除 == null);
        }

        public IQueryable<客戶聯絡人> All(string keyworkd)
        {
            return this.All().Where(p => p.姓名.Contains(keyworkd));
        }

    public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

    public IEnumerable<SelectListItem> getTitle()
    {

        return this.All()
            .AsQueryable()
            .Select(s => new SelectListItem()
            {
                Value = s.職稱,
                Text = s.職稱
            })
                 ;

    }



        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }
	}

	

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}