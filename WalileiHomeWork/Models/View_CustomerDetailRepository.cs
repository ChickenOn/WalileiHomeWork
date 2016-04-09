using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WalileiHomeWork.Models
{   
	public  class View_CustomerDetailRepository : EFRepository<View_CustomerDetail>, IView_CustomerDetailRepository
	{

	}

	public  interface IView_CustomerDetailRepository : IRepository<View_CustomerDetail>
	{

	}
}