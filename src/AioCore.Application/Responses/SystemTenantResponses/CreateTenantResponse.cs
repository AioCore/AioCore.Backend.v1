using AioCore.Domain.CoreEntities;
using AutoMapper;
using Package.AutoMapper;
using Package.DatabaseManagement;
using Package.Elasticsearch;
using System;

namespace AioCore.Application.Responses.SystemTenantResponses
{
    public record CreateTenantResponse : IMap
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid FaviconId { get; set; }

        public Guid LogoId { get; set; }

        public DatabaseInfo Database { get; set; }

        public ElasticsearchInfo Elasticsearch { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SystemTenant, CreateTenantResponse>()
                .ForMember(t => t.Database, t => t.MapFrom(x => DatabaseInfo.Parse(x.DatabaseInfo)))
                .ForMember(t => t.Elasticsearch, t => t.MapFrom(x => ElasticsearchInfo.Parse(x.ElasticsearchInfo)));
        }
    }
}