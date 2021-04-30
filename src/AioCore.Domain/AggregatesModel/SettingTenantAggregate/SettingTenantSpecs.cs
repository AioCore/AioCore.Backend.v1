using AioCore.Shared.Specifications;

namespace AioCore.Domain.AggregatesModel.SettingTenantAggregate
{
    public static class SettingTenantSpecs
    {
        public static ISpecification<SettingTenant> SearchTenantByName(string query) =>
            new Specification<SettingTenant>(x => string.IsNullOrEmpty(query) || x.Name.Contains(query));
    }
}