using Hful.Application.Contracts;
using Hful.Iam.Api.Dto.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Service
{
    public interface IUserService
    {
        Task<PageDto<UserDto>> GetListAsync(GetUserListDto dto);

        Task SaveUserAsync(SaveUserDto dto);

        Task DeleteAsync(Guid id);
    }
}
