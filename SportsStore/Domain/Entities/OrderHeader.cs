using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int CustomerId { get; set; }

        public string ShipAddress { get; set; }

        public string ShipmentMethod { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalDue { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
