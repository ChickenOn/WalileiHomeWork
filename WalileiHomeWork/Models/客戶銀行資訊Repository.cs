using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace WalileiHomeWork.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All();
        }

        public DbRawSqlQuery<客戶銀行資訊> QueryKeyWord(string key)
        {
            DbRawSqlQuery<客戶銀行資訊> list = base.UnitOfWork.Context.Database.SqlQuery<客戶銀行資訊>(@"SELECT *
  FROM[客戶銀行資訊]
  WHERE([客戶Id] like @p0 OR[帳戶名稱] like @p0 OR 銀行名稱 like @p0 OR 帳戶號碼 like @p0) AND ISDELETED = 0", "%" + key + "%");
            return list;
        }

        public 客戶銀行資訊 Find(int id)
        {
            return base.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.ISDELETED = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}