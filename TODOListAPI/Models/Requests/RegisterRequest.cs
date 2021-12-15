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
        public string Username { get; set; }

        [Required(ErrorMessage = "Type the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong password")]
        public string ConfirmPassword { get; set; }
    }
}
