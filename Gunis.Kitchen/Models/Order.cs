using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    public class Order
    {
        [Display(Name ="User Id")]
        [ForeignKey(nameof(Order.User))]
        public Guid UserId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage ="{0} can not be Empty")]
        [MaxLength(25,ErrorMessage ="{0} can not contain more than {1} characters")]
        [MinLength(5,ErrorMessage ="{0} can not contain more than {1} characters")]        
        [Display(Name ="Addressline 1")]
        public string AddressLine1 { get; set; }

        [MaxLength(25,ErrorMessage ="{0} can not contain more than {1} characters")]
        [MinLength(5,ErrorMessage ="{0} can not contain more than {1} characters")]
        [Display(Name ="Addressline 2")]
        public string AddressLine2 { get; set; }

        [Display(Name ="Zip Code")]
        [MaxLength(8, ErrorMessage ="{0} can not contain more than {1} characters.")]
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

        [Display(Name ="City")]
        [MaxLength(20, ErrorMessage ="{0} can not contain more than {1} characters.")]
        [Required(ErrorMessage ="{0} can not be Empty")]
        public string City { get; set; }


        [Required(ErrorMessage ="{0} can not be empty")]
        public string PaymentMethod { get; set; }

        public DateTime OrderPlaced { get; set; }
        public int OrderTotal { get; set; }

        #region
        public MyIdentityUser User { get; set; }
        public List<OrderDetail> OrderLines { get; set; }
        #endregion

    }
}
