using MediatR;
using Products.Application.DTOs;
using Products.Application.Interfaces;
using Products.Domain.Entities;

namespace Products.Application.Products.Commands;

public record CreateProductCommand(string Name, decimal Price)
	: IRequest<ProductDto>;

public class CreateProductHandler
	: IRequestHandler<CreateProductCommand, ProductDto>
{
	private readonly IProductRepository _repository;

	public CreateProductHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<ProductDto> Handle(
		CreateProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = new Product
		{
			Name = request.Name.Trim(),
			Price = request.Price
		};

		var created = await _repository.AddAsync(
			product, cancellationToken);

		return new ProductDto
		{
			Id = created.Id,
			Name = created.Name,
			Price = created.Price
		};
	}
}