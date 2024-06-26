

using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Dtos.User;
using PetsShop_API_DotNet.Dtos.User;

namespace petshop.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAll(GetUserDTO data);
        Task<UserDTO> GetById(int id);
        Task<UserDTO> Add(CreateUserDTO data);
        Task<UserDTO> Update(UpdateUserDTO data, int id);
        Task<bool> Remove(int id);
    }
}