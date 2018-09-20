using Boat.Backoffice.Utility;
using Boat.Data.DataModel.CustomerModule.Entity;
using Boat.Data.DataModel.CustomerModule.Service.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service
{
    public class CustomerService : ICustomerService
    {
        public Customer SelectByGuid(long guid)
        {
            Customer _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<Customer>(guid);
            }

            return _customer;
        }

        public Customer SelectByCustomerNumber(long customerNumber)
        {
            Customer _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Customer> customer = sqlConnection.Query<Customer>("select * from CUSTOMER where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (customer.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_NOT_FOUND);
                else
                    _customer = customer.FirstOrDefault();
            }

            return _customer;
        }

        public Customer SelectByIdentificationId(long identificationId)
        {
            Customer _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Customer> customer = sqlConnection.Query<Customer>("select * from CUSTOMER where IDENTIFICATION_ID = @id", new { id = identificationId });
                _customer = (Customer)customer;
            }

            return _customer;
        }

        public Customer Update(Customer cust)
        {
            Customer _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<Customer>(cust.CUSTOMER_NUMBER);
                _customer.RECORD_STATUS = 1;
                _customer.UPDATE_DATE = DateTime.Now;
                _customer.UPDATE_USER = cust.UPDATE_USER;
                _customer.CUSTOMER_NAME = cust.CUSTOMER_NAME;
                _customer.CUSTOMER_MIDDLE_NAME = cust.CUSTOMER_MIDDLE_NAME;
                _customer.CUSTOMER_SURNAME = cust.CUSTOMER_SURNAME;
                _customer.CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER;
                _customer.EMAIL = cust.EMAIL;
                _customer.PHONE_NUMBER = cust.PHONE_NUMBER;
                _customer.GENDER = cust.GENDER;
                _customer.PASSWORD_HASH = cust.PASSWORD_HASH;
                _customer.PASSWORD_SALT = cust.PASSWORD_SALT;

                sqlConnection.Update<Customer>(_customer);

                var result = sqlConnection.Get<Customer>(cust.CUSTOMER_NUMBER);

            }

            return _customer;
        }

        public long Insert(Customer cust)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _customer = new Customer()
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
                    GENDER = cust.GENDER,
                    PASSWORD_HASH = cust.PASSWORD_HASH,
                    PASSWORD_SALT = cust.PASSWORD_SALT
                };

                var customerGuid = sqlConnection.Insert<Customer>(_customer);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        public Customer Delete(Customer cust)
        {
            Customer _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<Customer>(cust.CUSTOMER_NUMBER);
                _customer.RECORD_STATUS = 0;
                _customer.UPDATE_DATE = DateTime.Now;
                _customer.UPDATE_USER = cust.UPDATE_USER;

                sqlConnection.Update<Customer>(_customer);

                var result = sqlConnection.Get<Customer>(cust.CUSTOMER_NUMBER);

            }

            return _customer;
        }
    }
}
