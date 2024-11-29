

using petshop.Dtos.Category;
using petshop.Dtos.Product;

namespace petshop.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id, int page, int limit);
        Task<CategoryDTO> Add(CreateCategoryDTO data);
        Task<CategoryDTO> Update(UpdateCategoryDTO data, int id);
        void Remove(int id);
    }
}