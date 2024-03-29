﻿using AioCore.Domain.CoreEntities;
using Package.AutoMapper;

namespace AioCore.Application.Responses.SystemUserResponses
{
    public abstract class CreateUserResponse : IMapFrom<SystemUser>
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}