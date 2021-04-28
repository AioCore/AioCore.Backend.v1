using System;
using RabbitMQ.Client;

namespace Package.EventBus.EventBus.RabbitMQ
{
    public interface IRabbitMqPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}