using Dapper.Contrib.Extensions;
using System;

namespace Boat.Data.DataModel.BoatModule.Entity
{
    [Table("BOAT_PHOTOS")]
    public class BoatPhotos
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long PHOTO_ID { get; set; }
        public string PHOTO { get; set; }
        public long BOAT_ID { get; set; }

    }
}
