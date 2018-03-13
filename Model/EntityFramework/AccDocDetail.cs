using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EntityFramework
{
    [Table("AccDocDetail")]
    public partial class AccDocDetail
    {
        public long ID { get; set; }

        [StringLength(2)]
        public string DocCode { get; set; }

        [StringLength(16)]
        public string DocNo { get; set; }

        public long WarehouseID { get; set; }

        public long ProductID { get; set; }

        public decimal? Price { get; set; }

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
