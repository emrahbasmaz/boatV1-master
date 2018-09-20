using Boat.Backoffice.DataModel.PaymentModule.Entity;
using Boat.Data.DataModel.PaymentModule.Service.Interface;
using Boat.Data.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Boat.Data.DataModel.PaymentModule.Service
{
    public class ReservationService : IReservationService
    {
        public List<Reservation> SelectByCustomerNumber(long customerNumber)
        {
            List<Reservation> _reserv = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Reservation> reservations = sqlConnection.Query<Reservation>("select * from RESERVATION where CUSTOMER_NUMBER = @id and RECORD_STATUS = 1", new { id = customerNumber });
                if (reservations.Count() == 0)
                    throw new Exception("PAYMENT_TRANSACTION_NOT_FOUND");
                else
                    _reserv = reservations.ToList();
            }

            return _reserv;
        }

        public Reservation SelectByPaymentId(long paymentId)
        {
            Reservation _cardMaster = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Reservation> card_master = sqlConnection.Query<Reservation>("select * from RESERVATION where PAYMENT_ID = @id and RECORD_STATUS = 1", new { id = paymentId });
                if (card_master.Count() == 0)
                    throw new Exception("PAYMENT_TRANSACTION_NOT_FOUND");
                else
                    _cardMaster = card_master.FirstOrDefault();
            }

            return _cardMaster;
        }

        public Reservation SelectByReservationId(long reservationId)
        {
            Reservation _cardMaster = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Reservation> card_master = sqlConnection.Query<Reservation>("select * from RESERVATION where RESERVATION_ID = @id and RECORD_STATUS = 1", new { id = reservationId });
                if (card_master.Count() == 0)
                    throw new Exception("PAYMENT_TRANSACTION_NOT_FOUND");
                else
                    _cardMaster = card_master.FirstOrDefault();
            }

            return _cardMaster;
        }

        public List<Reservation> SelectByBoatId(long boatId)
        {
            List<Reservation> _cardMaster = null;
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();
                IEnumerable<Reservation> card_master = sqlConnection.Query<Reservation>("select * from RESERVATION where BOAT_ID = @id and RECORD_STATUS = 1", new { id = boatId });
                if (card_master.Count() == 0)
                    throw new Exception("PAYMENT_TRANSACTION_NOT_FOUND");
                else
                    _cardMaster = card_master.ToList();
            }

            return _cardMaster;
        }

        public bool Update(Reservation request)
        {
            try
            {
                Reservation _reservation = null;
                using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                {
                    sqlConnection.Open();
                    _reservation = sqlConnection.Get<Reservation>(request.RESERVATION_ID);
                    _reservation.RECORD_STATUS = 1;
                    _reservation.INSERT_DATE = request.INSERT_DATE;
                    _reservation.INSERT_USER = request.INSERT_USER;
                    _reservation.UPDATE_DATE = DateTime.Now;
                    _reservation.UPDATE_USER = request.UPDATE_USER;
                    _reservation.RESERVATION_ID = request.RESERVATION_ID;
                    _reservation.PAYMENT_ID = request.PAYMENT_ID;
                    _reservation.CUSTOMER_NUMBER = request.CUSTOMER_NUMBER;
                    _reservation.PRICE = request.PRICE;
                    _reservation.BOAT_ID = request.BOAT_ID;
                    _reservation.TOUR_TYPE = request.TOUR_TYPE;
                    _reservation.RESERVATION_DATE = request.RESERVATION_DATE;
                    _reservation.RESERVATION_END_DATE = request.RESERVATION_END_DATE;
                    _reservation.CONFIRM = request.CONFIRM;
                    if (!String.IsNullOrEmpty(request.CAPACITY))
                        _reservation.CAPACITY = request.CAPACITY;
                    _reservation.PAYMENT_TYPE = Enum.GetName(typeof(PaymentType), Convert.ToInt16(request.PAYMENT_TYPE));

                    sqlConnection.Update<Reservation>(_reservation);

                    var result = sqlConnection.Get<Reservation>(_reservation.RESERVATION_ID);

                }

                return true;
            }
            catch (Exception ex)
            {
                //log.Error("Update Reservation has an ERROR: [ERROR : " + ex.Message + "]");
                return false;
            }

        }

        public long Insert(Reservation card)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
            {
                sqlConnection.Open();

                var _reservation = new Reservation()
                {
                    GUID = Guid.NewGuid().ToString(),
                    RECORD_STATUS = 1,
                    INSERT_DATE = DateTime.Now,
                    INSERT_USER = card.INSERT_USER,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_USER = card.UPDATE_USER,
                    CUSTOMER_NUMBER = card.CUSTOMER_NUMBER,
                    PAYMENT_ID = card.PAYMENT_ID,
                    PRICE = card.PRICE,
                    BOAT_ID = card.BOAT_ID,
                    TOUR_TYPE = card.TOUR_TYPE,
                    RESERVATION_DATE = card.RESERVATION_DATE,
                    RESERVATION_END_DATE = card.RESERVATION_END_DATE,
                    CAPACITY = card.CAPACITY,
                    CONFIRM = card.CONFIRM,
                    PAYMENT_TYPE = Enum.GetName(typeof(PaymentType), Convert.ToInt16(card.PAYMENT_TYPE))
                };

                var customerGuid = sqlConnection.Insert<Reservation>(_reservation);

                sqlConnection.Close();

                return customerGuid;
            }
        }

        public bool Delete(Reservation request)
        {
            try
            {
                Reservation _reservation = null;
                using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                {
                    sqlConnection.Open();
                    _reservation = sqlConnection.Get<Reservation>(request.RESERVATION_ID);
                    _reservation.RECORD_STATUS = 0;
                    _reservation.UPDATE_DATE = DateTime.Now;
                    _reservation.UPDATE_USER = request.UPDATE_USER;

                    sqlConnection.Update<Reservation>(_reservation);

                    var result = sqlConnection.Get<Reservation>(_reservation.RESERVATION_ID);
                }

                return true;
            }
            catch (Exception ex)
            {
                // log.Error("Delete Reservation has an ERROR: [ERROR : " + ex.Message + "]");
                return false;
            }

        }


    }
}
