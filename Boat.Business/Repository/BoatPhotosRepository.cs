using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Interface;
using Boat.Framework.GenericRepository;
using Dapper;

namespace Boat.Business.Repository
{
    public class BoatPhotosRepository : GenericRepository<BoatPhotos, long>, IBoatPhotosRepository
    {
    }
}
