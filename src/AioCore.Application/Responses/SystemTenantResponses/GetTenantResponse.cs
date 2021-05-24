using System;

namespace AioCore.Application.Responses.SystemTenantResponses
{
    public record GetTenantResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public GetTenantResponse(
            Guid id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}