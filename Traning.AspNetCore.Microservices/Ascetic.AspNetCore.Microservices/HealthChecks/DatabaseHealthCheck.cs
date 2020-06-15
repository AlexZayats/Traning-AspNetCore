using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Ascetic.AspNetCore.Microservices.HealthChecks
{
    public class DatabaseHealthCheck<TContextInterface, TContextImplementation> : IHealthCheck
        where TContextImplementation : DbContext, TContextInterface
    {
        private readonly TContextImplementation _dbContext;

        public DatabaseHealthCheck(TContextInterface dbContext)
        {
            _dbContext = (TContextImplementation)dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Database.CanConnectAsync(cancellationToken)
              ? HealthCheckResult.Healthy()
              : HealthCheckResult.Unhealthy();
        }
    }
}
