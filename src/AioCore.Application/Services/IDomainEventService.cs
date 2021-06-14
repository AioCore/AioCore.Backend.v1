using AioCore.Domain.Common;
using System.Threading.Tasks;

namespace AioCore.Application.Services
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
