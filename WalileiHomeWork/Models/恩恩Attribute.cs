using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WalileiHomeWork.Models
{
    public class 恩恩Attribute : DataTypeAttribute
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public 恩恩Attribute():base(DataType.Text)
        {

        }
        public override bool IsValid(object value)
        {

            if (value != null)
            {
               var one = db.客戶聯絡人.FirstOrDefault(r => r.Email == value.ToString());
                return one == null;
            }
            else
                return false;
        }
    }
}