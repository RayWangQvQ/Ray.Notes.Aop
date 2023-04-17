using FluentValidation;

namespace ScrutorCastleDynamicProxyScanFullSample.AppServices.Account
{
    public class UserAccountDto
    {
        public string Name { get; set; }

        public string Pwd { get; set; }
    }

    public class CustomerValidator : AbstractValidator<UserAccountDto>
    {
        public CustomerValidator()
        {
            RuleFor(it => it.Name).NotEmpty();
            RuleFor(it => it.Pwd).NotEmpty();

            RuleFor(it => it.Name).Must(s =>
            {
                return false;
            });
        }
    }
}
