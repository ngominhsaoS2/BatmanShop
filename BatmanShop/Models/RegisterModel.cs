using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BatmanShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        public string UserName { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be more than 6 characters")]
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirmed password is not correct")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please use a real email")]
        public string Email { get; set; }

        public string Phone { get; set; }

    }
}