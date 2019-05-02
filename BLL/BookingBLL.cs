using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.RespositoryPattern;

namespace BLL
{
   public class BookingBLL
    {
        IGenericRepository<Booking> _bookingDal = new GenericRepository<Booking>();
        IGenericRepository<ShippingAddress> _addressDal = new GenericRepository<ShippingAddress>();

        public int BookNow(Booking booking)
        {
          return  _bookingDal.Insert(booking);
        }
        public List<ShippingAddress> GetMyAdresses(long id)
        {
            return _addressDal.FindBy(x => x.AccountId == id);
        }
    }
}
