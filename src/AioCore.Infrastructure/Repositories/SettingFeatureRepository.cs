using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate;

namespace AioCore.Infrastructure.Repositories
{
    public class SettingFeatureRepository : ISettingFeatureRepository
    {
        private readonly AioCoreContext _context;

        public SettingFeatureRepository(AioCoreContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public SettingFeature Add(SettingFeature feature)
        {
            return _context.SettingFeatures.Add(feature).Entity;
        }

        public void Update(SettingFeature feature)
        {
            _context.Entry(feature).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var feature = _context.SettingFeatures.Find(id);
            _context.Entry(feature).State = EntityState.Deleted;
        }
    }
}