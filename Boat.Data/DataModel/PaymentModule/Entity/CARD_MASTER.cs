
using Dapper.Contrib.Extensions;
using System;

namespace Boat.Backoffice.DataModel.PaymentModule.Entity
{
    [Table("CARD_MASTER")]
    public class CardMaster
    {
        public string GUID { get; set; }
        public Int16 RECORD_STATUS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public string CONVERSATION_ID { get; set; }
        [Key]
        public long CUSTOMER_NUMBER { get; set; }
        public string CARD_ALIAS { get; set; }
        public string CARD_HOLDER_NAME { get; set; }
        public string CARD_REF_NUMBER { get; set; }
        public string CARD_MASKED_NUMBER { get; set; }
        public string CARD_EXPIRE_YEAR { get; set; }
        public string CARD_EXPIRE_MONTH { get; set; }
        public string CARD_EXPIRE_DATE { get; set; }
        public string CARD_CVV { get; set; }
    }
}
