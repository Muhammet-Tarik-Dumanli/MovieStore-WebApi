using FluentValidation;

namespace MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(c => c.Model.Name).NotEmpty();
        RuleFor(c => c.Model.DirectorId).GreaterThan(0);
        RuleFor(c => c.Model.GenreId).GreaterThan(0);
        RuleFor(c => c.Model.Price).GreaterThan(0);
    }
}