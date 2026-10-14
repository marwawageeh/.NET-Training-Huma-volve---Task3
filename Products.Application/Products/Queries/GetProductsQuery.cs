using MediatR;
using Products.Application.DTOs;
using Products.Application.Interfaces;

namespace Products.Application.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;

public class GetProductsHandler
	: IRequestHandler<GetProductsQuery, List<ProductDto>>
{
	private readonly IProductRepository _repository;

	public GetProductsHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<ProductDto>> Handle(
		GetProductsQuery request,
		CancellationToken cancellationToken)
	{
		var products = await _repository.GetAllAsync(cancellationToken);

		return products.Select(p => new ProductDto
		{
			Id = p.Id,
			Name = p.Name,
			Price = p.Price
		}).ToList();
	}
}