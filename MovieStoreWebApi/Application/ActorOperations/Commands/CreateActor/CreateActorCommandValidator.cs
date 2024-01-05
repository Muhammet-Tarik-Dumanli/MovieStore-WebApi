using FluentValidation;

namespace MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(command => command.Model.LastName).NotEmpty().MaximumLength(50);
    }
}