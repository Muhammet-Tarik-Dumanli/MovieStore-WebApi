using FluentValidation;

namespace MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(command => command.Model.LastName).NotEmpty().MaximumLength(50);
        RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(8);
        RuleFor(command => command.Model.Email).NotEmpty();
    }
}