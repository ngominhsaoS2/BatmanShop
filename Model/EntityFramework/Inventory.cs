using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EntityFramework
{
    [Table("Inventory")]
    public partial class Inventory
    {
        public long ID { get; set; }

        public long ActionID { get; set; }

        public DateTime? Date { get; set; }

        public long OrderID { get; set; }

        public long WarehouseID { get; set; }

        public long ProductID { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }


    }
}
