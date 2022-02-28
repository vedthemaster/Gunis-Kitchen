using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(15, ErrorMessage = "{0} can not have more than {1} characters")]
        public string CategoryName { get; set; }

        #region
        public ICollection<Item> Items { get; set; }

        #endregion

    }
}
