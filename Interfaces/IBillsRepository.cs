using petshop.Models;

namespace PetsShop_API_DotNet.Interfaces
{
    public interface IBillsRepository
    {
        Task<List<Bill>?> GenerateBills(int[] orderIds);
        Task<List<Bill>?> GetBills();
        Task<Bill?> GetById(int billId);
        Task<bool?> Delete(int bill_id);


    }
}