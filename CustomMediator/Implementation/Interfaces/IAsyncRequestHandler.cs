using System.Threading;
using System.Threading.Tasks;

namespace CustomMediator.Implementation.Interfaces
{
    /// <summary>
    /// Defines a handler for a request
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled</typeparam>
    /// <typeparam name="TResponse">The type of response from the handler</typeparam>
    public interface IAsyncRequestHandler<in TRequest, TResponse> : IBaseRequestHandler
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines a handler for a request with a void (<see cref="Unit" />) response.
    /// You do not need to register this interface explicitly with a container as it inherits from the base <see cref="IRequestHandler{TRequest, TResponse}" /> interface.
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled</typeparam>
    public interface IAsyncRequestHandler<in TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : IRequest<Unit>
    {
    }
}
