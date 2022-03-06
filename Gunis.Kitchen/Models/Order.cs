using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage ="{0} can not be Empty")]
        [Display(Name ="Addressline 1")]
        public string AddressLine1 { get; set; }

        [Display(Name ="Addressline 2")]
        public string AddressLine2 { get; set; }

        [Display(Name ="Zip Code")]
        [MaxLength(6, ErrorMessage ="{0} can not contain more than {1} characters.")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public int ZipCode { get; set; }

        [Display(Name ="Country")]
        [MaxLength(20, ErrorMessage ="{0} can not contain more than {1} characters.")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public string Country { get; set; }

        [Display(Name ="State")]
        [MaxLength(20, ErrorMessage ="{0} can not contain more than {1} characters.")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public string State { get; set; }

        public string PaymentMethod { get; set; }
        public DateTime OrderPlaced { get; set; }
        public int OrderTotal { get; set; }

        #region
        public MyIdentityUser User { get; set; }
        public List<OrderDetail> OrderLines { get; set; }
        #endregion

    }
}
