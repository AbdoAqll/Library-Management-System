using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public DateTime? DueDate { get; set; }

        public string Status { get; set; }


    }
}
