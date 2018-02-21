﻿using System.Collections.Immutable;
using WarHub.ArmouryModel.Source.CodeGeneration.Tests.GeneratedCode;
using Xunit;

namespace WarHub.ArmouryModel.Source.CodeGeneration.Tests
{
    public class GeneratedCoreTests
    {
        [Fact]
        public void With_SimpleProperty_DoesNotModifyInstance()
        {
            const string newName = "New Name";
            var subject = new ItemCore.Builder().ToImmutable();
            subject.WithName(newName);
            Assert.Null(subject.Name);
        }

        [Fact]
        public void With_SimpleProperty_CreatesModifiedInstance()
        {
            const string newName = "New Name";
            var subject = new ItemCore.Builder().ToImmutable();
            var newInstance = subject.WithName(newName);
            Assert.Equal(newName, newInstance.Name);
        }

        [Fact]
        public void With_CollectionProperty_DoesNotModifyInstance()
        {
            var item = new ItemCore.Builder().ToImmutable();
            var subject = new ContainerCore.Builder().ToImmutable();
            subject.WithItems(new[] { item }.ToImmutableArray());
            Assert.Empty(subject.Items);
        }

        [Fact]
        public void With_CollectionProperty_CreatesModifiedInstance()
        {
            var item = new ItemCore.Builder().ToImmutable();
            var subject = new ContainerCore.Builder().ToImmutable();
            var newInstance = subject.WithItems(new[] { item }.ToImmutableArray());
            Assert.Collection(newInstance.Items, x => Assert.Same(item, x));
        }

        [Fact]
        public void ToBuilder_CopiesSimpleProperty()
        {
            const string name = "Name";
            var subject = new ItemCore.Builder() { Name = name }.ToImmutable();
            var builder = subject.ToBuilder();
            Assert.Equal(subject.Name, builder.Name);
        }

        [Fact]
        public void ToBuilder_CopiesCollectionProperty()
        {
            const string itemId = "qwerty12345";
            var itemBuilder = new ItemCore.Builder() { Id = itemId };
            var container = new ContainerCore.Builder() { Items = { itemBuilder } }.ToImmutable();
            var builder = container.ToBuilder();
            Assert.Collection(builder.Items, x => Assert.Same(itemId, x.Id));
        }
    }
}