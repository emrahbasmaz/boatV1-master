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
   public class CustomerAddressService : ICustomerAddressService
    {
        public CustomerAddress SelectByCustomerNumber(long customerNumber)
        {
            CustomerAddress _customerAddress = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<CustomerAddress> customerAddress = sqlConnection.Query<CustomerAddress>("select * from CUSTOMER_ADDRESS where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (customerAddress.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_ADDRESS_NOT_FOUND);
                else
                    _customerAddress = customerAddress.FirstOrDefault();
            }

            return _customerAddress;
        }

        public bool Update(CustomerAddress cust)
        {
            CustomerAddress _customer = null;
            bool response = false;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<CustomerAddress>(cust.CUSTOMER_NUMBER);
                _customer.RECORD_STATUS = 1;
                _customer.UPDATE_DATE = DateTime.Now;
                _customer.UPDATE_USER = cust.UPDATE_USER;
                _customer.CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER;
                _customer.CITY = cust.CITY;
                _customer.COUNTRY = cust.COUNTRY;
                _customer.DESCRIPTION = cust.DESCRIPTION;
                _customer.ZIPCODE = cust.ZIPCODE;

                sqlConnection.Update<CustomerAddress>(_customer);

                var result = sqlConnection.Get<CustomerAddress>(cust.CUSTOMER_NUMBER);
                return response = true;
            }

            return response;
        }

        public long Insert(CustomerAddress cust)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _customer = new CustomerAddress()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = cust.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = cust.UPDATE_USER,
                    CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER,
                    CITY = cust.CITY,
                    COUNTRY = cust.COUNTRY,
                    DESCRIPTION = cust.DESCRIPTION,
                    ZIPCODE = cust.ZIPCODE
                };

                var customerGuid = sqlConnection.Insert<CustomerAddress>(_customer);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        public bool Delete(CustomerAddress cust)
        {
            bool response = false;
            CustomerAddress _customer = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _customer = sqlConnection.Get<CustomerAddress>(cust.CUSTOMER_NUMBER);
                _customer.RECORD_STATUS = 0;
                _customer.UPDATE_DATE = DateTime.Now;
                _customer.UPDATE_USER = cust.UPDATE_USER;

                sqlConnection.Update<CustomerAddress>(_customer);

                var result = sqlConnection.Get<CustomerAddress>(cust.CUSTOMER_NUMBER);

                return response = true;
            }

            return response;
        }
    }
}
