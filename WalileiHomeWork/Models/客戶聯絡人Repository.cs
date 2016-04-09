using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace WalileiHomeWork.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All();
        }

        public DbRawSqlQuery<客戶聯絡人> QueryKeyWord(string key)
        {
            DbRawSqlQuery<客戶聯絡人> list = base.UnitOfWork.Context.Database.SqlQuery<客戶聯絡人>(@"select * from dbo.客戶聯絡人 
 WHERE (客戶Id like @p0 OR 職稱 like @p0 OR 姓名 like @p0 OR Email like @p0 OR 手機 like @p0 OR 電話 like @p0) AND ISDELETED = 0", "%" + key + "%");
            return list;
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