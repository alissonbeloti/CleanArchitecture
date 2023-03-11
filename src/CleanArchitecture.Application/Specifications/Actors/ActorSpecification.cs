using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Specifications.Actors;

public class ActorSpecification: BaseSpecification<Actor>
{
    public ActorSpecification(ActorSpecificationParams actorParams)
        : base(
              x => 
                string.IsNullOrEmpty(actorParams.Search) || x.Nome!.Contains(actorParams.Search)
              )
    {
        ApplyPaging(actorParams.PageSize * (actorParams.PageIndex - 1), actorParams.PageSize);

        if (!string.IsNullOrEmpty(actorParams.Sort))
        {
            switch (actorParams.Sort)
            {
                case "nomeAsc":
                    AddOrderBy(x => x.Nome!);
                    break;
                case "nomeDesc":
                    AddOrderByDescending(x => x.Nome!);
                    break;
                default:
                    AddOrderBy(x => x.CreatedDate!); 
                    break;
            }
        }
    }
}
