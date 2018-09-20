using Boat.Backoffice.DataModel.PaymentModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.PaymentModule.Service.Interface
{
    public interface IPaymentTransactionService
    {
        List<PaymentTransaction> SelectByCustomerNumber(long customerNumber);
        PaymentTransaction SelectByCardRefNumber(long cardRefNumber);
        PaymentTransaction SelectByPaymentId(long paymentId);
        List<PaymentTransaction> SelectByBoatId(long boatId);
        PaymentTransaction Update(PaymentTransaction card);
        long Insert(PaymentTransaction card);
    }
}
