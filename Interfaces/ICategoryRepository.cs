

using petshop.Dtos.Category;
using petshop.Dtos.Product;

namespace petshop.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task<CategoryDTO> Add(CreateCategoryDTO data);
        void Update(UpdateCategoryDTO data);
        void Remove(int id);
    }
}