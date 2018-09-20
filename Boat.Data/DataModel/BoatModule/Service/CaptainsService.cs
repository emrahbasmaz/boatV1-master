using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Service.Interface;
using Boat.Data.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Boat.Data.DataModel.BoatModule.Service
{
   public class CaptainsService : ICaptainsService
    {
        public Captains SelectByBoatId(long captainId)
        {
            Captains _captain = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Captains> captain = sqlConnection.Query<Captains>("select * from CAPTAINS where CAPTAIN_ID = @id", new { id = captainId });
                _captain = captain.FirstOrDefault();
            }

            return _captain;
        }

        public Captains Update(Captains captain)
        {
            Captains _captain = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _captain = sqlConnection.Get<Captains>(captain.CAPTAIN_ID);
                _captain.RECORD_STATUS = 1;
                _captain.UPDATE_DATE = DateTime.Now;
                _captain.UPDATE_USER = captain.UPDATE_USER;
                _captain.CAPTAIN_INFO = captain.CAPTAIN_INFO;
                _captain.BOAT_ID = captain.BOAT_ID;
                _captain.CAPTAIN_MIDDLE_NAME = captain.CAPTAIN_MIDDLE_NAME;
                _captain.CAPTAIN_NAME = captain.CAPTAIN_NAME;
                _captain.CAPTAIN_SURNAME = captain.CAPTAIN_SURNAME;
                _captain.EMAIL = captain.EMAIL;
                _captain.PHONE_NUMBER = captain.PHONE_NUMBER;

                sqlConnection.Update<Captains>(_captain);
                var result = sqlConnection.Get<Captains>(captain.BOAT_ID);

            }

            return _captain;
        }

        public long Insert(Captains captain)
        {
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _captain = new Captains()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = captain.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = captain.UPDATE_USER,
                    BOAT_ID = captain.BOAT_ID,
                    CAPTAIN_INFO = captain.CAPTAIN_INFO,
                    CAPTAIN_MIDDLE_NAME = captain.CAPTAIN_MIDDLE_NAME,
                    CAPTAIN_NAME = captain.CAPTAIN_NAME,
                    CAPTAIN_SURNAME = captain.CAPTAIN_SURNAME,
                    EMAIL = captain.EMAIL,
                    PHONE_NUMBER = captain.PHONE_NUMBER,
                    IDENTIFICATION_ID = captain.IDENTIFICATION_ID
                };

                var captainGuid = sqlConnection.Insert<Captains>(_captain);

                sqlConnection.Close();

                return captainGuid;
            }
        }

        public Captains Delete(Captains captain)
        {
            Captains _captain = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _captain = sqlConnection.Get<Captains>(captain.CAPTAIN_ID);
                _captain.RECORD_STATUS = 0;
                _captain.UPDATE_DATE = DateTime.Now;
                _captain.UPDATE_USER = captain.UPDATE_USER;

                sqlConnection.Update<Captains>(_captain);
                var result = sqlConnection.Get<Captains>(captain.CAPTAIN_ID);
            }
            return _captain;
        }
    }
}
