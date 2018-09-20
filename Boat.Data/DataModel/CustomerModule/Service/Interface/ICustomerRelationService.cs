using Boat.Data.DataModel.CustomerModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.CustomerModule.Service.Interface
{
    public interface ICustomerRelationService
    {
        List<CustomerRelation> SelectByCustomerNumber(long customerNumber);
        long Insert(CustomerRelation cust);
        CustomerRelation Update(CustomerRelation cust);
        CustomerRelation Delete(CustomerRelation cust);

    }
}
