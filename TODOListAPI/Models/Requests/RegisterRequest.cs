using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODOListAPI.Models.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Type your name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must have at least 2 characters, max = 20")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Type the password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must have from 8 to 20 characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong password")]
        public string ConfirmPassword { get; set; }
    }
}
