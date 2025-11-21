using MediatR;

namespace Basket.Commands;

public record DeleteBasketByUserNameCommand(string UserName) : IRequest<Unit>;