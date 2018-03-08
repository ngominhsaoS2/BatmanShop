using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EntityFramework
{
    [Table("StatusCategory")]
    public partial class StatusCategory
    {
        public int ID { get; set; }

        [StringLength(16)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int Type { get; set; }

        [StringLength(50)]
        public string ForTable { get; set; }

        public bool Status { get; set; }

    }
}
