using Boat.Data.DataModel.CustomerModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service.Interface
{
    public interface ICustomerService
    {
        Customer SelectByGuid(long guid);
        Customer SelectByCustomerNumber(long customerNumber);
        Customer SelectByIdentificationId(long identificationId);
        Customer Update(Customer cust);
        long Insert(Customer cust);
        Customer Delete(Customer cust);
    }
}
