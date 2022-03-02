using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    public class MyIdentityRole
          : IdentityRole<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Display(Name = "RoleName")]
        [StringLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string RoleName { get; set; }

    }
}
