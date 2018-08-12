using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace WarHub.ArmouryModel.Source
{
    public static class ImmutableExtensions
    {
        public static ImmutableArray<TCore> ToImmutableRecursive<TBuilder, TCore>(this IReadOnlyList<TBuilder> builders)
            where TBuilder : IBuilder<TCore>
        {
            var count = builders.Count;
            var resultBuilder = ImmutableArray.CreateBuilder<TCore>(count);
            for (int i = 0; i < count; i++)
            {
                resultBuilder.Add(builders[i].ToImmutable());
            }
            return resultBuilder.MoveToImmutable();
        }

        internal static ImmutableArray<TCore> ToImmutableRecursive<TBuilder, TCore>(this List<TBuilder> builders)
            where TBuilder : IBuilder<TCore>
        {
            var count = builders.Count;
            var resultBuilder = ImmutableArray.CreateBuilder<TCore>(count);
            for (int i = 0; i < count; i++)
            {
                resultBuilder.Add(builders[i].ToImmutable());
            }
            return resultBuilder.MoveToImmutable();
        }

        public static List<TBuilder> ToBuildersList<TCore, TBuilder>(this ImmutableArray<TCore> cores)
            where TCore : IBuildable<TCore, TBuilder>
            where TBuilder : IBuilder<TCore>
        {
            return cores.Select(x => x.ToBuilder()).ToList();
        }
    }
}