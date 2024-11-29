

using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Dtos.User;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.User;

namespace petshop.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedResult<UserDTO>> GetAll(GetUserDTO data);
        Task<UserDTO> GetById(int id);
        Task<UserDTO> Add(CreateUserDTO data);
        Task<UserDTO> Update(UpdateUserDTO data, int userId);
        Task<bool> Remove(int id);
        Task<bool?> ChangePassword(PasswordDTO data, int userId);
        Task<User> SaveChange(User user);
        Task<User> UpdateAvatar(string filePath, int userId);

    }
}