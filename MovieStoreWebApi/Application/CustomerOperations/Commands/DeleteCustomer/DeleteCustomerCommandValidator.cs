using FluentValidation;

namespace MovieStoreWebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(command => command.CustomerId).GreaterThan(0);
    }
}