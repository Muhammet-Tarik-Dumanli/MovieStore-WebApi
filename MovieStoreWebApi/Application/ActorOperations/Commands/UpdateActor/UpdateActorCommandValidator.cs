using FluentValidation;

namespace MovieStoreWebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(command => command.ActorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(command => command.Model.LastName).NotEmpty().MaximumLength(50);
    }
}
