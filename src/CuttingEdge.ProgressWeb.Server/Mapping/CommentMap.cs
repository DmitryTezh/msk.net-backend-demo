using System;
using CuttingEdge.Patterns.Repository;
using CuttingEdge.ProgressWeb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuttingEdge.ProgressWeb.Server.Mapping
{
    public class CommentMap : EntityConfiguration<Comment>
    {
        public override void Map(EntityTypeBuilder builder)
        {
            builder.ToTable("Comment");
        }

        public override void Map(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
        }
    }
}