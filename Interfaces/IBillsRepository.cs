using petshop.Models;

namespace PetsShop_API_DotNet.Interfaces
{
    public interface IBillsRepository
    {
        Task<List<Bill>?> GenerateBills(int[] orderIds);
    }
}