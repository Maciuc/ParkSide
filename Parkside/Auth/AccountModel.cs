﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parkside.Backend.Auth
{
    public class AccountModel
    {
        public class RegisterModel
        {
            [EmailAddress]
            [Required(ErrorMessage = "Email is required")]
            [Display(Name = "Email")]
            public string? Email { get; set; }
            public List<string>? Roles { get; set; }
        }
        public class LoginModel
        {
            [Required(ErrorMessage = "User Name is required")]
            public string? Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }
        }

        public class ResetPasswordModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string? ConfirmPassword { get; set; }

            public string? Code { get; set; }
        }

        public class ForgotPasswordModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string? Email { get; set; }
        }
    }
}
