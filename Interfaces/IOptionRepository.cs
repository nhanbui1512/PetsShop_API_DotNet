
using petshop.Dtos.Option;
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IOptionRepository
    {
        Task<List<OptionDTO>> GetAll();
        Task<OptionDTO?> GetById(int id);
        Task<OptionDTO?> Add(CreateOptionDTO data, int? productId);
        Task<OptionDTO?> Update(UpdateOptionDTO data, int id);
        Task<List<OptionDTO>?> GetByProductId(int productId);
        Task<List<OptionDTO>?> GetOptionsByIds(int[] ids);
        Task<List<OptionDTO>?> DecreaseQuantity(List<OrderItem> orderItems);
        Task<bool> Remove(int id);
    }
}