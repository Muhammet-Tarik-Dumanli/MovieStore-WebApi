using FluentValidation;

namespace MovieStoreWebApi.Application.ActorOperations.Commands;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(c => c.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(c => c.Model.LastName).NotEmpty().MaximumLength(50);
    }
}