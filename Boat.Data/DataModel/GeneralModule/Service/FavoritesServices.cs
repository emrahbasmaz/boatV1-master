using Boat.Backoffice.Utility;
using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Service.Interface;
using Boat.Data.DataModel.CustomerModule.Entity;
using Boat.Data.DataModel.CustomerModule.Service.Interface;
using Boat.Data.DataModel.GeneralModule.Entity;
using Boat.Data.DataModel.GeneralModule.Service.Interface;
using Boat.Data.Dto.BoatModule.Response;
using Boat.Data.Dto.GeneralModule.Response;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Boat.Data.DataModel.GeneralModule.Service
{
    public class FavoritesServices : IFavoritesServices
    {
        private readonly IBoatsService boatsService;
        private readonly IRegionService regionService;
        private readonly ICustomerService customerService;

        public Favorites SelectByBoatId(long boatId)
        {
            Favorites _favorite = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Favorites> favorite = sqlConnection.Query<Favorites>("select * from FAVORITES where BOAT_ID = @id and RECORD_STATUS = 1", new { id = boatId });

                if (favorite.Count() == 0)
                    throw new Exception(CommonDefinitions.BOAT_NOT_FOUND);
                else
                    _favorite = favorite.FirstOrDefault();
            }

            return _favorite;
        }
        public List<ResponseBoats> SelectPopularBoats()
        {
            Boats boat = null;
            List<ResponseBoats> _favorite = new List<ResponseBoats>();
            List<ResponsePopularBoats> respFavorites = new List<ResponsePopularBoats>();
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<ResponsePopularBoats> favorite = sqlConnection.Query<ResponsePopularBoats>("select distinct (BOAT_ID) as BoatId , COUNT(*) OVER (PARTITION BY BOAT_ID) AS Count from FAVORITES(NOLOCK) where  RECORD_STATUS = 1  order by Count desc");

                if (favorite.Count() == 0)
                    throw new Exception(CommonDefinitions.BOAT_NOT_FOUND);
                else
                    respFavorites = favorite.ToList();

                //Most popular 3 boat informations
                for (int i = 0; i < 3; i++)
                {
                    if (i < respFavorites.Count())
                    {
                        boat = new Boats();
                        boat = boatsService.SelectByBoatId(respFavorites[i].BoatId);
                        ResponseBoats response = new ResponseBoats
                        {
                            INSERT_USER = boat.INSERT_USER,
                            UPDATE_USER = boat.UPDATE_USER,
                            BOAT_ID = boat.BOAT_ID,
                            BOAT_INFO = boat.BOAT_INFO,
                            BOAT_NAME = boat.BOAT_NAME,
                            CAPTAIN_ID = boat.CAPTAIN_ID,
                            FLAG = boat.FLAG,
                            QUANTITY = boat.QUANTITY,
                            ROTA_INFO = boat.ROTA_INFO,
                            REGION_ID = boat.REGION_ID,
                            REGION_NAME = regionService.SelectByRegionId(boat.REGION_ID).REGION_NAME,
                            PRICE = boat.PRICE,
                            PRIVATE_PRICE = boat.PRIVATE_PRICE,
                            TOUR_TYPE = boat.TOUR_TYPE,
                            header = new Dto.ResponseHeader
                            {
                                IsSuccess = true,
                                ResponseCode = CommonDefinitions.SUCCESS,
                                ResponseMessage = CommonDefinitions.SUCCESS_MESSAGE,
                            }
                        };
                        _favorite.Add(response);
                    }

                }
                return _favorite;

            }
        }
        public List<Favorites> SelectByCustomerNumber(long customerNumber)
        {
            List<Favorites> _favorite = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Favorites> favorite = sqlConnection.Query<Favorites>("select * from FAVORITES where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });

                if (favorite.Count() == 0)
                    throw new Exception(CommonDefinitions.BOAT_NOT_FOUND);
                else
                    _favorite = favorite.ToList();
            }

            return _favorite;
        }

        public Favorites Update(Favorites fav)
        {
            Favorites _favorite = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _favorite = sqlConnection.Get<Favorites>(fav.CUSTOMER_NUMBER);
                _favorite.RECORD_STATUS = 1;
                _favorite.UPDATE_DATE = DateTime.Now;
                _favorite.UPDATE_USER = fav.UPDATE_USER;
                _favorite.BOAT_ID = fav.BOAT_ID;
                _favorite.CUSTOMER_NUMBER = fav.CUSTOMER_NUMBER;

                sqlConnection.Update<Favorites>(_favorite);

                var result = sqlConnection.Get<Favorites>(fav.CUSTOMER_NUMBER);

            }

            return _favorite;
        }
        public long Insert(Favorites fav)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _favorites = new Favorites()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = fav.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = fav.UPDATE_USER,
                    CUSTOMER_NUMBER = fav.CUSTOMER_NUMBER,
                    BOAT_ID = fav.BOAT_ID
                };

                var complaintGuid = sqlConnection.Insert<Favorites>(_favorites);

                sqlConnection.Close();

                return complaintGuid;
            }
        }
        public bool Delete(Favorites fav)
        {
            try
            {
                Favorites _favorite = null;
                using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
                {
                    sqlConnection.Open();
                    _favorite = SelectByBoatId(fav.BOAT_ID);
                    _favorite.RECORD_STATUS = 0;
                    _favorite.UPDATE_DATE = DateTime.Now;
                    _favorite.UPDATE_USER = fav.UPDATE_USER;

                    sqlConnection.Update<Favorites>(_favorite);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteAllforCustomer(long customerNumber)
        {
            try
            {
                Customer cust = null;
                using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
                {
                    sqlConnection.Open();
                    cust = customerService.SelectByCustomerNumber(customerNumber);
                    IEnumerable<Favorites> favorite = sqlConnection.Query<Favorites>("UPDATE FAVORITES SET RECORD_STATUS = 0,UPDATE_DATE = GETDATE(),UPDATE_USER ='@updateUser' where CUSTOMER_NUMBER = @id ", new { id = customerNumber, updateUser = cust.UPDATE_USER });
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
