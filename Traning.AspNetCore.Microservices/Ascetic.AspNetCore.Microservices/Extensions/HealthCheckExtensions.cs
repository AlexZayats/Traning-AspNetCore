using Ascetic.AspNetCore.Microservices.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddDatabaseCheck<TContextInterface, TContextImplementation>(this IHealthChecksBuilder builder, IEnumerable<string> tags = null)
            where TContextImplementation : DbContext, TContextInterface
        {
            return builder.AddCheck<DatabaseHealthCheck<TContextInterface, TContextImplementation>>(typeof(TContextImplementation).Name, tags: tags);
        }
    }
}
