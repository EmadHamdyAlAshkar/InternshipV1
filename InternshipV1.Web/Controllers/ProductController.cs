using InternshipV1.Domain.Entities;
using InternshipV1.Repository.Interfaces;
using InternshipV1.Service.ProductServices;
using InternshipV1.Service.ProductServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipV1.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            return Ok(product);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto productDto)
        {
            await _productService.AddAsync(productDto);

            return Ok(productDto);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut]

        public async Task<IActionResult> UpdateProduct(UpdateProductDto productDto)
        {
            await _productService.UpdateAsync(productDto);
            return Ok(productDto);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok(new {Message = $"Product With Id ({id}) Deleted Successfully" });
        }
    }
}
