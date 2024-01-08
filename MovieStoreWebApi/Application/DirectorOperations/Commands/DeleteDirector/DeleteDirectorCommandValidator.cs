using FluentValidation;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
{
    public DeleteDirectorCommandValidator()
    {
        RuleFor(command => command.DirectorId).GreaterThan(0);
    }
}