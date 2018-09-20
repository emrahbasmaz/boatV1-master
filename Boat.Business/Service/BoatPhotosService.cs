using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Interface;
using Boat.Framework.Service;

namespace Boat.Business.Service
{
    public class BoatPhotosService : Service<BoatPhotos, long, IBoatPhotosRepository>, IBoatPhotosService
    {
        public BoatPhotosService(IBoatPhotosRepository repository) : base(repository)
        {
        }
    }
}
