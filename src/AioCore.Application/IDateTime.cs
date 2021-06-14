using System;

namespace AioCore.Application
{
    public interface IDateTime
    {
        DateTimeOffset Now { get; }
    }

    public class CustomDateTime : IDateTime
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
