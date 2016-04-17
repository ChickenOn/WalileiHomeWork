using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace WalileiHomeWork.Controllers
{
    internal class CalculateTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.dtStart = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var dtTimeSpan = (DateTime.Now - (DateTime)filterContext.Controller.ViewBag.dtStart).Milliseconds;
            filterContext.Controller.ViewBag.dtTimespan = dtTimeSpan;
            base.OnActionExecuted(filterContext);
            Debug.WriteLine("安安你好這頁總共花了"+dtTimeSpan.ToString()+"毫秒");
        }
    }
}