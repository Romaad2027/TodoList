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
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please, use letters in the first name. Digits are not allowed.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please, use letters in the first name. Digits are not allowed.")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Type the password")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 8, ErrorMessage = "Password must have from 8 to 20 characters")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$", ErrorMessage = "A password must contain at least eight characters, including at least one number and includes both lower and uppercase letters and special characters, for example #, ?, !.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong password")]
        public string ConfirmPassword { get; set; }
    }
}
