using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.ViewModel
{
    public class OpenInventoryViewModel
    {
        public long ID { get; set; }

        public long WarehouseID { get; set; }

        [StringLength(16)]
        public string WarehouseCode { get; set; }

        [StringLength(250)]
        public string WarehouseName { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }

        public long ProductID { get; set; }

        [StringLength(16)]
        public string ProductCode { get; set; }

        [StringLength(250)]
        public string ProductName { get; set; }

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
