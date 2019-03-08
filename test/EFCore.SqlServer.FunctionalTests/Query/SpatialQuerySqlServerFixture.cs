﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class SpatialQuerySqlServerFixture : SpatialQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => SqlServerTestStoreFactory.Instance;

        protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
            => base.AddServices(serviceCollection)
                .AddEntityFrameworkSqlServerNetTopologySuite();

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
        {
            var optionsBuilder = base.AddOptions(builder);
            new SqlServerDbContextOptionsBuilder(optionsBuilder).UseNetTopologySuite();

            return optionsBuilder;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            //modelBuilder.HasDbFunction(
            //    typeof(GeoExtensions).GetMethod(nameof(GeoExtensions.Distance)),
            //    b => b.HasTranslation(
            //        e => new SqlFunctionExpression(
            //            e.First(),
            //            "STDistance",
            //            typeof(double),
            //            e.Skip(1))));
        }
    }
}
