﻿// WarHub licenses this file to you under the MIT license.
// See LICENSE file in the project root for more information.

namespace WarHub.Armoury.Model
{
    public interface IRuleMock : INameable, IBookIndexable,
        IHideable, IForceItem
    {
        string DescriptionText { get; set; }

        ILinkPath<IRule> OriginRulePath { get; }
    }
}
