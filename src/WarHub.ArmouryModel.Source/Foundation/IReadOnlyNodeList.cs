using System.Collections.Generic;

namespace WarHub.ArmouryModel.Source
{
    /// <summary>
    /// This is an abstraction of a list-like lazy object provider.
    /// Similar to an <see cref="IReadOnlyList{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IReadOnlyNodeList<out T> where T : SourceNode
    {
        /// <summary>
        /// Gets the number of elements in the list.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets an item at the specified index. This may cause computation if necessary.
        /// </summary>
        /// <param name="index">The index to retrieve item at.</param>
        /// <returns>Item at a given index.</returns>
        T this[int index] { get; }
    }
}