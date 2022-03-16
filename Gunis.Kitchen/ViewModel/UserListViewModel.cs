using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gunis.Kitchen.ViewModel
{
    public class UserListViewModel
    {
        [Key]
        [Display(Name ="User ID")]
        public Guid Id { get; set; }
        [Display(Name ="Username")]
        public string UserName { get; set; }
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Display(Name="Roles of USer")]
        public List<string> RolesOfUser { get; set; }

    }
}
