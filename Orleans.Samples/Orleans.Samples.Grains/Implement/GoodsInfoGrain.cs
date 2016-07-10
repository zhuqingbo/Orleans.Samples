using Orleans.Providers;
using Orleans.Samples.DataAccess.Context;
using Orleans.Samples.DataAccess.Entities;
using Orleans.Samples.Grains.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Samples.Grains.Implement
{
    [StorageProvider(ProviderName = "GoodsStorgeProvider")]
    public class GoodsInfoGrain : Grain<GoodsInfo>, IGoodsInfoGrain
    {
        public Task<List<GoodsInfo>> GetAllGoods()
        {
            using (var context = EntityContext.Factory())
            {
                return Task.FromResult(context.GoodsInfo.AsNoTracking().ToList());
            }
        }

        public async Task<bool> BuyGoods(int count, string buyerUser)
        {
            Console.WriteLine(buyerUser + ":购买商品--" + this.State.GoodsName + "    " + count + "个");

            if (count>0 && this.State.Stock >= count)
            {
                this.State.Stock -= count;
                OrdersInfo ordersInfo = new OrdersInfo();
                ordersInfo.OrderNo = Guid.NewGuid().ToString("n");
                ordersInfo.BuyCount = count;
                ordersInfo.BuyerNo = buyerUser;
                ordersInfo.GoodsNo = this.State.GoodsNo;
                ordersInfo.InTime = DateTime.Now;
                using (var context = EntityContext.Factory())
                {
                    context.OrdersInfo.Add(ordersInfo);
                    await context.SaveChangesAsync();
                }
                await this.WriteStateAsync();
                Console.WriteLine("购买完成");
                return await Task.FromResult(true);
            }
            else
            {
                Console.WriteLine("库存不足--剩余库存:" + this.State.Stock);
                return await Task.FromResult(false);
            }
        }
        
        
    }
}
