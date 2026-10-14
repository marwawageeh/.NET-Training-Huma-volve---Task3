using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs;
using Products.Application.Products.Commands;
using Products.Application.Products.Queries;

namespace Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly ISender _sender;

	public ProductsController(ISender sender)
	{
		_sender = sender;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll(
		CancellationToken cancellationToken)
	{
		var products = await _sender.Send(
			new GetProductsQuery(), cancellationToken);

		return Ok(products);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(
		int id,
		CancellationToken cancellationToken)
	{
		var product = await _sender.Send(
			new GetProductByIdQuery(id), cancellationToken);

		if (product is null)
			return NotFound();

		return Ok(product);
	}

	[HttpPost]
	public async Task<IActionResult> Create(
		[FromBody] CreateProductDto dto,
		CancellationToken cancellationToken)
	{
		var product = await _sender.Send(
			new CreateProductCommand(dto.Name, dto.Price),
			cancellationToken);

		return CreatedAtAction(
			nameof(GetById),
			new { id = product.Id },
			product);
	}
}