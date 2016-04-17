using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace WalileiHomeWork.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All();
        }

        public IQueryable<客戶資料> QueryKeyWord(string key, string sorting)
        {
            var result = base.All().Where(p => p.ISDELETED == false);
            if (!string.IsNullOrEmpty(key))
            {
                result = result.Where(p => p.客戶名稱.Contains(key));
            }

            if (!string.IsNullOrEmpty(sorting))
            {
                var param = Expression.Parameter(typeof(客戶資料), "customer");
                if (sorting.Contains("asc"))
                {
                    sorting = sorting.Replace("asc", "");
                    var sortExp = Expression.Lambda<Func<客戶資料, object>>(Expression.Property(param, sorting), param);
                    result = result.OrderBy(sortExp);
                }
                else if (sorting.Contains("desc"))
                {
                    sorting = sorting.Replace("desc", "");
                    var sortExp = Expression.Lambda<Func<客戶資料, object>>(Expression.Property(param, sorting), param);
                    result = result.OrderByDescending(sortExp);
                }
            }
            else
                result = result.OrderBy(p => p.Id);
            return result;
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