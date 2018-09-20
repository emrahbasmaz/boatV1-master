using Boat.Data.DataModel.CustomerModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service.Interface
{
    public interface ITokenTransactionService
    {
        TokenTransaction SelectByCustomerNumber(long customerNumber);
        long Insert(TokenTransaction cust);
        bool Delete(TokenTransaction cust);

    }
}
