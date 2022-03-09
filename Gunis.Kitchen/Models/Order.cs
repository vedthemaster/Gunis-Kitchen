using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    public class Order
    {

  

        [Display(Name = "User Id")]
        [ForeignKey(nameof(Order.User))]
        public Guid UserId { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} can not be Empty")]
        [Display(Name ="Addressline 1")]
        public string AddressLine1 { get; set; }

        [Display(Name ="Addressline 2")]
        public string AddressLine2 { get; set; }

        [Display(Name ="Zip Code")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public int ZipCode { get; set; }

        [Display(Name ="Country")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public string Country { get; set; }

        [Display(Name ="State")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public string State { get; set; }

        [Display(Name ="City")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public string City { get; set; }


        [Required(ErrorMessage ="{0} can not be empty")]
        public string PaymentMethod { get; set; }

        public DateTime OrderPlaced { get; set; }
        public int OrderTotal { get; set; }

        #region
        public MyIdentityUser User { get; set; }
        public  List<OrderDetail> OrderDetails { get; set; }
        #endregion

    }
}
