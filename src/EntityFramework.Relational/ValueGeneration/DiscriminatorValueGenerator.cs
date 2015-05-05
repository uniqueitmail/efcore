// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.ValueGeneration;

namespace Microsoft.Data.Entity.Relational.ValueGeneration
{
    public class DiscriminatorValueGenerator : ValueGenerator
    {
        private readonly object _discriminator;

        public DiscriminatorValueGenerator([NotNull] object discriminator)
        {
            Check.NotNull(discriminator, nameof(discriminator));

            _discriminator = discriminator;
        }

        protected override object NextValue() => _discriminator;

        public override bool GeneratesTemporaryValues => false;
    }
}
