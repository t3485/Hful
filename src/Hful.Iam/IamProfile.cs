using AutoMapper;

using Hful.Domain.Iam;
using Hful.Iam.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam
{
    public class IamProfile : Profile
    {
        public IamProfile()
        {
            CreateMap<User, SaveUserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
