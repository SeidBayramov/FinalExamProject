using FluentValidation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace FinalExamProject.ViewModel
{
    public class RegisterVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
    public class RegisterVmValidation : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required");
            RuleFor(x=>x.Password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("ConfirmPassword is required").Equal(x => x.Password).WithMessage("Password must match");
        }
    }
}
