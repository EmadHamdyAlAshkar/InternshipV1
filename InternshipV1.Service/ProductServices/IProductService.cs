using InternshipV1.Domain.Entities;
using InternshipV1.Service.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductDetailsDto>> GetAllAsync();
        Task AddAsync(CreateProductDto product);
        Task UpdateAsync(UpdateProductDto product);
        Task DeleteAsync(int id);
    }
}
