using System.Threading.Tasks;
using AioCore.Domain.Common;

namespace AioCore.Application.Services
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}