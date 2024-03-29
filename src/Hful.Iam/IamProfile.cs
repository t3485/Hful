﻿using AutoMapper;

using Hful.Domain.Iam;
using Hful.Iam.Domain;
using Hful.Iam.Dto;

namespace Hful.Iam
{
    public class IamProfile : Profile
    {
        public IamProfile()
        {
            CreateMap<User, SaveUserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<Menu, MenuDto>();
        }
    }
}
