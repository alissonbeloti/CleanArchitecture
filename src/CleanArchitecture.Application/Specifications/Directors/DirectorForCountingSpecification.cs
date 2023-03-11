using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Specifications.Directors;

public class DirectorForCountingSpecification: BaseSpecification<Director>
{
	public DirectorForCountingSpecification(DirectorSpecificationParams directorParams): 
		base (
			x => string.IsNullOrEmpty(directorParams.Search) || x.Nome!.Contains(directorParams.Search)
		)
	{ }
}
