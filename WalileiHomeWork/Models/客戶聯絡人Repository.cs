using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace WalileiHomeWork.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All();
        }

        public IQueryable<客戶聯絡人> QueryKeyWord(string key,string ddl,string sorting)
        {
            var result = base.All().Where(p => p.ISDELETED == false);
            if (!string.IsNullOrEmpty(key))
            {
                result = result.Where(p=>p.姓名.Contains(key));
            }
            if (!string.IsNullOrEmpty(ddl))
            {
                result = result.Where(p => p.職稱 == ddl);
            }

            if (!string.IsNullOrEmpty(sorting))
            {
                var param = Expression.Parameter(typeof(客戶聯絡人), "contact");
                if (sorting.Contains("asc"))
                {
                    sorting = sorting.Replace("asc", "");
                    var sortExp = Expression.Lambda<Func<客戶聯絡人, object>>(Expression.Property(param, sorting), param);
                    result = result.OrderBy(sortExp);
                }
                else if (sorting.Contains("desc"))
                {
                    sorting = sorting.Replace("desc", "");
                    var sortExp = Expression.Lambda<Func<客戶聯絡人, object>>(Expression.Property(param, sorting), param);
                    result = result.OrderByDescending(sortExp);
                }
            }
            else
                result = result.OrderBy(p => p.Id);

            return result;
        }

        public 客戶聯絡人 Find(int id)
        {
            return base.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.ISDELETED = true;
        }

        public override void Add(客戶聯絡人 entity)
        {
            var list = All().ToList();
            foreach (客戶聯絡人 item in list)
            {
                if (item.Email == entity.Email)
                {
                    throw new EmailRepeatException();
                }
            }
            base.Add(entity);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}