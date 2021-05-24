using System.Collections.Generic;
using AioCore.Application.Responses.SystemUserResponses;

namespace AioCore.Application.Responses.SystemTenantResponses
{
    public record CreateTenantResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<CreateUserResponse> Users { get; set; }
    }
}