using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testeBlazor.Models
{
    public class User
    {
        [BindProperty]
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 30 characters")]
        public string Password { get; set; }
        [BindProperty]
        [Required]
        [Compare(nameof(Password),ErrorMessage = "teste")]
        public string ConfirmPassword { get; set; }
    }
}
