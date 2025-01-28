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
        public string ConfirmPassword { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lütfen isim giriniz");
            
            RuleFor(x => x.Surname)
               .NotEmpty().WithMessage("Lütfen soyisim giriniz");
            
            RuleFor(x => x.Mail)
            .NotEmpty().WithMessage("Lütfen mail giriniz");

            RuleFor(x => x.Mail)
            .EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Lütfen telefon giriniz");

            RuleFor(x => x.Phone)
           .MaximumLength(10).WithMessage("en fazla 10 karakter girebilirsiniz");

            RuleFor(x => x.Adress)
           .NotEmpty().WithMessage("Lütfen adres giriniz");
            RuleFor(x => x.Adress)
           .MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifreinizi giriniz");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifreniz ile Doğrulama şifreniz birbiriyle aynı değildir");
        }
    }
}
