using Boat.Data.DataModel.BoatModule.Entity;
using System.Collections.Generic;

namespace Boat.Data.DataModel.BoatModule.Service.Interface
{
   public interface IBoatPhotosService
    {
        List<BoatPhotos> SelectByBoatId(long boatId);
        BoatPhotos Update(BoatPhotos photo);
        long Insert(BoatPhotos photo);
        BoatPhotos Delete(BoatPhotos photo);
        bool DeleteByBoatId(Boats boat);
    }
}
