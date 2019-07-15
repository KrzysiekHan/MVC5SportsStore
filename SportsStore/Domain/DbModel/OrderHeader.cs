namespace Domain.DbModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderHeader")]
    public partial class OrderHeader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int CustomerId { get; set; }

        public string ShipAddress { get; set; }

        [StringLength(50)]
        public string ShipmentMethod { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Comment { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalDue { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}
