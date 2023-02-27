using Hful.Domain.Iam;
using Hful.Domain;
using Hful.Iam.Api.Dto.Users;
using Hful.Core.Mapper;
using Hful.Application.Contracts;

namespace Hful.Iam.Service
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAsyncExecutor _asyncExecutor;

        public UserService(IObjectMapper objectMapper, IRepository<User> userRepository, IAsyncExecutor asyncExecutor)
        {
            _objectMapper = objectMapper;
            _userRepository = userRepository;
            _asyncExecutor = asyncExecutor;
        }

        public async Task<PageDto<UserDto>> GetListAsync(GetUserListDto dto)
        {
            var data = await _asyncExecutor.ToListAsync(_userRepository.AsQueryable());
            return new PageDto<UserDto>(_objectMapper.MapList<User, UserDto>(data));
        }

        public async Task SaveUserAsync(SaveUserDto dto)
        {
            var entity = await _userRepository.FindById(dto.Id);
            if (entity != null)
            {
                _objectMapper.Map(dto, entity);
                await _userRepository.SaveAsync(entity);
            }
            else
            {
                entity = _objectMapper.Map<SaveUserDto, User>(dto);
                await _userRepository.SaveAsync(entity);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
