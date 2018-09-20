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

namespace Boat.Data.DataModel.PaymentModule.Service
{
    public class CardMasterService : ICardMasterService
    {
        public CardMaster SelectByCustomerNumber(long customerNumber)
        {
            CardMaster _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<CardMaster> card_master = sqlConnection.Query<CardMaster>("select * from CARD_MASTER where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (card_master.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_NOT_FOUND);
                else
                    _cardMaster = card_master.FirstOrDefault();
            }

            return _cardMaster;
        }

        public CardMaster SelectByCardRefNumber(long cardRefNumber)
        {
            CardMaster _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<CardMaster> card_master = sqlConnection.Query<CardMaster>("select * from CARD_MASTER where CARD_REF_NUMBER = @id and RECORD_STATUS = 1", new { id = cardRefNumber });
                if (card_master.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_NOT_FOUND);
                else
                    _cardMaster = card_master.FirstOrDefault();
            }

            return _cardMaster;
        }

        public CardMaster Update(CardMaster card)
        {
            CardMaster _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _cardMaster = sqlConnection.Get<CardMaster>(card.CUSTOMER_NUMBER);
                _cardMaster.RECORD_STATUS = 1;
                _cardMaster.UPDATE_DATE = DateTime.Now;
                _cardMaster.UPDATE_USER = card.UPDATE_USER;
                _cardMaster.CARD_ALIAS = card.CARD_ALIAS;
                _cardMaster.CARD_CVV = card.CARD_CVV;
                _cardMaster.CARD_EXPIRE_DATE = card.CARD_EXPIRE_DATE;
                _cardMaster.CUSTOMER_NUMBER = card.CUSTOMER_NUMBER;
                _cardMaster.CARD_EXPIRE_MONTH = card.CARD_EXPIRE_MONTH;
                _cardMaster.CARD_EXPIRE_YEAR = card.CARD_EXPIRE_YEAR;
                _cardMaster.CARD_HOLDER_NAME = card.CARD_HOLDER_NAME;
                _cardMaster.CARD_MASKED_NUMBER = card.CARD_MASKED_NUMBER;
                _cardMaster.CARD_REF_NUMBER = card.CARD_REF_NUMBER;
                _cardMaster.CONVERSATION_ID = card.CONVERSATION_ID;


                sqlConnection.Update<CardMaster>(_cardMaster);

                var result = sqlConnection.Get<CardMaster>(_cardMaster.CUSTOMER_NUMBER);

            }

            return _cardMaster;
        }

        public long Insert(CardMaster card)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _cardMaster = new CardMaster()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = card.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = card.UPDATE_USER,
                    CUSTOMER_NUMBER = card.CUSTOMER_NUMBER,
                    CARD_ALIAS = card.CARD_ALIAS,
                    CARD_CVV = card.CARD_CVV,
                    CARD_EXPIRE_DATE = card.CARD_EXPIRE_DATE,
                    CARD_EXPIRE_MONTH = card.CARD_EXPIRE_MONTH,
                    CARD_EXPIRE_YEAR = card.CARD_EXPIRE_YEAR,
                    CARD_HOLDER_NAME = card.CARD_HOLDER_NAME,
                    CARD_MASKED_NUMBER = card.CARD_MASKED_NUMBER,
                    CARD_REF_NUMBER = card.CARD_REF_NUMBER,
                    CONVERSATION_ID = card.CONVERSATION_ID
                };

                var customerGuid = sqlConnection.Insert<CardMaster>(_cardMaster);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        public CardMaster Delete(CardMaster card)
        {
            CardMaster _cardMaster = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _cardMaster = sqlConnection.Get<CardMaster>(card.CUSTOMER_NUMBER);
                _cardMaster.RECORD_STATUS = 0;
                _cardMaster.UPDATE_DATE = DateTime.Now;
                _cardMaster.UPDATE_USER = card.UPDATE_USER;

                sqlConnection.Update<CardMaster>(_cardMaster);

                var result = sqlConnection.Get<CardMaster>(card.CUSTOMER_NUMBER);

            }

            return _cardMaster;
        }
    }
}
