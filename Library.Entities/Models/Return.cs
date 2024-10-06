using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Models
{
    public class Return
    {
        public int Id { get; set; }

        [ForeignKey("CheckOut")]
        [Required]
        public int CheckoutId { get; set; }

        public Checkout CheckOut { get; set; }

        public DateTime ReturnDate { get; set; }
        public bool HasPenalty { get; set; }
    }
}