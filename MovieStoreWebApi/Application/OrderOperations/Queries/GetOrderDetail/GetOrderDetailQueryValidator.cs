using FluentValidation;

namespace MovieStoreWebApi.Application.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQueryValidator : AbstractValidator<GetOrderDetailQuery>
{
    public GetOrderDetailQueryValidator()
    {
        RuleFor(c => c.OrderId).GreaterThan(0);
    }
}