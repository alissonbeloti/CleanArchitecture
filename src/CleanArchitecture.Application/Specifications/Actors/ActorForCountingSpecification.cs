using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Specifications.Actors;

public class ActorForCountingSpecification: BaseSpecification<Actor>
{
    public ActorForCountingSpecification(ActorSpecificationParams actorParams)
        : base(x => string.IsNullOrEmpty(actorParams.Search) || x.Nome!.Contains(actorParams.Search))
    {
        
    }
}
