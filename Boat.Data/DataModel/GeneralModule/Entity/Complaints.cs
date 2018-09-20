using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.GeneralModule.Entity
{
    [Table("COMPLATINS")]
    public class Complaints
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long COMPLAINT_ID { get; set; }
        public string CONTENT_HEADER { get; set; }
        public string CONTENT_TEXT { get; set; }
        public long RESERVATION_ID { get; set; }
        public long CUSTOMER_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string PHOTO { get; set; }
        public Int16 CONFIRM { get; set; }
    }
}
