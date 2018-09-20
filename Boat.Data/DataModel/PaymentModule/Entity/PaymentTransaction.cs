using Dapper.Contrib.Extensions;
using System;

namespace Boat.Backoffice.DataModel.PaymentModule.Entity
{
    [Table("PAYMENT_TRANSACTION")]
    public class PaymentTransaction
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public string CONVERSATION_ID { get; set; }
        [Key]
        public string PAYMENT_ID { get; set; }//BASKET_ID
        public long CUSTOMER_NUMBER { get; set; }
        public string CARD_HOLDER_NAME { get; set; }
        public string CARD_REF_NUMBER { get; set; }
        public string PRICE { get; set; }
        public string PAID_PRICE { get; set; }
        public string PAYMENT_CHANNEL { get; set; }
        public string CALLBACK_URL { get; set; }
        public string IP { get; set; }
        public string CURRENCY { get; set; }
        public long BOAT_ID { get; set; }
        public string TOUR_TYPE { get; set; }
        public string PAYMENT_TYPE { get; set; }
    }
}
