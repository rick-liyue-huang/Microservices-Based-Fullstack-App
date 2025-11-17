using Catalog.Responses;
using MediatR;

namespace Catalog.Queries;

public record GetProductsByNameQuery(string Name) : IRequest<IList<ProductResponse>>;