using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.BoatModule.Entity
{
    [Table("BOATS")]
    public class Boats
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long BOAT_ID { get; set; }
        public string BOAT_NAME { get; set; }
        public string FLAG { get; set; }
        public Int32 QUANTITY { get; set; }
        public string ROTA_INFO { get; set; }
        public string BOAT_INFO { get; set; }
        public long CAPTAIN_ID { get; set; }
        public int REGION_ID { get; set; }
        public string PRICE { get; set; }
        public string TOUR_TYPE { get; set; }
        public string PRIVATE_PRICE { get; set; }
    }
}
