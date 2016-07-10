using Orleans.Samples.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Samples.DataAccess.Context
{
    public class EntityContext : DbContext
    {

        private EntityContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<EntityContext>(null);
        }

        public static EntityContext Factory()
        {
            
            return new EntityContext("name=SamplesConnectionString");
        }
        public DbSet<GoodsInfo> GoodsInfo { get; set; }

        public DbSet<OrdersInfo> OrdersInfo { get; set; }
        
    }
}
