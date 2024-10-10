using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Models
{
    public class Penalty
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("Return")]
        public int ReturnId { get; set; }

        public Return Return { get; set; }
    }
}