using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using Package.AutoMapper;

namespace AioCore.Application.Responses.SecurityUserResponses
{
    public abstract class CreateUserResponse : IMapFrom<SecurityUser>
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}