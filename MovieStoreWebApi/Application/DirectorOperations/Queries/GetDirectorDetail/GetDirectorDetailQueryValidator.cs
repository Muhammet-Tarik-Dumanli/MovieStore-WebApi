using FluentValidation;

namespace MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
{
    public GetDirectorDetailQueryValidator()
    {
        RuleFor(c => c.DirectorId).GreaterThan(0);
    }
}