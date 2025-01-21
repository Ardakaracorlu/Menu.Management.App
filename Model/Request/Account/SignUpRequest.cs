using FluentValidation;
using Menu.Management.App.Pages.Account;

namespace Menu.Management.App.Model.Request.Account
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Password { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen kullanıcı adınızı giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifreinizi giriniz");
        }
    }
}
