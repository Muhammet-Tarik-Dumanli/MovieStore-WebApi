using FluentValidation;

namespace MovieStoreWebApi.Application.CustomerOperations.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(command => command.CustomerId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(command => command.Model.LastName).NotEmpty().MaximumLength(50);
        RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(8);
        RuleFor(command => command.Model.Email).NotEmpty();
    }
}
