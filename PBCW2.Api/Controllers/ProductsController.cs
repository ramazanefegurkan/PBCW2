using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PBCW2.Api;
using PBCW2.Bussiness.Service;
using PBCW2.Schema;
using System.Xml.Linq;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // Retrieves a product by its ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Product>>> Get(long id)
    {
        var product = await _productService.GetById(id);
        return Ok(product);
    }

    // Lists all products or filters by name if the 'name' query parameter is provided
    [HttpGet("list")]
    public async Task<ActionResult<ApiResponse<List<Product>>>> List([FromQuery] string? name)
    {
        var products = await _productService.GetAll(name);
        return Ok(products);
    }

    // Sorts products by price in ascending or descending order based on the 'ascending' query parameter
    [HttpGet("sortByPrice")]
    public async Task<ActionResult<ApiResponse<List<Product>>>> SortByPrice([FromQuery] bool ascending = true)
    {
        var products = await _productService.SortByPrice(ascending);
        return Ok(products);
    }

    // Creates a new product
    [HttpPost]
    public async Task<ActionResult<ApiResponse<Product>>> Create([FromBody] ProductRequest product)
    {
        var createdProduct = await _productService.Add(product);
        return Ok(createdProduct);
    }

    // Updates an existing product by its ID
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Product>>> Update(long id, [FromBody] ProductRequest updatedProduct)
    {
        var isSuccess = await _productService.Update(id,updatedProduct);
        return Ok(isSuccess);
    }

    // Deletes a product by its ID
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var isSuccess = await _productService.Delete(id);
        return Ok(isSuccess);
    }

    [Authorize]
    [HttpGet("secure-data")]
    public IActionResult SecureData()
    {
        return Ok("This is secured data");
    }
}