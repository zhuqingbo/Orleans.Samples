using Orleans.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Samples.DataAccess.Context;
using Orleans.Samples.DataAccess.Entities;

namespace Orleans.Samples.StorageProvider
{
    public class GoodsStorgeProvider : IStorageProvider
    {
        public Logger Log
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            return TaskDone.Done;
        }

        public Task Close()
        {
            return TaskDone.Done;
        }

        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            this.Name = nameof(GoodsStorgeProvider);
            this.Log = providerRuntime.GetLogger(this.Name);

            return TaskDone.Done;
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            Console.WriteLine("获取商品信息");
            var goodsNo = grainReference.GetPrimaryKeyString();
            using (var context = EntityContext.Factory())
            {
                grainState.State = context.GoodsInfo.AsNoTracking().FirstOrDefault(o => o.GoodsNo.Equals(goodsNo));
            }
            await TaskDone.Done;
            
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var model = grainState.State as GoodsInfo;
            using (var context = EntityContext.Factory())
            {
                var entity = context.GoodsInfo.FirstOrDefault(o => o.GoodsNo.Equals(model.GoodsNo));
                entity.Stock = model.Stock;
                await context.SaveChangesAsync();
            }
        }
    }
}
