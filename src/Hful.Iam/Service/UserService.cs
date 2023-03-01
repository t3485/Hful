using Hful.Domain.Iam;
using Hful.Domain;
using Hful.Core.Mapper;
using Hful.Core.Application;
using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    internal class UserService : CurdService<User, UserDto, GetUserListDto, SaveUserDto, IRepository<User>>, IUserService
    {
        public UserService(IObjectMapper objectMapper, IRepository<User> userRepository, IAsyncExecutor asyncExecutor) : base(objectMapper, userRepository, asyncExecutor)
        {
        }
    }
}
