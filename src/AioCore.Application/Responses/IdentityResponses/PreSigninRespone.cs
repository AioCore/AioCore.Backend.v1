using System;
using System.Collections.Generic;

namespace AioCore.Application.Responses.IdentityResponses
{
    public class PreSigninRespone
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }
        public string Password { get; set; }
        public IReadOnlyCollection<TenantInfoRespone> Tenants { get; set; }

    }

    public class TenantInfoRespone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
