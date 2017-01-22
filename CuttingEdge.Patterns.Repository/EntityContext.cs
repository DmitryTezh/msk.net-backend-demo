using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CuttingEdge.Patterns.Repository
{
    public class EntityContext<TDomain> : DbContext where TDomain : class
    {
        public EntityContext(DbContextOptions<EntityContext<TDomain>> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = from entityType in typeof(TDomain).Assembly.GetTypes()
                              where entityType.IsClass && !entityType.IsAbstract
                                    && entityType.IsSubclassOf(typeof(TDomain))
                              select entityType;
            var configTypes = from configType in Assembly.GetEntryAssembly().GetTypes()
                              let baseType = configType.BaseType
                              let genericType = baseType != null && baseType.IsGenericType ? baseType.GetGenericTypeDefinition() : null
                              let genericTypes = baseType?.GetGenericArguments()
                              let entityType = genericTypes?.Length > 0 ? genericTypes[0] : null
                              where configType.IsClass && !configType.IsAbstract
                                    && genericType != null && genericType == typeof(EntityConfiguration<>)
                                    && entityType != null && entityType.IsSubclassOf(typeof(TDomain))
                              select new
                              {
                                  ConfigType = configType,
                                  EntityType = entityType
                              };

            foreach (var entityType in entityTypes)
            {
                var configType = configTypes.FirstOrDefault(t => t.EntityType == entityType)?.ConfigType;
                if (configType != null)
                {
                    var configInstance = (EntityConfiguration)Activator.CreateInstance(configType);
                    modelBuilder.AddConfiguration(configInstance, entityType);
                }
                else
                {
                    modelBuilder.Entity(entityType);
                }
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}
