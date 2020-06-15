using MediatR;
using OpenTracing;
using System.Threading;
using System.Threading.Tasks;

namespace Ascetic.AspNetCore.Microservices.RequestBehaviours
{
    public class RequestTraceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ITracer _tracer;

        public RequestTraceBehaviour(ITracer tracer)
        {
            _tracer = tracer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (var requestSpan = _tracer.BuildSpan($"CQRS: {typeof(TRequest).FullName}").StartActive(finishSpanOnDispose: true))
            {
                var response = await next();
                return response;
            }
        }
    }
}
