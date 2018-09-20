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
    public class BoatPhotosService : IBoatPhotosService
    {
        public List<BoatPhotos> SelectByBoatId(long boatId)
        {
            List<BoatPhotos> _photos = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                var sql = String.Format("select * from BOAT_PHOTOS where BOAT_ID = @id and RECORD_STATUS = 1", boatId);
                sqlConnection.Open();
                IEnumerable<BoatPhotos> photos = SqlMapper.QueryAsync<BoatPhotos>(sqlConnection, sql).Result.ToList();
                //sqlConnection.Query<BoatPhotos>("select * from BOAT_PHOTOS where BOAT_ID = @id and RECORD_STATUS = 1", new { id = boatId });

                if (photos.Count() == 0)
                    throw new Exception("BOAT_PHOTOS_NOT_FOUND");
                else
                    _photos = photos.ToList();
            }

            return _photos;
        }

        public BoatPhotos Update(BoatPhotos photo)
        {
            BoatPhotos _photo = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _photo = sqlConnection.Get<BoatPhotos>(photo.BOAT_ID);
                _photo.RECORD_STATUS = 1;
                _photo.UPDATE_DATE = DateTime.Now;
                _photo.UPDATE_USER = photo.UPDATE_USER;
                _photo.PHOTO = photo.PHOTO;

                sqlConnection.Update<BoatPhotos>(_photo);

                var result = sqlConnection.Get<BoatPhotos>(photo.BOAT_ID);

            }

            return _photo;
        }

        public long Insert(BoatPhotos photo)
        {
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _photo = new BoatPhotos()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = photo.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = photo.UPDATE_USER,
                    BOAT_ID = photo.BOAT_ID,
                    PHOTO = photo.PHOTO
                };

                var boatGuid = sqlConnection.Insert<BoatPhotos>(_photo);

                sqlConnection.Close();

                return boatGuid;
            }
        }

        public BoatPhotos Delete(BoatPhotos photo)
        {
            BoatPhotos _photo = null;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                _photo = sqlConnection.Get<BoatPhotos>(photo.BOAT_ID);
                _photo.RECORD_STATUS = 0;
                _photo.UPDATE_DATE = DateTime.Now;
                _photo.UPDATE_USER = photo.UPDATE_USER;

                sqlConnection.Update<BoatPhotos>(_photo);

                var result = sqlConnection.Get<BoatPhotos>(photo.BOAT_ID);

            }

            return _photo;
        }

        public bool DeleteByBoatId(Boats boat)
        {
            bool _photo = false;
            using (var sqlConnection = new SqlConnection(DbDbConstant.DatabaseConnection))
            {
                sqlConnection.Open();

                IEnumerable<BoatPhotos> photos = sqlConnection.Query<BoatPhotos>("UPDATE BOAT_PHOTOS SET RECORD_STATUS = 0,UPDATE_DATE = GETDATE(),UPDATE_USER = @updateUser where BOAT_ID = @id", new { id = boat.BOAT_ID, updateUser = boat.UPDATE_USER });

                if (photos != null && photos.Count() > 0)
                    _photo = true;
            }

            return _photo;
        }
    }
}
