using System;
using Microsoft.Azure.ServiceBus;

namespace Package.EventBus.EventBus.ServiceBus
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ITopicClient TopicClient { get; }
        ISubscriptionClient SubscriptionClient { get; }
    }
}