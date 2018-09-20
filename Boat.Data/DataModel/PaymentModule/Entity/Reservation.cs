using Dapper.Contrib.Extensions;
using System;

namespace Boat.Backoffice.DataModel.PaymentModule.Entity
{
    [Table("RESERVATION")]
    public class Reservation
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        [Key]
        public long RESERVATION_ID { get; set; }
        public string PAYMENT_ID { get; set; }//BASKET_ID
        public long CUSTOMER_NUMBER { get; set; }
        public long BOAT_ID { get; set; }
        public string PRICE { get; set; }
        public string TOUR_TYPE { get; set; }
        public string PAYMENT_TYPE { get; set; }
        public DateTime RESERVATION_DATE { get; set; }
        public DateTime RESERVATION_END_DATE { get; set; }
        public string CAPACITY { get; set; }
        public Int16 CONFIRM { get; set; }
    }
}
