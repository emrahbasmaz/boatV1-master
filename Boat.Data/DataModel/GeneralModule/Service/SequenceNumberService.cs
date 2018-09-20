using Boat.Backoffice.Utility;
using Boat.Data.DataModel.GeneralModule.Entity;
using Boat.Data.DataModel.GeneralModule.Service.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Boat.Data.DataModel.GeneralModule.Service
{
    public class SequenceNumberService : ISequenceNumberService
    {
        public long SelectByNewCustomerId()
        {
            string NextSequenceSql = "DECLARE @value BIGINT;EXECUTE GetCustomerSEQUENCE @value OUTPUT;SELECT @value AS ID ";

            SequenceNumber _sequence = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<SequenceNumber> customerNumber = sqlConnection.Query<SequenceNumber>(NextSequenceSql);
                _sequence = customerNumber.FirstOrDefault();
            }

            return _sequence.ID;
        }

        public long SelectByNewId()
        {
            string NextSequenceSql = "DECLARE @value BIGINT;EXECUTE GetSEQUENCE @value OUTPUT;SELECT @value AS ID ";

            SequenceNumber _sequence = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<SequenceNumber> customerNumber = sqlConnection.Query<SequenceNumber>(NextSequenceSql);
                _sequence = customerNumber.FirstOrDefault();
            }

            return _sequence.ID;
        }
    }
}
