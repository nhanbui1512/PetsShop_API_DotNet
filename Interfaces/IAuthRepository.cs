

using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Dtos.User;
using PetsShop_API_DotNet.Dtos.User;

namespace petshop.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDTO?> CheckLogin(LoginDTO data);
    }
}