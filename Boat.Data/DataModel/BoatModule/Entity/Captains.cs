using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.BoatModule.Entity
{
    [Table("CAPTAINS")]
    public class Captains
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public long BOAT_ID { get; set; }
        [Key]
        public long CAPTAIN_ID { get; set; }
        public string CAPTAIN_NAME { get; set; }
        public string CAPTAIN_MIDDLE_NAME { get; set; }
        public string CAPTAIN_SURNAME { get; set; }
        public string CAPTAIN_INFO { get; set; }
        public string PHONE_NUMBER { get; set; }
        public long IDENTIFICATION_ID { get; set; }
        public string EMAIL { get; set; }
    }
}
