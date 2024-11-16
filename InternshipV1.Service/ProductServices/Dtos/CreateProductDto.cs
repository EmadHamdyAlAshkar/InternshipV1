using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.ProductServices.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
    }
}
