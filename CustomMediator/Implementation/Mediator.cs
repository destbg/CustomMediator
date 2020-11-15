using Autofac;
using CustomMediator.Implementation.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomMediator.Implementation
{
    public class Mediator : IMediator
    {
        private readonly IContainer _coantainer;
        private readonly Type _requestHandlerType;
        private readonly Type _asyncRequestHandlerType;

        public Mediator(IContainer coantainer)
        {
            _coantainer = coantainer;
            _requestHandlerType = typeof(IRequestHandler<,>);
            _asyncRequestHandlerType = typeof(IAsyncRequestHandler<,>);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();

            if (_coantainer.TryResolve(_requestHandlerType.MakeGenericType(requestType, typeof(TResponse)), out object handler))
                return Task.Run(() => (TResponse)InvokeHandlerHandleMethod(handler, request));

            if (_coantainer.TryResolve(_asyncRequestHandlerType.MakeGenericType(requestType, typeof(TResponse)), out handler))
                return (Task<TResponse>)InvokeHandlerHandleMethod(handler, request, cancellationToken);

            throw new InvalidOperationException("Mediator was passed in request class without handler " + requestType);
        }

        private object InvokeHandlerHandleMethod(object handler, params object[] parameters)
        {
            var handlerType = handler.GetType();
            var handlerHandleMethod = handlerType.GetMethod("Handle");
            return handlerHandleMethod.Invoke(handler, parameters);
        }
    }
}
