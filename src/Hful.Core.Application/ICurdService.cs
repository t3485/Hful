using Hful.Domain.Shared;
using Hful.Domain;

namespace Hful.Core.Application
{
    public interface ICurdService<TEntity, TDto, TGetDto, TSaveDto, TRepository>
        where TEntity : BaseEntity
        where TRepository : IRepository<TEntity>
        where TSaveDto : IEntityDto
    {
        Task<PageDto<TDto>> GetListAsync(TGetDto dto);

        Task SaveUserAsync(TSaveDto dto);

        Task DeleteAsync(Guid id);
    }
}
