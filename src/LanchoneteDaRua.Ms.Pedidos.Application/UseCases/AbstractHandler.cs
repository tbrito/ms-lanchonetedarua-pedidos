using System.Net;
using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases;

public abstract class AbstractHandler<TRequest, TResponse> : 
    IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response, new()
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    
    protected TResponse GenerateErrorResponse(HttpStatusCode? statusCode, IEnumerable<string> errorMessages)
    {
        return new TResponse
        {
            ErrorCode = statusCode,
            ErrorMessages = string.Join(", ",errorMessages.Select(x => x))
        };
    }

    protected TResponse GenerateErrorResponse(HttpStatusCode? statusCode, string errorMessages)
    {
        return new TResponse
        {
            ErrorCode = statusCode,
            ErrorMessages = errorMessages
        };
    }

    protected List<TResponse> GenerateErrorResponseList(HttpStatusCode? statusCode, string errorMessages)
    {
        return new List<TResponse>()
        {
            new TResponse
            {
                ErrorCode = statusCode,
                ErrorMessages = errorMessages
            }

        };
    }
}
