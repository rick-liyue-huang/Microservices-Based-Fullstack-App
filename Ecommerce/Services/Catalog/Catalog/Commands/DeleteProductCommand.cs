using MediatR;

namespace Catalog.Commands;

public record DeleteProductCommand(string Id) : IRequest<bool>;