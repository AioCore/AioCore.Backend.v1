﻿using Package.Mediator;
using AioCore.Shared.Exceptions;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.UnitOfWorks;
using AioCore.Application.Services;

namespace AioCore.Application.Commands.DynamicEntityCommand
{
    public class DeleteEntityCommand : IRequest<DeleteEntityRespone>
    {
        public Guid Id { get; set; }

        internal class Handler : IRequestHandler<DeleteEntityCommand, DeleteEntityRespone>
        {
            private readonly ITenantService _tenantService;
            private readonly IAioDynamicUnitOfWork _dynamicUnitOfWork;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                  ITenantService tenantService
                , IAioDynamicUnitOfWork dynamicUnitOfWork
                , IElasticsearchService elasticsearchService)
            {
                _tenantService = tenantService;
                _dynamicUnitOfWork = dynamicUnitOfWork;
                _elasticsearchService = elasticsearchService;
            }

            public async Task<Response<DeleteEntityRespone>> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
            {
                var currentTenantId = _tenantService.GetCurrentTenantId();
                var dynamicEntity = await _dynamicUnitOfWork
                    .DynamicEntities.FindAsync(new object[] { request.Id }, cancellationToken);
                if (dynamicEntity is null)
                {
                    throw new AioCoreException("Current entity does not exists");
                }
                if (dynamicEntity.TenantId != currentTenantId)
                {
                    throw new AioCoreException("Current entity does not exists for this tenant");
                }

                _dynamicUnitOfWork.DynamicEntities.Remove(dynamicEntity);

                await _dynamicUnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.DeleteAsync<dynamic>(request.Id);

                return new DeleteEntityRespone();
            }
        }
    }
}