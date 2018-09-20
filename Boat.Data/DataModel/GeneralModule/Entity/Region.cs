using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.GeneralModule.Entity
{
    [Table("REGION")]
    public class Region
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public int REGION_ID { get; set; }
        public string REGION_NAME { get; set; }
    }
}

