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
    public class BoatsCapacityService : IBoatsCapacityService
    {
        public BoatsCapacity SelectByBoatId(BoatsCapacity request)
        {
            BoatsCapacity _boatCapacity = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<BoatsCapacity> boats_capacity = sqlConnection.Query<BoatsCapacity>("select * from BOATS_CAPACITY where BOAT_ID = @id and RESERVATION_DATE = @date and RECORD_STATUS = 1", new { id = request.BOAT_ID, date = request.RESERVATION_DATE });
                if (boats_capacity.Count() == 0)
                    boats_capacity = null;
                //throw new Exception(CommonDefinitions.BOAT_NOT_FOUND);
                else
                    _boatCapacity = boats_capacity.FirstOrDefault();
            }

            return _boatCapacity;
        }

        public bool Update(BoatsCapacity request)
        {
            try
            {
                BoatsCapacity _boatCapacity = null;
                using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
                {
                    sqlConnection.Open();

                    IEnumerable<BoatsCapacity> boats_capacity = sqlConnection.Query<BoatsCapacity>("select * from BOATS_CAPACITY where BOAT_ID = @id and RESERVATION_DATE = @date and RECORD_STATUS = 1", new { id = request.BOAT_ID, date = request.RESERVATION_DATE });
                    if (boats_capacity.Count() == 0)
                        throw new Exception(CommonDefinitions.BOAT_NOT_FOUND);
                    else
                        _boatCapacity = boats_capacity.FirstOrDefault();

                    _boatCapacity.RECORD_STATUS = 1;
                    _boatCapacity.INSERT_DATE = request.INSERT_DATE;
                    _boatCapacity.INSERT_USER = request.INSERT_USER;
                    _boatCapacity.UPDATE_DATE = DateTime.Now;
                    _boatCapacity.UPDATE_USER = request.UPDATE_USER;
                    _boatCapacity.RESERVATION_ID = request.RESERVATION_ID;
                    _boatCapacity.CAPACITY = request.CAPACITY;
                    _boatCapacity.BOAT_ID = request.BOAT_ID;
                    _boatCapacity.BOAT_CAPACITY_ID = request.BOAT_CAPACITY_ID;
                    _boatCapacity.RESERVATION_DATE = request.RESERVATION_DATE;
                    _boatCapacity.RESERVATION_END_DATE = request.RESERVATION_END_DATE;

                    sqlConnection.Update<BoatsCapacity>(_boatCapacity);

                    var result = sqlConnection.Get<BoatsCapacity>(_boatCapacity.BOAT_CAPACITY_ID);

                }

                return true;
            }
            catch (Exception ex)
            {
                //log.Error("Update BoatsCapacity has an ERROR: [ERROR : " + ex.Message + "]");
                return false;
            }

        }

        public long Insert(BoatsCapacity card)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _boatsCapacity = new BoatsCapacity()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = card.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = card.UPDATE_USER,
                    RESERVATION_ID = card.RESERVATION_ID,
                    BOAT_ID = card.BOAT_ID,
                    RESERVATION_DATE = card.RESERVATION_DATE,
                    RESERVATION_END_DATE = card.RESERVATION_END_DATE,
                    CAPACITY = card.CAPACITY,
                };

                var customerGuid = sqlConnection.Insert<BoatsCapacity>(_boatsCapacity);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        //Probably !!This method will not be use
        public bool Delete(BoatsCapacity request)
        {
            try
            {
                BoatsCapacity _boatsCapacity = null;
                using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
                {
                    sqlConnection.Open();
                    _boatsCapacity = sqlConnection.Get<BoatsCapacity>(request.BOAT_ID);
                    _boatsCapacity.RECORD_STATUS = 0;
                    _boatsCapacity.UPDATE_DATE = DateTime.Now;
                    _boatsCapacity.UPDATE_USER = request.UPDATE_USER;

                    sqlConnection.Update<BoatsCapacity>(_boatsCapacity);

                    var result = sqlConnection.Get<BoatsCapacity>(_boatsCapacity.BOAT_ID);
                }

                return true;
            }
            catch (Exception ex)
            {
                //log.Error("Delete BoatsCapacity has an ERROR: [ERROR : " + ex.Message + "]");
                return false;
            }

        }
    }
}
