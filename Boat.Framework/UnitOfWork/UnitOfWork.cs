using Boat.Framework.Interface;
using Boat.Framework.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Framework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // public IRepository boatPhotosRepository => new GenericRepository<BoatPhotos,long>();

        public IRepository BoatsRepository => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
