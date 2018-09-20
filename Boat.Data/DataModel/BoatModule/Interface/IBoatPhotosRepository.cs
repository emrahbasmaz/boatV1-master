using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Framework.Interface;

namespace Boat.Data.DataModel.BoatModule.Interface
{
    public interface IBoatPhotosRepository : IRepository<BoatPhotos, long>, IRepository
    {
    }
}
