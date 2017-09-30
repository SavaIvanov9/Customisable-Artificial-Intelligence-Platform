﻿namespace CAI.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using Models;

    public interface ICaiDbContext : IDisposable
    {
        IDbSet<Bot> Bots { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}