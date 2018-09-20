using Boat.Data.DataModel.BoatModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.BoatModule.Service.Interface
{
    public interface IBoatsService
    {
        Boats SelectByBoatId(long boatId);
        List<Boats> SelectAllBoat();
        List<Boats> SelectByRegionId(long regionId);
        Boats Update(Boats boat);
        long Insert(Boats boat);
        Boats Delete(Boats boat);
    }
}
