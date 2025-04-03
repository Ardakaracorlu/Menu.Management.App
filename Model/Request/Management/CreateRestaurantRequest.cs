using FluentValidation;

namespace Menu.Management.App.Model.Request.Management
{
    public class CreateRestaurantRequest
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int NumberOfTable { get; set; }
        public string? Description { get; set; }
    }

    public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantRequest>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.CompanyId)
               .NotEmpty().WithMessage("Lütfen Şirket seçiniz");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lütfen Restoran ismi giriniz");

            RuleFor(x => x.Mail)
            .NotEmpty().WithMessage("Lütfen Şirket E-Mail giriniz");

            RuleFor(x => x.Mail)
            .EmailAddress().WithMessage("Lütfen geçerli bir E-Mail adresi giriniz");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Lütfen telefon giriniz");

            RuleFor(x => x.Phone)
           .MaximumLength(10).WithMessage("en fazla 10 karakter girebilirsiniz");

            RuleFor(x => x.NumberOfTable)
           .NotEmpty().WithMessage("Lütfen Masa Sayısını giriniz");

            RuleFor(x => x.Adress)
.NotEmpty().WithMessage("Lütfen adres giriniz");

            RuleFor(x => x.Adress)
           .MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");


            RuleFor(x => x.Description)
          .MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");
        }
    }
}
