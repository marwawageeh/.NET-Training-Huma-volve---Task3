
using Products.Domain.Entities;

namespace Products.Application.Interfaces;

public interface IProductRepository
{
	Task<List<Product>> GetAllAsync(
		CancellationToken cancellationToken);

	Task<Product?> GetByIdAsync(
		int id,
		CancellationToken cancellationToken);

	Task<Product> AddAsync(
		Product product,
		CancellationToken cancellationToken);
}
