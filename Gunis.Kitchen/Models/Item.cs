using Gunis.Kitchen.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    [Table("Items")]
    public class Item
    {
        [Display(Name = "Item Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(20, ErrorMessage = "{0} can not have more than {1} characters")]
        public string ItemName { get; set; }

        [Display(Name = "Item Price")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [Range(minimum: 100, maximum: 2000, ErrorMessage = "{0} needs to between {1} and {2}")]
        public decimal ItemPrice { get; set; }

        [Required]
        [Display(Name ="Unit of Measure")]
        public string UnitOfMeasure { get; set; }

        //[Display(Name = "Item Size")]
        //public string ItemSize { get; set; }

        [Display(Name = "Size")]
        [Column(TypeName = "varchar(20)")]
        public ProductSizes ItemSize { get; set; }

        [Display(Name ="ImageName")]
        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name ="Item Image")]
        [Required(ErrorMessage ="It can not be blank")]
        public IFormFile ItemImage { get; set; }


        #region
        [Display(Name = "Category Name")]
        [Required]
        [ForeignKey(nameof(Item.Category))]
        public int CategoryID { get; set; }

        public Category Category { get; set; }


        #endregion

    }
}
