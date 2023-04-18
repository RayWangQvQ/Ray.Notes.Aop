using FluentValidation;
using ScrutorCastleDynamicProxyScanFullSample.AppServices.Account;

namespace ScrutorCastleDynamicProxyScanFullSample.AppServices.Book
{
    public class BookDto
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }

    public class CustomerValidator : AbstractValidator<BookDto>
    {
        public CustomerValidator()
        {
            RuleFor(it => it.Title).NotEmpty();
            RuleFor(it => it.Author).NotEmpty();
        }
    }
}
