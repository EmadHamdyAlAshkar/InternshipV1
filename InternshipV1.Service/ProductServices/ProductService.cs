using AutoMapper;
using InternshipV1.Domain.Entities;
using InternshipV1.Repository.Interfaces;
using InternshipV1.Service.ProductServices.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IproductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IproductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return mappedProducts;
        }

        public async Task<ProductDetailsDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedProduct;
        }

        public async Task UpdateAsync(UpdateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);
        }
    }
}
