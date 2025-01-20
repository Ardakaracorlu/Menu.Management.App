using FluentValidation;

namespace Menu.Management.App.Model.Request.Account
{
    public class LoginRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Lütfen kullanıcı adınızı giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifreinizi giriniz");
        }
    }
}
