using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.CustomerModule.Entity
{
    [Table("CUSTOMER_ADDRESS")]
    public class CustomerAddress
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long CUSTOMER_NUMBER { get; set; }
        public string CITY { get; set; }
        public string COUNTRY { get; set; }
        public string DESCRIPTION { get; set; }
        public string ZIPCODE { get; set; }
    }
}
