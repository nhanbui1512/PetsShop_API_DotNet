
using petshop.Dtos.Option;

namespace petshop.Interfaces
{
    public interface IOptionRepository
    {
        Task<List<OptionDTO>> GetAll();
        Task<OptionDTO> GetById(int id);
        Task<OptionDTO> Add(CreateOptionDTO data);
        Task<OptionDTO> Update(UpdateOptionDTO data, int id);
        Task<List<OptionDTO>> GetByProductId(int productId);
        void Remove(int id);
    }
}