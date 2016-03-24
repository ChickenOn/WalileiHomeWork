namespace WalileiHomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(View_CustomerDetailMetaData))]
    public partial class View_CustomerDetail
    {
    }
    
    public partial class View_CustomerDetailMetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> BankCount { get; set; }
        public Nullable<int> Contact { get; set; }
    }
}
