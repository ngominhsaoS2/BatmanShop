using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EntityFramework
{
    [Table("Action")]
    public partial class Action
    {
        public long ID { get; set; }

        [StringLength(16)]
        [Required(ErrorMessage = "Please enter Code")]
        public string Code { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

    }
}
