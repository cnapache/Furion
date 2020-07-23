﻿using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Filters;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Fur.Core.DbEntities
{
    [Table("Tests")]
    public class Test : DbEntityBase, IDbQueryFilterOfT<Test>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Dictionary<Expression<Func<Test, bool>>, IEnumerable<Type>> HasQueryFilter(ITenantProvider tenantProvider)
        {
            if (tenantProvider == null) return null;

            return new Dictionary<Expression<Func<Test, bool>>, IEnumerable<Type>>
            {
                {
                    b => EF.Property<int>(b, nameof(DbEntityBase.TenantId)) == tenantProvider.GetTenantId(),
                    new List<Type>
                    {
                        typeof(FurDbContextIdentifier)
                    }
                }
            };
        }
    }
}