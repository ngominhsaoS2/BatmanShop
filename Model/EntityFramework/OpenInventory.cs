using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EntityFramework
{
    [Table("OpenInventory")]
    public partial class OpenInventory
    {
        public long ID { get; set; }

        public long WarehouseID { get; set; }

        public int? Year { get; set; }

        [StringLength(3)]
        public string Month { get; set; }

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
