using FluentValidation;

namespace MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQueryValidator : AbstractValidator<GetActorDetailQuery>
{
    public GetActorDetailQueryValidator()
    {
        RuleFor(c => c.ActorId).GreaterThan(0);
    }
}