using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petshop.Models;
using PetsShop_API_DotNet.Interfaces;

namespace PetsShop_API_DotNet.Repository
{
    public class BillsRepository : IBillsRepository
    {
        public Task<List<Bill>?> GenerateBills(int[] billIds)
        {
            throw new NotImplementedException();
        }
    }
}