using System;
using System.Linq;
using System.Collections.Generic;
	
namespace LAB1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{

        public override IQueryable<客戶資料> All()
        {

            return base.All().Include("客戶聯絡人").Where(p => p.是否已刪除 == false || p.是否已刪除 == null);

        }

        public IQueryable<客戶資料> All(string keyword)
        {

            return this.All().Where(p => p.客戶名稱.Contains(keyword));

        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }


        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}