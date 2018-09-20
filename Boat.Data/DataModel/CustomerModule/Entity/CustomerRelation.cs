using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.CustomerModule.Entity
{

    [Table("CUSTOMER_RELATION")]
    public class CustomerRelation
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long RELATION_NUMBER { get; set; }
        public long CUSTOMER_NUMBER { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_MIDDLE_NAME { get; set; }
        public string CUSTOMER_SURNAME { get; set; }
        public long IDENTIFICATION_ID { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string GENDER { get; set; }

    }
}
