using Boat.Data.DataModel.GeneralModule.Entity;
using Boat.Data.Dto.BoatModule.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.GeneralModule.Service.Interface
{
    public interface IFavoritesServices
    {
        List<Favorites> SelectByCustomerNumber(long customerNumber);
        List<ResponseBoats> SelectPopularBoats();
        Favorites SelectByBoatId(long boatId);

        long Insert(Favorites fav);
        Favorites Update(Favorites fav);
        bool Delete(Favorites fav);
        bool DeleteAllforCustomer(long customerNumber);
    }
}
