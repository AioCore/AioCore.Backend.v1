﻿using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;

namespace AioCore.Infrastructure.Repositories
{
    public class SecurityUserRepository : ISecurityUserRepository
    {
        private readonly AioCoreContext _context;

        public SecurityUserRepository(AioCoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public SecurityUser Add(SecurityUser user)
        {
            return _context.SecurityUsers.Add(user).Entity;
        }

        public void Update(SecurityUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}