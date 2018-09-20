using Boat.Data.DataModel.GeneralModule.Entity;
using Boat.Data.DataModel.GeneralModule.Service.Interface;
using Boat.Backoffice.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace Boat.Data.DataModel.GeneralModule.Service
{
    public class RegionService : IRegionService
    {
        public Region SelectByRegionId(long regionId)
        {
            Region _region = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Region> regions = sqlConnection.Query<Region>("select * from REGION where REGION_ID = @id and RECORD_STATUS = 1", new { id = regionId });
                if (regions.Count() == 0)
                    throw new Exception(CommonDefinitions.CUSTOMER_NOT_FOUND);
                else
                    _region = regions.FirstOrDefault();
            }

            return _region;
        }

        public List<Region> SelectAllRegion()
        {
            List<Region> _region = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Region> regions = sqlConnection.Query<Region>("select * from REGION where  RECORD_STATUS = 1");
                if (regions.Count() == 0)
                    throw new Exception(CommonDefinitions.ERROR_MESSAGE);
                else
                    _region = regions.ToList();
            }

            return _region;
        }

        public Region Update(Region region)
        {
            Region _regions = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _regions = sqlConnection.Get<Region>(region.REGION_ID);
                _regions.RECORD_STATUS = 1;
                _regions.UPDATE_DATE = DateTime.Now;
                _regions.UPDATE_USER = region.UPDATE_USER;
                _regions.REGION_NAME = region.REGION_NAME;

                sqlConnection.Update<Region>(_regions);

                var result = sqlConnection.Get<Region>(region.REGION_ID);

            }

            return _regions;
        }

        public long Insert(Region region)
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _region = new Region()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = region.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = region.UPDATE_USER,
                    REGION_NAME = region.REGION_NAME

                };

                var regionId = sqlConnection.Insert<Region>(_region);

                sqlConnection.Close();

                return regionId;
            }
        }

        public Region Delete(Region region)
        {
            Region _regions = null;
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();
                _regions = sqlConnection.Get<Region>(region.REGION_ID);
                _regions.RECORD_STATUS = 0;
                _regions.UPDATE_DATE = DateTime.Now;
                _regions.UPDATE_USER = region.UPDATE_USER;

                sqlConnection.Update<Region>(_regions);

                var result = sqlConnection.Get<Region>(region.REGION_ID);

            }

            return _regions;
        }
    }
}
