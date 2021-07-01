using System.Threading.Tasks;
using AioCore.Domain.Common;

namespace AioCore.Infrastructure.Services.Abstracts
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}