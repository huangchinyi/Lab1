using System;
using System.Linq;
using System.Collections.Generic;
	
namespace LAB1.Models
{   
	public  class 客戶清單資訊Repository : EFRepository<客戶清單資訊>, I客戶清單資訊Repository
	{

	}

	public  interface I客戶清單資訊Repository : IRepository<客戶清單資訊>
	{

	}
}