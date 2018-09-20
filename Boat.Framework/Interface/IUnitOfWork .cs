using System;

namespace Boat.Framework.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<BoatPhotos,long> BoatPhotosRepository { get; }
        IRepository BoatsRepository { get; }
    }
}
