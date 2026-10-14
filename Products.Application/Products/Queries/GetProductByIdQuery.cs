using MediatR;
using Products.Application.DTOs;
using Products.Application.Interfaces;

namespace Products.Application.Products.Queries;

public record GetProductByIdQuery(int Id)
	: IRequest<ProductDto?>;

public class GetProductByIdHandler
	: IRequestHandler<GetProductByIdQuery, ProductDto?>
{
	private readonly IProductRepository _repository;

	public GetProductByIdHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<ProductDto?> Handle(
		GetProductByIdQuery request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(
			request.Id, cancellationToken);

		if (product is null)
			return null;

		return new ProductDto
		{
			Id = product.Id,
			Name = product.Name,
			Price = product.Price
		};
	}
}