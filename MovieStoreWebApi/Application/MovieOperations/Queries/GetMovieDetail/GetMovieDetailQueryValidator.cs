using FluentValidation;

namespace MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
{
    public GetMovieDetailQueryValidator()
    {
        RuleFor(c => c.MovieId).GreaterThan(0);
    }
}