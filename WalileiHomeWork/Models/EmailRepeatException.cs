using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalileiHomeWork.Models
{
    public class EmailRepeatException:Exception
    {
        public override string Message
        {
            get
            {
                return "Email與其他聯絡人重複";
            }
        }
    }
}