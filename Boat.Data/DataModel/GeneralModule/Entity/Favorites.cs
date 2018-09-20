using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.GeneralModule.Entity
{
    [Table("FAVORITES")]
    public class Favorites
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long FAVORITE_ID { get; set; }
        public long CUSTOMER_NUMBER { get; set; }
        public long BOAT_ID { get; set; }
    }
}
