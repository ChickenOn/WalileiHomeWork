﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalileiHomeWork.Models;

namespace WalileiHomeWork.Controllers
{
    public class BaseController : Controller
    {
        protected 客戶資料Repository repo客戶資料 = RepositoryHelper.Get客戶資料Repository();
        protected 客戶銀行資訊Repository repo客戶銀行 = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶聯絡人Repository repo客戶聯絡人 = RepositoryHelper.Get客戶聯絡人Repository();
        // GET: Base
    }
}