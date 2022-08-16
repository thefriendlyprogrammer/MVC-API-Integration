﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Assignment.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Department { get; set; }

    }
}