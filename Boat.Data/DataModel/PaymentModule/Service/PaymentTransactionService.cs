using Boat.Backoffice.DataModel.PaymentModule.Entity;
using Boat.Backoffice.Utility;
using Boat.Data.DataModel.PaymentModule.Service.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Boat.Data.Dto.Enums;

namespace Boat.Data.DataModel.PaymentModule.Service
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        public List<PaymentTransaction> SelectByCustomerNumber(long customerNumber)
        {
            List<PaymentTransaction> _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<PaymentTransaction> card_master = sqlConnection.Query<PaymentTransaction>("select * from PAYMENT_TRANSACTION where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (card_master.Count() == 0)
                    throw new Exception(CommonDefinitions.PAYMENT_TRANSACTION_NOT_FOUND);
                else
                    _cardMaster = card_master.ToList();
            }

            return _cardMaster;
        }

        public PaymentTransaction SelectByCardRefNumber(long cardRefNumber)
        {
            PaymentTransaction _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<PaymentTransaction> card_master = sqlConnection.Query<PaymentTransaction>("select * from PAYMENT_TRANSACTION where CARD_REF_NUMBER = @id and RECORD_STATUS = 1", new { id = cardRefNumber });
                if (card_master.Count() == 0)
                    throw new Exception(CommonDefinitions.PAYMENT_TRANSACTION_NOT_FOUND);
                else
                    _cardMaster = card_master.FirstOrDefault();
            }

            return _cardMaster;
        }

        public PaymentTransaction SelectByPaymentId(long paymentId)
        {
            PaymentTransaction _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<PaymentTransaction> card_master = sqlConnection.Query<PaymentTransaction>("select * from PAYMENT_TRANSACTION where PAYMENT_ID = @id and RECORD_STATUS = 1", new { id = paymentId });
                if (card_master.Count() == 0)
                    throw new Exception(CommonDefinitions.PAYMENT_TRANSACTION_NOT_FOUND);
                else
                    _cardMaster = card_master.FirstOrDefault();
            }

            return _cardMaster;
        }

        public List<PaymentTransaction> SelectByBoatId(long boatId)
        {
            List<PaymentTransaction> _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<PaymentTransaction> card_master = sqlConnection.Query<PaymentTransaction>("select * from PAYMENT_TRANSACTION where BOAT_ID = @id and RECORD_STATUS = 1", new { id = boatId });
                if (card_master.Count() == 0)
                    throw new Exception(CommonDefinitions.PAYMENT_TRANSACTION_NOT_FOUND);
                else
                    _cardMaster = card_master.ToList();
            }

            return _cardMaster;
        }

        public PaymentTransaction Update(PaymentTransaction card)
        {
            PaymentTransaction _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _cardMaster = sqlConnection.Get<PaymentTransaction>(card.CUSTOMER_NUMBER);
                _cardMaster.RECORD_STATUS = 1;
                _cardMaster.UPDATE_DATE = DateTime.Now;
                _cardMaster.UPDATE_USER = card.UPDATE_USER;
                _cardMaster.PAYMENT_ID = card.PAYMENT_ID;
                _cardMaster.CONVERSATION_ID = card.CONVERSATION_ID;
                _cardMaster.CALLBACK_URL = card.CALLBACK_URL;
                _cardMaster.CUSTOMER_NUMBER = card.CUSTOMER_NUMBER;
                _cardMaster.CURRENCY = card.CURRENCY;
                _cardMaster.INSERT_DATE = card.INSERT_DATE;
                _cardMaster.CARD_HOLDER_NAME = card.CARD_HOLDER_NAME;
                _cardMaster.IP = card.IP;
                _cardMaster.CARD_REF_NUMBER = card.CARD_REF_NUMBER;
                _cardMaster.CONVERSATION_ID = card.CONVERSATION_ID;
                _cardMaster.PAID_PRICE = card.PAID_PRICE;
                _cardMaster.PAYMENT_CHANNEL = card.PAYMENT_CHANNEL;
                _cardMaster.BOAT_ID = card.BOAT_ID;
                _cardMaster.TOUR_TYPE = card.TOUR_TYPE;
                _cardMaster.PAYMENT_TYPE = Enum.GetName(typeof(PaymentType), Convert.ToInt16(card.PAYMENT_TYPE));

                sqlConnection.Update<PaymentTransaction>(_cardMaster);

                var result = sqlConnection.Get<PaymentTransaction>(_cardMaster.PAYMENT_ID);

            }

            return _cardMaster;
        }

        public long Insert(PaymentTransaction card)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _cardMaster = new PaymentTransaction()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = card.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = card.UPDATE_USER,
                    CUSTOMER_NUMBER = card.CUSTOMER_NUMBER,
                    PAYMENT_CHANNEL = card.PAYMENT_CHANNEL,
                    PAID_PRICE = card.PAID_PRICE,
                    PAYMENT_ID = card.PAYMENT_ID,
                    IP = card.IP,
                    CALLBACK_URL = card.CURRENCY,
                    CARD_HOLDER_NAME = card.CARD_HOLDER_NAME,
                    PRICE = card.PRICE,
                    CARD_REF_NUMBER = card.CARD_REF_NUMBER,
                    CONVERSATION_ID = card.CONVERSATION_ID,
                    BOAT_ID = card.BOAT_ID,
                    TOUR_TYPE = card.TOUR_TYPE,
                    PAYMENT_TYPE = Enum.GetName(typeof(PaymentType), Convert.ToInt16(card.PAYMENT_TYPE)),
                    CURRENCY = card.CURRENCY
                };

                var customerGuid = sqlConnection.Insert<PaymentTransaction>(_cardMaster);

                sqlConnection.Close();

                return customerGuid;
            }
        }
    }
}
