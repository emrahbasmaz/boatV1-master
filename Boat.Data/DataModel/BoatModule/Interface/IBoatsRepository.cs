using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Framework.Interface;

namespace Boat.Data.DataModel.BoatModule.Interface
{
   public interface IBoatsRepository : IRepository<Boats, long>
    {
    }
}
