using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InGame.Business.Tools.Validations;

namespace InGame.Business.Concrete.DTO.Concrete.User
{
    public class UserRegisterDto 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
