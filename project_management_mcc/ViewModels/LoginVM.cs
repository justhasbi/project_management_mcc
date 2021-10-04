﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(40, ErrorMessage = "Email must be between 10 and 40 characters", MinimumLength = 10)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password must be between 5 and 50 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
