using Boat.Data.DataModel.GeneralModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.GeneralModule.Service.Interface
{
    public interface IComplaintsService
    {
        Complaints SelectByCustomerNumber(long customerNumber);
        Complaints Update(Complaints cust);
        long Insert(Complaints cust);
        Complaints Delete(Complaints cust);
    }
}
