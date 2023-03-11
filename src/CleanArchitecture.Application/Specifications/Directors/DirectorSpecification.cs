using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Specifications.Directors;

public class DirectorSpecification: BaseSpecification<Director>
{
    public DirectorSpecification(DirectorSpecificationParams specificationParams): base(
        x => string.IsNullOrEmpty(specificationParams.Search) || x.Nome!.Contains(specificationParams.Search)
        )
    { 
        ApplyPaging(specificationParams.PageSize * (specificationParams.PageIndex - 1), specificationParams.PageSize);
        if (!string.IsNullOrWhiteSpace(specificationParams.Sort))
        {
            switch (specificationParams.Sort)
            {
                case "nomeAsc":
                    AddOrderBy(p => p.Nome!);
                    break;
                case "nomeDesc":
                    AddOrderByDescending(p => p.Nome!);
                    break;
                case "sobrenomeAsc":
                    AddOrderBy(p => p.Sobrenome!);
                    break;
                case "sobrenomeDesc":
                    AddOrderByDescending(p => p.Sobrenome!);
                    break;
                case "createDateAsc":
                    AddOrderBy(p => p.CreatedDate!);
                    break;
                case "createDateDesc":
                    AddOrderByDescending(p => p.CreatedDate!);
                    break;
                default:
                    AddOrderBy(p => p.Nome!);
                    break;
            }
        }
    }
}
