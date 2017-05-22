using System;
using CuttingEdge.Patterns.Repository;
using CuttingEdge.DemoWeb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuttingEdge.DemoWeb.Server.Mapping
{
    public class TodoItemMap : EntityConfiguration<TodoItem>
    {
        public override void Map(EntityTypeBuilder builder)
        {
            builder.ToTable("TodoItem");
        }

        public override void Map(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("TodoItem");
        }
    }
}