
using petshop.Dtos.Option;
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IOptionRepository
    {
        Task<List<OptionDTO>> GetAll();
        Task<OptionDTO> GetById(int id);
        Task<OptionDTO> Add(CreateOptionDTO data, int? productId);
        Task<OptionDTO> Update(UpdateOptionDTO data, int id);
        Task<List<OptionDTO>> GetByProductId(int productId);
        Task<List<OptionDTO>> GetOptionsByIds(int[] ids);
        Task<List<OptionDTO>> DecreaseQuantity(List<OrderItem> orderItems);
        Task<List<Option>> IncreaseQuantity(List<OrderItem> orderItems);
        Task<List<Option>> UpdateOptions(List<Option> options);
        Task<bool> Remove(int id);
    }
}