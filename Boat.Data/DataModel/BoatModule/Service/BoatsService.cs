using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Service.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Boat.Data.Utility;

namespace Boat.Data.DataModel.BoatModule.Service
{
    public class BoatsService : IBoatsService
    {
        public Boats SelectByBoatId(long boatId)
        {
            Boats _boat = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Boats> customer = sqlConnection.Query<Boats>("select * from BOATS where BOAT_ID = @id and RECORD_STATUS = 1", new { id = boatId });
                _boat = customer.FirstOrDefault();
            }

            return _boat;
        }

        public List<Boats> SelectAllBoat()
        {
            List<Boats> _boat = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Boats> allboats = sqlConnection.Query<Boats>("select * from BOATS where  RECORD_STATUS = 1");
                _boat = allboats.ToList();
            }

            return _boat;
        }

        public List<Boats> SelectByRegionId(long regionId)
        {
            List<Boats> _boat = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Boats> boats = sqlConnection.Query<Boats>("select * from BOATS where REGION_ID = @id and RECORD_STATUS = 1", new { id = regionId });
                _boat = boats.ToList();
            }

            return _boat;
        }

        public Boats Update(Boats boat)
        {
            Boats _boat = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _boat = sqlConnection.Get<Boats>(boat.BOAT_ID);
                _boat.RECORD_STATUS = 1;
                _boat.UPDATE_DATE = DateTime.Now;
                _boat.UPDATE_USER = boat.UPDATE_USER;
                _boat.BOAT_INFO = boat.BOAT_INFO;
                _boat.ROTA_INFO = boat.ROTA_INFO;
                _boat.QUANTITY = boat.QUANTITY;
                _boat.FLAG = boat.FLAG;
                _boat.BOAT_NAME = boat.BOAT_NAME;
                _boat.CAPTAIN_ID = boat.CAPTAIN_ID;
                _boat.PRICE = boat.PRICE;
                _boat.PRIVATE_PRICE = boat.PRIVATE_PRICE;
                _boat.TOUR_TYPE = boat.TOUR_TYPE;

                sqlConnection.Update<Boats>(_boat);

                var result = sqlConnection.Get<Boats>(boat.BOAT_ID);

            }

            return _boat;
        }

        public long Insert(Boats boat)
        {
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _boat = new Boats()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = boat.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = boat.UPDATE_USER,
                    BOAT_INFO = boat.BOAT_INFO,
                    BOAT_NAME = boat.BOAT_NAME,
                    CAPTAIN_ID = boat.CAPTAIN_ID,
                    FLAG = boat.FLAG,
                    QUANTITY = boat.QUANTITY,
                    ROTA_INFO = boat.ROTA_INFO,
                    PRICE = boat.PRICE,
                    PRIVATE_PRICE = boat.PRIVATE_PRICE,
                    TOUR_TYPE = boat.TOUR_TYPE,
                    REGION_ID = boat.REGION_ID
                };

                var boatGuid = sqlConnection.Insert<Boats>(_boat);

                sqlConnection.Close();

                return boatGuid;
            }
        }

        public Boats Delete(Boats boat)
        {
            Boats _boat = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _boat = sqlConnection.Get<Boats>(boat.BOAT_ID);
                _boat.RECORD_STATUS = 0;
                _boat.UPDATE_DATE = DateTime.Now;
                _boat.UPDATE_USER = boat.UPDATE_USER;

                sqlConnection.Update<Boats>(_boat);

                var result = sqlConnection.Get<Boats>(boat.BOAT_ID);

                //Delete Photo
                BoatPhotosService boatPhotosService = new BoatPhotosService();
                boatPhotosService.DeleteByBoatId(boat);
            }

            return _boat;
        }
    }
}
