﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SutRepositoryLibraryTests.CollectionFixtureTests
{
    [CollectionDefinition(nameof(DatabaseCollection))]
    public class DatabaseCollection : ICollectionFixture<DataContextCollectionFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
