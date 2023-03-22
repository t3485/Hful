using Hful.Domain.Iam;
using Hful.Domain;
using Hful.Iam.Dto;
using Hful.Core.Mapper;

namespace Hful.Iam.Service
{
    internal class LoginService : ILoginService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IAsyncExecutor _asyncExecutor;
        private readonly IObjectMapper _objectMapper;

        public LoginService(IRepository<User> userRepository, IAsyncExecutor asyncExecutor, IObjectMapper objectMapper)
        {
            _userRepository = userRepository;
            _asyncExecutor = asyncExecutor;
            _objectMapper = objectMapper;
        }

        public async Task<LoginDto> LoginAsync(string username, string password)
        {
            var user = await _asyncExecutor.FirstOrDefaultAsync(_userRepository.AsQueryable().Where(x => x.UserName == username));
            if (user == null)
            {
                return new LoginDto { Status = false };
            }

            return new LoginDto
            {
                Status = user.Password == password,
                User = _objectMapper.Map<User, UserDto>(user)
            };
        }
    }
}
