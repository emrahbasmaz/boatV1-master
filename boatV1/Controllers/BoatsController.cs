using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Interface;
using boatV1.Base;
using Microsoft.AspNetCore.Mvc;

namespace boatV1.Controllers
{
    [Route("api/[controller]")]
    public class BoatsController : BaseController<BoatPhotos, long, IBoatPhotosRepository, IBoatPhotosService>
    {
        public BoatsController(IBoatPhotosService service) : base(service)
        {
        }
    }
}