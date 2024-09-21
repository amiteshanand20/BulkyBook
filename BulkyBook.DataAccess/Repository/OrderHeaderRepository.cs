using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string OrderStatus, string? PaymentStatus = null)
        {
            var OrderFromDB = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);

            if (OrderFromDB != null)
            {
                OrderFromDB.OrderStatus = OrderStatus;

                if (!string.IsNullOrEmpty(PaymentStatus))
                {
                    OrderFromDB.PaymentStatus = PaymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string SessionId, string PaymentIntentId)
        {
            var OrderFromDB = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrWhiteSpace(SessionId))
            {
                OrderFromDB.SessionId = SessionId;
            }
            if (!string.IsNullOrWhiteSpace(PaymentIntentId))
            {
                OrderFromDB.PaymentIntentId = PaymentIntentId;
                OrderFromDB.PaymentDate = DateTime.Now;
            }
        }
    }
}
