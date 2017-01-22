using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuttingEdge.Patterns.Repository
{
    public abstract class EntityConfiguration<TEntity> : EntityConfiguration where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class EntityConfiguration
    {
        public abstract void Map(EntityTypeBuilder builder);
    }

    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityConfiguration<TEntity> configuration)
            where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }

        public static void AddConfiguration(this ModelBuilder modelBuilder, EntityConfiguration configuration, Type entityType)
        {
            configuration.Map(modelBuilder.Entity(entityType));
        }
    }
}
