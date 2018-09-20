using Boat.Backoffice.DataModel.PaymentModule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boat.Data.DataModel.PaymentModule.Service.Interface
{
    public interface IReservationService
    {
        List<Reservation> SelectByCustomerNumber(long customerNumber);
        Reservation SelectByPaymentId(long paymentId);
        Reservation SelectByReservationId(long reservationId);
        List<Reservation> SelectByBoatId(long boatId);
        bool Update(Reservation request);
        long Insert(Reservation card);
        bool Delete(Reservation request);
    }
}
