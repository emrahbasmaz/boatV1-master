using Boat.Data.DataModel.GeneralModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.GeneralModule.Service.Interface
{
    public interface IRegionService
    {
        Region SelectByRegionId(long regionId);
        List<Region> SelectAllRegion();
        Region Update(Region region);
        long Insert(Region region);
        Region Delete(Region region);
    }
}
