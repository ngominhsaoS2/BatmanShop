using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Validation
{
    public class UserModel
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "Please enter UserName")]
        public string UserName { get; set; }

        
        public string Password { get; set; }

        
        public string GroupID { get; set; }

        
        public string Name { get; set; }

        
        public string Address { get; set; }

        
        public string Email { get; set; }

        
        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }
    }
}
