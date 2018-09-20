using Boat.Backoffice.DataModel.PaymentModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.PaymentModule.Service.Interface
{
    public interface ICardMasterService
    {
        CardMaster SelectByCustomerNumber(long customerNumber);
        CardMaster SelectByCardRefNumber(long cardRefNumber);
        CardMaster Update(CardMaster card);
        long Insert(CardMaster card);
        CardMaster Delete(CardMaster card);
    }
}
