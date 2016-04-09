using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace WalileiHomeWork.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All();
        }

        public IQueryable<客戶資料> QueryKeyWord(string key)
        {
            var result = base.All().Where(p => p.ISDELETED == false);
            if (string.IsNullOrEmpty(key))
                return result;
            else {
                return result.Where(p => p.客戶名稱.Contains(key));
            }
        }

        public 客戶資料 Find(int id)
        {
            return base.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.ISDELETED = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}