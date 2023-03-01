using Hful.Core.Mapper;
using Hful.Domain;
using Hful.Domain.Shared;

namespace Hful.Core.Application
{
    public abstract class CurdService<TEntity, TDto, TGetDto, TSaveDto, TRepository> : ICurdService<TEntity, TDto, TGetDto, TSaveDto, TRepository>
        where TEntity : BaseEntity
        where TRepository : IRepository<TEntity>
        where TSaveDto : IEntityDto
    {
        protected readonly TRepository _userRepository;
        protected readonly IObjectMapper _objectMapper;
        protected readonly IAsyncExecutor _asyncExecutor;

        public CurdService(IObjectMapper objectMapper, TRepository userRepository, IAsyncExecutor asyncExecutor)
        {
            _objectMapper = objectMapper;
            _userRepository = userRepository;
            _asyncExecutor = asyncExecutor;
        }

        public async Task<PageDto<TDto>> GetListAsync(TGetDto dto)
        {
            var data = await _asyncExecutor.ToListAsync(_userRepository.AsQueryable());
            return new PageDto<TDto>(_objectMapper.MapList<TEntity, TDto>(data));
        }

        public async Task SaveUserAsync(TSaveDto dto)
        {
            var entity = await _userRepository.FindById(dto.Id);
            if (entity != null)
            {
                _objectMapper.Map(dto, entity);
                await _userRepository.SaveAsync(entity);
            }
            else
            {
                entity = _objectMapper.Map<TSaveDto, TEntity>(dto);
                await _userRepository.SaveAsync(entity);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
