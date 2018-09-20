using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.GeneralModule.Service.Interface
{
    public interface ISequenceNumberService
    {
        long SelectByNewId();
        long SelectByNewCustomerId();
    }
}
