using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;
    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams specParams)
    {
        var query = new GetAllProductsQuery(specParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        // todo: handle not found
        if (result == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        return Ok(result);
    }

    [HttpGet("productName/{name}")]
    public async Task<ActionResult<IList<ProductDto>>> GetProductsByName(string name)
    {
        var query = new GetProductsByNameQuery(name);
        var result = await _mediator.Send(query);
        // todo: handle empty result
        if (result == null || !result.Any())
        {
            throw new KeyNotFoundException($"No products found with name containing '{name}'.");
        }
        var dtoList = result.Select(p => p.ToProductDto()).ToList();
        return Ok(dtoList);
    }
    
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        // return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto productDto)
    {
        var command = productDto.ToProductCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("GetAllBrands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    [HttpGet("GetAllTypes")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("brand/{brand}", Name="GetProductsByBrand")]

    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByBrand(string brand)
    {
        // get the products
        var query = new GetProductsByBrandQuery(brand);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
}