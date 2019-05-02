using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required,DataType(DataType.PhoneNumber),Display(Name ="Contact Number")]
        public string ContactNo { get; set; }
        [Required]
        public string Address { get; set; }
        public string ImagePath { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string UserRole { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string ResetCode { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public HttpPostedFileBase File { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }

    }
}
