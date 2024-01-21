using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace FinalExamProject.ViewModel
{
    public class LoginVm
    {
        public string UsernameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class LoginVmValidation : AbstractValidator<LoginVm>
    {
        public LoginVmValidation()
        {
            RuleFor(x => x.UsernameorEmail).NotEmpty().WithMessage("Username/Email or password is wrong");
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
