using Boat.Backoffice.Utility;
using Boat.Data.DataModel.GeneralModule.Entity;
using Boat.Data.DataModel.GeneralModule.Service.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Boat.Data.DataModel.GeneralModule.Service
{
   public class ComplaintsService : IComplaintsService
    {
        public Complaints SelectByCustomerNumber(long customerNumber)
        {
            Complaints _complatins = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Complaints> complaints = sqlConnection.Query<Complaints>("select * from COMPLATINS where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (complaints.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_NOT_FOUND);
                else
                    _complatins = complaints.FirstOrDefault();
            }

            return _complatins;
        }

        public Complaints Update(Complaints cust)
        {
            Complaints _complatins = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _complatins = sqlConnection.Get<Complaints>(cust.CUSTOMER_NUMBER);
                _complatins.RECORD_STATUS = 1;
                _complatins.UPDATE_DATE = DateTime.Now;
                _complatins.UPDATE_USER = cust.UPDATE_USER;
                _complatins.CONTENT_HEADER = cust.CONTENT_HEADER;
                _complatins.CONTENT_TEXT = cust.CONTENT_TEXT;
                _complatins.PHOTO = cust.PHOTO;
                _complatins.CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER;
                _complatins.EMAIL = cust.EMAIL;
                _complatins.PHONE_NUMBER = cust.PHONE_NUMBER;
                _complatins.CONFIRM = cust.CONFIRM;

                sqlConnection.Update<Complaints>(_complatins);

                var result = sqlConnection.Get<Complaints>(cust.CUSTOMER_NUMBER);

            }

            return _complatins;
        }

        public long Insert(Complaints cust)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _complatins = new Complaints()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = cust.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = cust.UPDATE_USER,
                    CUSTOMER_NUMBER = cust.CUSTOMER_NUMBER,
                    RESERVATION_ID = cust.RESERVATION_ID,
                    CONTENT_HEADER = cust.CONTENT_HEADER,
                    CONTENT_TEXT = cust.CONTENT_TEXT,
                    EMAIL = cust.EMAIL,
                    PHONE_NUMBER = cust.PHONE_NUMBER,
                    PHOTO = cust.PHOTO,
                    CONFIRM = cust.CONFIRM
                };

                var complaintGuid = sqlConnection.Insert<Complaints>(_complatins);

                sqlConnection.Close();

                return complaintGuid;
            }
        }

        public Complaints Delete(Complaints cust)
        {
            Complaints _complaints = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _complaints = sqlConnection.Get<Complaints>(cust.CUSTOMER_NUMBER);
                _complaints.RECORD_STATUS = 0;
                _complaints.UPDATE_DATE = DateTime.Now;
                _complaints.UPDATE_USER = cust.UPDATE_USER;

                sqlConnection.Update<Complaints>(_complaints);

                var result = sqlConnection.Get<Complaints>(cust.CUSTOMER_NUMBER);

            }

            return _complaints;
        }
    }
}
