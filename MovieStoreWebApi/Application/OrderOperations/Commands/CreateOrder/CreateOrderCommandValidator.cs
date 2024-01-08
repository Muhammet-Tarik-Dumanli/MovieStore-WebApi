using FluentValidation;

namespace MovieStoreWebApi.Application.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.Model.CustomerId).GreaterThan(0);
		RuleFor(c => c.Model.MovieId).GreaterThan(0);
    }
}