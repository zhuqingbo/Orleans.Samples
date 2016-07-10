using Orleans.Samples.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Samples.Grains.Interface
{
    public interface IGoodsInfoGrain : IGrainWithStringKey
    {
        Task<List<GoodsInfo>> GetAllGoods();
        Task<bool> BuyGoods(int count, string buyerUser);
    }
}
