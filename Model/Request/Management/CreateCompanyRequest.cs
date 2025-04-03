using FluentValidation;

namespace Menu.Management.App.Model.Request.Management
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string TaxIdentityNumber { get; set; }
        public string? Description { get; set; }
    }

    public class CreateCompanyValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lütfen Şirket ismi giriniz");

            RuleFor(x => x.Mail)
            .NotEmpty().WithMessage("Lütfen Şirket E-Mail giriniz");

            RuleFor(x => x.Mail)
            .EmailAddress().WithMessage("Lütfen geçerli bir E-Mail adresi giriniz");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Lütfen telefon giriniz");
           
            RuleFor(x => x.TaxIdentityNumber)
        .NotEmpty().WithMessage("Lütfen Vergi numarasınızı giriniz");

            RuleFor(x => x.Phone)
           .MaximumLength(10).WithMessage("en fazla 10 karakter girebilirsiniz");

            RuleFor(x => x.Adress)
           .NotEmpty().WithMessage("Lütfen adres giriniz");
           
            RuleFor(x => x.Adress)
           .MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");

            RuleFor(x => x.Description)
          .MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");
        }
    }
}
