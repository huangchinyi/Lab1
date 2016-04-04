using System;
using System.Linq;
using System.Collections.Generic;
	
namespace LAB1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {

            return base.All().Include("客戶資料").Where(p => p.是否已刪除 == false || p.是否已刪除 == null);

        }

        public  IQueryable<客戶銀行資訊> All(string keyword)
        {

            return this.All().Where(p => p.銀行名稱.Contains(keyword));

        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }

	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
     


	}
}