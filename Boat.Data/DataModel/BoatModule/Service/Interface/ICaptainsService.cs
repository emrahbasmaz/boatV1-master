using Boat.Data.DataModel.BoatModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.BoatModule.Service.Interface
{
   public interface ICaptainsService
    {
        Captains SelectByBoatId(long captainId);
        Captains Update(Captains captain);
        long Insert(Captains captain);
        Captains Delete(Captains captain);
    }
}
