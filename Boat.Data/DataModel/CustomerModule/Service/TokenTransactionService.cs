using Boat.Backoffice.Utility;
using Boat.Data.DataModel.CustomerModule.Entity;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service
{
   public class TokenTransactionService
    {
        public TokenTransaction SelectByCustomerNumber(long customerNumber)
        {
            TokenTransaction _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<TokenTransaction> customer = sqlConnection.Query<TokenTransaction>("select * from TOKEN_TRANSACTION where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (customer.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_NOT_FOUND);
                else
                    _customer = customer.FirstOrDefault();
            }

            return _customer;
        }

        public long Insert(TokenTransaction cust)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _customer = new TokenTransaction()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = cust.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = cust.UPDATE_USER,
                    CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER,
                    TOKEN = cust.TOKEN
                };

                var customerGuid = sqlConnection.Insert<TokenTransaction>(_customer);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        public bool Delete(TokenTransaction cust)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
                {
                    sqlConnection.Open();
                    sqlConnection.Delete<TokenTransaction>(cust);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
