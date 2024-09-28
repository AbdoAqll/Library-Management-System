using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
        [Required]
        public int Stock { get; set; }
        [Display(Name = "Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
