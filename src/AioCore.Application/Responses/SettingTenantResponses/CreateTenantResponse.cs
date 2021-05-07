using AioCore.Application.Responses.SecurityUserResponses;
using System.Collections.Generic;

namespace AioCore.Application.Responses.SettingTenantResponses
{
    public class CreateTenantResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<CreateUserResponse> Users { get; set; }
    }
}