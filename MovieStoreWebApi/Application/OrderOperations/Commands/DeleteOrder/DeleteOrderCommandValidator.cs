using FluentValidation;

namespace MovieStoreWebApi.Application.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(command => command.OrderId).GreaterThan(0);
    }
}