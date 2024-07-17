using PBCW2.Schema;
using PBW2.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCW2.Bussiness.Service
{
    public interface IProductService
    {
        Task<ApiResponse<List<ProductResponse>>> GetAll(string? name);
        Task<ApiResponse<ProductResponse>> GetById(long id);
        Task<ApiResponse<ProductResponse>> Add(ProductRequest product);
        Task<ApiResponse<bool>> Update(long id,ProductRequest product);
        Task<ApiResponse<bool>> Delete(int id);
        Task<ApiResponse<List<Product>>> SortByPrice(bool ascending);
    }
}
