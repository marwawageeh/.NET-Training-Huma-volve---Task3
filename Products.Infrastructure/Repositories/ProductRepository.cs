using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly AppDbContext _context;

	public ProductRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<List<Product>> GetAllAsync(
		CancellationToken cancellationToken)
	{
		return await _context.Products
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<Product?> GetByIdAsync(
		int id,
		CancellationToken cancellationToken)
	{
		return await _context.Products
			.AsNoTracking()
			.FirstOrDefaultAsync(
				p => p.Id == id, cancellationToken);
	}

	public async Task<Product> AddAsync(
		Product product,
		CancellationToken cancellationToken)
	{
		_context.Products.Add(product);

		await _context.SaveChangesAsync(cancellationToken);

		return product;
	}
}