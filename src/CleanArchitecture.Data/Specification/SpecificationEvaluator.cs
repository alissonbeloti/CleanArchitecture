﻿using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Application.Specifications;

namespace CleanArchitecture.Infrastructure.Specification;

public class SpecificationEvaluator<T> where T: BaseDomainModel
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec) 
    { 
        if (spec.Criteria != null)
        {
            inputQuery = inputQuery.Where(spec.Criteria);
        }

        if (spec.OrderBy!= null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnable)
        {
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
        }

        inputQuery = spec.Includes.Aggregate(inputQuery, (current, includes) => current.Include(includes));

        return inputQuery; 
    }
    
}
