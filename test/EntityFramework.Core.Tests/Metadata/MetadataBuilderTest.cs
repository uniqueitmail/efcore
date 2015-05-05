// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Xunit;

namespace Microsoft.Data.Entity.Tests.Metadata
{
    public class MetadataBuilderTest
    {
        [Fact]
        public void Can_write_convention_model_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .ModelBuilderExtension("V1")
                .ModelBuilderExtension("V2");

            Assert.IsType<ModelBuilder>(returnedBuilder);

            var model = builder.Model;

            Assert.Equal("V2.Annotation", model["Annotation"]);
            Assert.Equal("V2.Metadata", model["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_entity_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity(typeof(Gunter))
                .EntityBuilderExtension("V1")
                .EntityBuilderExtension("V2");

            Assert.IsType<EntityTypeBuilder>(returnedBuilder);

            var model = builder.Model;
            var entityType = model.GetEntityType(typeof(Gunter));

            Assert.Equal("V2.Annotation", entityType["Annotation"]);
            Assert.Equal("V2.Metadata", entityType["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_entity_builder_extension_and_use_with_generic_builder()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .EntityBuilderExtension("V1")
                .EntityBuilderExtension("V2");

            Assert.IsType<EntityTypeBuilder<Gunter>>(returnedBuilder);

            var model = builder.Model;
            var entityType = model.GetEntityType(typeof(Gunter));

            Assert.Equal("V2.Annotation", entityType["Annotation"]);
            Assert.Equal("V2.Metadata", entityType["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_generic_convention_entity_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .GenericEntityBuilderExtension("V1")
                .GenericEntityBuilderExtension("V2");

            Assert.IsType<EntityTypeBuilder<Gunter>>(returnedBuilder);

            var model = builder.Model;
            var entityType = model.GetEntityType(typeof(Gunter));

            Assert.Equal("V2.Annotation", entityType["Annotation"]);
            Assert.Equal("V2.Metadata", entityType["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_key_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .Key(e => e.Id)
                .KeyBuilderExtension("V1")
                .KeyBuilderExtension("V2");

            Assert.IsType<KeyBuilder>(returnedBuilder);

            var model = builder.Model;
            var key = model.GetEntityType(typeof(Gunter)).GetPrimaryKey();

            Assert.Equal("V2.Annotation", key["Annotation"]);
            Assert.Equal("V2.Metadata", key["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_property_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .Property(e => e.Id)
                .PropertyBuilderExtension("V1")
                .PropertyBuilderExtension("V2");

            Assert.IsType<PropertyBuilder<int>>(returnedBuilder);

            var model = builder.Model;
            var property = model.GetEntityType(typeof(Gunter)).GetProperty("Id");

            Assert.Equal("V2.Annotation", property["Annotation"]);
            Assert.Equal("V2.Metadata", property["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_index_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .Index(e => e.Id)
                .IndexBuilderExtension("V1")
                .IndexBuilderExtension("V2");

            Assert.IsType<IndexBuilder>(returnedBuilder);

            var model = builder.Model;
            var index = model.GetEntityType(typeof(Gunter)).Indexes.Single();

            Assert.Equal("V2.Annotation", index["Annotation"]);
            Assert.Equal("V2.Metadata", index["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_one_to_many_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>().Collection(e => e.Gates).InverseReference(e => e.Gunter)
                .OneToManyBuilderExtension("V1")
                .OneToManyBuilderExtension("V2");

            Assert.IsType<ReferenceCollectionBuilder<Gunter, Gate>>(returnedBuilder);

            var model = builder.Model;
            var foreignKey = model.GetEntityType(typeof(Gate)).GetForeignKeys().Single();

            Assert.Equal("V2.Annotation", foreignKey["Annotation"]);
            Assert.Equal("V2.Metadata", foreignKey["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_many_to_one_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gate>().Reference(e => e.Gunter).InverseCollection(e => e.Gates)
                .ManyToOneBuilderExtension("V1")
                .ManyToOneBuilderExtension("V2");

            Assert.IsType<CollectionReferenceBuilder<Gate, Gunter>>(returnedBuilder);

            var model = builder.Model;
            var foreignKey = model.GetEntityType(typeof(Gate)).GetForeignKeys().Single();

            Assert.Equal("V2.Annotation", foreignKey["Annotation"]);
            Assert.Equal("V2.Metadata", foreignKey["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_one_to_one_builder_extension()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Avatar>().Reference(e => e.Gunter).InverseReference(e => e.Avatar)
                .PrincipalKey<Gunter>(e => e.Id)
                .OneToOneBuilderExtension("V1")
                .OneToOneBuilderExtension("V2");

            Assert.IsType<ReferenceReferenceBuilder<Avatar, Gunter>>(returnedBuilder);

            var model = builder.Model;
            var foreignKey = model.GetEntityType(typeof(Avatar)).GetForeignKeys().Single();

            Assert.Equal("V2.Annotation", foreignKey["Annotation"]);
            Assert.Equal("V2.Metadata", foreignKey["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_model_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<ModelBuilder>(returnedBuilder);

            var model = builder.Model;

            Assert.Equal("V2.Annotation", model["Annotation"]);
            Assert.Equal("V2.Metadata", model["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_entity_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity(typeof(Gunter))
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<EntityTypeBuilder>(returnedBuilder);

            var model = builder.Model;
            var entityType = model.GetEntityType(typeof(Gunter));

            Assert.Equal("V2.Annotation", entityType["Annotation"]);
            Assert.Equal("V2.Metadata", entityType["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_entity_builder_extension_and_use_with_generic_builder_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<EntityTypeBuilder<Gunter>>(returnedBuilder);

            var model = builder.Model;
            var entityType = model.GetEntityType(typeof(Gunter));

            Assert.Equal("V2.Annotation", entityType["Annotation"]);
            Assert.Equal("V2.Metadata", entityType["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_generic_convention_entity_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<EntityTypeBuilder<Gunter>>(returnedBuilder);

            var model = builder.Model;
            var entityType = model.GetEntityType(typeof(Gunter));

            Assert.Equal("V2.Annotation", entityType["Annotation"]);
            Assert.Equal("V2.Metadata", entityType["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_key_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .Key(e => e.Id)
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<KeyBuilder>(returnedBuilder);

            var model = builder.Model;
            var key = model.GetEntityType(typeof(Gunter)).GetPrimaryKey();

            Assert.Equal("V2.Annotation", key["Annotation"]);
            Assert.Equal("V2.Metadata", key["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_property_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .Property(e => e.Id)
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<PropertyBuilder<int>>(returnedBuilder);

            var model = builder.Model;
            var property = model.GetEntityType(typeof(Gunter)).GetProperty("Id");

            Assert.Equal("V2.Annotation", property["Annotation"]);
            Assert.Equal("V2.Metadata", property["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_index_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>()
                .Index(e => e.Id)
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<IndexBuilder>(returnedBuilder);

            var model = builder.Model;
            var index = model.GetEntityType(typeof(Gunter)).Indexes.Single();

            Assert.Equal("V2.Annotation", index["Annotation"]);
            Assert.Equal("V2.Metadata", index["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_one_to_many_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gunter>().Collection(e => e.Gates).InverseReference(e => e.Gunter)
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<ReferenceCollectionBuilder<Gunter, Gate>>(returnedBuilder);

            var model = builder.Model;
            var foreignKey = model.GetEntityType(typeof(Gate)).GetForeignKeys().Single();

            Assert.Equal("V2.Annotation", foreignKey["Annotation"]);
            Assert.Equal("V2.Metadata", foreignKey["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_many_to_one_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Gate>().Reference(e => e.Gunter).InverseCollection(e => e.Gates)
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<CollectionReferenceBuilder<Gate, Gunter>>(returnedBuilder);

            var model = builder.Model;
            var foreignKey = model.GetEntityType(typeof(Gate)).GetForeignKeys().Single();

            Assert.Equal("V2.Annotation", foreignKey["Annotation"]);
            Assert.Equal("V2.Metadata", foreignKey["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        [Fact]
        public void Can_write_convention_one_to_one_builder_extension_with_common_name()
        {
            var builder = CreateModelBuilder();

            var returnedBuilder = builder
                .Entity<Avatar>().Reference(e => e.Gunter).InverseReference(e => e.Avatar)
                .PrincipalKey<Gunter>(e => e.Id)
                .SharedNameExtension("V1")
                .SharedNameExtension("V2");

            Assert.IsType<ReferenceReferenceBuilder<Avatar, Gunter>>(returnedBuilder);

            var model = builder.Model;
            var foreignKey = model.GetEntityType(typeof(Avatar)).GetForeignKeys().Single();

            Assert.Equal("V2.Annotation", foreignKey["Annotation"]);
            Assert.Equal("V2.Metadata", foreignKey["Metadata"]);
            Assert.Equal("V2.Model", model["Model"]);
        }

        protected virtual ModelBuilder CreateModelBuilder()
        {
            return TestHelpers.Instance.CreateConventionBuilder();
        }

        private class Gunter
        {
            public int Id { get; set; }

            public ICollection<Gate> Gates { get; set; }

            public Avatar Avatar { get; set; }
        }

        private class Gate
        {
            public int Id { get; set; }

            public int GunterId { get; set; }
            public Gunter Gunter { get; set; }
        }

        private class Avatar
        {
            public int Id { get; set; }

            public Gunter Gunter { get; set; }
        }
    }

    internal static class TestExtensions
    {
        public static ModelBuilder ModelBuilderExtension(this ModelBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Model["Metadata"] = value + ".Metadata";
            builder.Model["Model"] = value + ".Model";

            return builder;
        }

        public static EntityTypeBuilder EntityBuilderExtension(this EntityTypeBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static EntityTypeBuilder<TEntity> GenericEntityBuilderExtension<TEntity>(this EntityTypeBuilder<TEntity>builder, string value)
            where TEntity : class
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static KeyBuilder KeyBuilderExtension(this KeyBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static PropertyBuilder PropertyBuilderExtension(this PropertyBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static IndexBuilder IndexBuilderExtension(this IndexBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static ReferenceCollectionBuilder OneToManyBuilderExtension(this ReferenceCollectionBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static CollectionReferenceBuilder ManyToOneBuilderExtension(this CollectionReferenceBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static ReferenceReferenceBuilder OneToOneBuilderExtension(this ReferenceReferenceBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static ModelBuilder SharedNameExtension(this ModelBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Model["Metadata"] = value + ".Metadata";
            builder.Model["Model"] = value + ".Model";

            return builder;
        }

        public static EntityTypeBuilder SharedNameExtension(this EntityTypeBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static EntityTypeBuilder<TEntity> SharedNameExtension<TEntity, TBuilder>(this EntityTypeBuilder<TEntity> builder, string value)
            where TEntity : class
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static KeyBuilder SharedNameExtension(this KeyBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static PropertyBuilder SharedNameExtension(this PropertyBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static IndexBuilder SharedNameExtension(this IndexBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static ReferenceCollectionBuilder SharedNameExtension(this ReferenceCollectionBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static CollectionReferenceBuilder SharedNameExtension(this CollectionReferenceBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }

        public static ReferenceReferenceBuilder SharedNameExtension(this ReferenceReferenceBuilder builder, string value)
        {
            builder.Annotation("Annotation", value + ".Annotation");
            builder.Metadata["Metadata"] = value + ".Metadata";
            ((IAccessor<Model>)builder).Service["Model"] = value + ".Model";

            return builder;
        }
    }
}
