using System;

namespace AioCore.Shared.Abstracts
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