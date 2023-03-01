using Hful.Core.Application;
using Hful.Domain.Iam;
using Hful.Domain;
using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    public interface IUserService: ICurdService<User, UserDto, GetUserListDto, SaveUserDto, IRepository<User>>
    {
    }
}
