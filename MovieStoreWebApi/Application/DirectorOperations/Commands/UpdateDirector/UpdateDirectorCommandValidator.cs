using FluentValidation;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
    {
        RuleFor(command => command.DirectorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(command => command.Model.LastName).NotEmpty().MaximumLength(50);
    }
}
