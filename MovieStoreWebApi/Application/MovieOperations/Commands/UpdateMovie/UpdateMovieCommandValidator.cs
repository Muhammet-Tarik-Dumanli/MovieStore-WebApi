using FluentValidation;

namespace MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(30);
        RuleFor(command => command.Model.Price).GreaterThan(0);
    }
}
