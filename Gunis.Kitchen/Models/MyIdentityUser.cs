using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Gunis.Kitchen.Models
{
    public class MyIdentityUser : IdentityUser<Guid>

    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(30, ErrorMessage = "{0} can not have more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} should have at least {1} characters.")]
        public string Name { get; set; }

        [Display(Name ="Admin Activation Key")]
        public string AdminKey { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        [PersonalData]
        [Column(TypeName = "smalldatetime")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Can not be Empty")]
        public string Gender { get; set; }

        public Order Order { get; set; }



    }
}
