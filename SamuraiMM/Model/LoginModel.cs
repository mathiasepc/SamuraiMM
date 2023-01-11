using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class LoginModel
    {
        //hvis feltet er tomt
        //email validering
        [Required, EmailAddress(ErrorMessage = "Email is wrong example mail: test@test.test")]
        public string Email { get; set; }
        //hvis feltet er tomt
        //skal være på mindst 6 karakterer
        //kan være max 15 karakterer
        [Required, MinLength(6, ErrorMessage = "Password must be between 6-15 characters."), StringLength(15, ErrorMessage = "Password must be between 6-15 characters.")]
        public string Password { get; set; }
    }
}
