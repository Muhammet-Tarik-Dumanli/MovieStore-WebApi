using FluentValidation;

namespace MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomerDetail;

public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
{
    public GetCustomerDetailQueryValidator()
    {
        RuleFor(c => c.CustomerId).GreaterThan(0);
    }
}