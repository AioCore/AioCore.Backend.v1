﻿using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Services
{
    public interface ITenantService
    {
        Guid? GetCurrentTenant();

        Task<SystemTenant> CreateTenantAsync(SystemTenant systemTenant, CancellationToken cancellationToken);
    }
}
