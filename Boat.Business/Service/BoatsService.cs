using Boat.Data.DataModel.BoatModule.Entity;
using Boat.Data.DataModel.BoatModule.Interface;
using Boat.Framework.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Business.Service
{
    public class BoatsService : Service<Boats, long, IBoatsRepository>, IBoatsService
    {
        public BoatsService(IBoatsRepository repository) : base(repository)
        {
        }
    }
}
