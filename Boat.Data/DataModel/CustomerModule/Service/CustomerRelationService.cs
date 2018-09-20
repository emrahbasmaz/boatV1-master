using Boat.Backoffice.Utility;
using Boat.Data.DataModel.CustomerModule.Entity;
using Boat.Data.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service
{
    public class CustomerRelationService
    {
        public List<CustomerRelation> SelectByCustomerNumber(long customerNumber)
        {
            List<CustomerRelation> _customer = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<CustomerRelation> customer = sqlConnection.Query<CustomerRelation>("select * from CUSTOMER_RELATION where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (customer.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_RELATION_NOT_FOUND);
                else
                    _customer = customer.ToList();
            }

            return _customer;
        }

        public long Insert(CustomerRelation cust)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _customer = new CustomerRelation()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = cust.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = cust.UPDATE_USER,
                    CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER,
                    CUSTOMER_NAME = cust.CUSTOMER_NAME,
                    CUSTOMER_MIDDLE_NAME = cust.CUSTOMER_MIDDLE_NAME,
                    CUSTOMER_SURNAME = cust.CUSTOMER_SURNAME,
                    IDENTIFICATION_ID = cust.IDENTIFICATION_ID,
                    EMAIL = cust.EMAIL,
                    PHONE_NUMBER = cust.PHONE_NUMBER,
                    GENDER = cust.GENDER
                };

                var customerGuid = sqlConnection.Insert<CustomerRelation>(_customer);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        public CustomerRelation Update(CustomerRelation cust)
        {
            CustomerRelation _customer = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<CustomerRelation>(cust.RELATION_NUMBER);
                _customer.RECORD_STATUS = 1;
                _customer.UPDATE_DATE = DateTime.Now;
                _customer.UPDATE_USER = cust.UPDATE_USER;
                _customer.CUSTOMER_NAME = cust.CUSTOMER_NAME;
                _customer.CUSTOMER_MIDDLE_NAME = cust.CUSTOMER_MIDDLE_NAME;
                _customer.CUSTOMER_SURNAME = cust.CUSTOMER_SURNAME;
                _customer.RELATION_NUMBER = cust.RELATION_NUMBER;
                _customer.CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER;
                _customer.EMAIL = cust.EMAIL;
                _customer.PHONE_NUMBER = cust.PHONE_NUMBER;
                _customer.GENDER = cust.GENDER;

                sqlConnection.Update<CustomerRelation>(_customer);

                var result = sqlConnection.Get<CustomerRelation>(cust.RELATION_NUMBER);

            }

            return _customer;
        }

        public CustomerRelation Delete(CustomerRelation cust)
        {
            CustomerRelation _customer = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<CustomerRelation>(cust.RELATION_NUMBER);
                _customer.RECORD_STATUS = 0;
                _customer.UPDATE_DATE = DateTime.Now;
                _customer.UPDATE_USER = cust.UPDATE_USER;
                _customer.CUSTOMER_NAME = cust.CUSTOMER_NAME;
                _customer.CUSTOMER_MIDDLE_NAME = cust.CUSTOMER_MIDDLE_NAME;
                _customer.CUSTOMER_SURNAME = cust.CUSTOMER_SURNAME;
                _customer.RELATION_NUMBER = cust.RELATION_NUMBER;
                _customer.EMAIL = cust.EMAIL;
                _customer.PHONE_NUMBER = cust.PHONE_NUMBER;
                _customer.GENDER = cust.GENDER;

                sqlConnection.Update<CustomerRelation>(_customer);

                var result = sqlConnection.Get<CustomerRelation>(cust.RELATION_NUMBER);

            }

            return _customer;
        }
    }
}
