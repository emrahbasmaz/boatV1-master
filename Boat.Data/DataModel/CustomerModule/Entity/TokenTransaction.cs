
using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.CustomerModule.Entity
{
    [Table("TOKEN_TRANSACTION")]
    public class TokenTransaction
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long TOKEN_ID { get; set; }
        public long CUSTOMER_NUMBER { get; set; }
        public string TOKEN { get; set; }

    }
}
