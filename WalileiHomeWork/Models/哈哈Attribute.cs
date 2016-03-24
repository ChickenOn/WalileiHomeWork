using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WalileiHomeWork.Models
{
    public class 哈哈Attribute : DataTypeAttribute
    {
        public 哈哈Attribute() : base(DataType.Text)
        {
            
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                Regex reg = new Regex(@"\d{4}-\d{6}");
                var matches = reg.Match(value.ToString());
                return matches.Success;
            }
            else
                return false;
        }
    }
}