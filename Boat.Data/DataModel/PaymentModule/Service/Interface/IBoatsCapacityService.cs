using Boat.Backoffice.DataModel.PaymentModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.PaymentModule.Service.Interface
{
    public interface IBoatsCapacityService
    {
        BoatsCapacity SelectByBoatId(BoatsCapacity request);
        bool Update(BoatsCapacity request);
        long Insert(BoatsCapacity card);
        bool Delete(BoatsCapacity request);
    }
}
