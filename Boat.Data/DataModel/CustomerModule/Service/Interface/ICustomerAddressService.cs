using Boat.Data.DataModel.CustomerModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service.Interface
{
    public interface ICustomerAddressService
    {
        CustomerAddress SelectByCustomerNumber(long customerNumber);
        bool Update(CustomerAddress cust);
        long Insert(CustomerAddress cust);
        bool Delete(CustomerAddress cust);
    }
}
