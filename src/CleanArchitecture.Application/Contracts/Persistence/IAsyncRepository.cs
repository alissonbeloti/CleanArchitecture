using CleanArchitecture.Application.Specifications;
using CleanArchitecture.Domain.Common;

using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : BaseDomainModel, new()
{
    #region Gets
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeString = null,
        bool disabledTracking = true
        );

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<Expression<Func<T, object>>>? includes = null,
        bool disabledTracking = true
        );

    Task<T> GetByIdAsync(int id);
    #endregion

    #region Manipulate
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DelteAsync(T entity);

    void AddEntity(T entity);
    void UpdateEntity(T entity);
    void DeleteEntity(T entity);

    Task<T> GetByIdWithSpec(ISpecification<T> spec);

    Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);
    #endregion
}
