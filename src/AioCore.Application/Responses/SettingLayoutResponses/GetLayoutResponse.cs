using System;

namespace AioCore.Application.Responses.SettingLayoutResponses
{
    public record GetLayoutResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public GetLayoutResponse() { }

        public GetLayoutResponse(
            Guid id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}