using FluentValidation;

namespace MovieStoreWebApi.Application.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Model.CustomerId).GreaterThan(0);
		RuleFor(command => command.Model.MovieId).GreaterThan(0);
    }
}