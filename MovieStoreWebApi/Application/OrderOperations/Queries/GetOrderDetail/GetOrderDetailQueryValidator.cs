using FluentValidation;

namespace MovieStoreWebApi.Application.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQueryValidator : AbstractValidator<GetOrderDetailQuery>
{
    public GetOrderDetailQueryValidator()
    {
        RuleFor(query => query.OrderId).GreaterThan(0);
    }
}