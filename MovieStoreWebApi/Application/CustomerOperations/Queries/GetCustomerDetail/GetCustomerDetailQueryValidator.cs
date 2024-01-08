using FluentValidation;

namespace MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomerDetail;

public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
{
    public GetCustomerDetailQueryValidator()
    {
        RuleFor(query => query.CustomerId).GreaterThan(0);
    }
}