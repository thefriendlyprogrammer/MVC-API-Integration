using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Assignment.Models
{
    public class AdminUserModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int V_Code { get; set; }

    }
}