using System;

namespace WarHub.ArmouryModel.Source
{
    /// <summary>
    /// This object is a Core of some Node.
    /// </summary>
    /// <typeparam name="T">Type of the Node this instance is a Core of.</typeparam>
    public interface ICore<T> where T : SourceNode
    {
        /// <summary>
        /// Create an instance of the Node based on this Core.
        /// </summary>
        /// <param name="parent">Optional parent node of the created Node.</param>
        /// <returns>The newly created Node.</returns>
        T ToNode(SourceNode parent = null);

        // TODO uncomment after generation implemented

        //Type GetSerializationProxyType();

        //Type GetBuilderType();

        //Type GetSerializationEnumerableType();
    }
}
