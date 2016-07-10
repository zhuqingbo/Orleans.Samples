using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Samples.HostSilo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new SiloHost("Default"))
            {
                host.ConfigFileName = "OrleansConfiguration.xml";
                host.LoadOrleansConfig();
                host.InitializeOrleansSilo();
                host.StartOrleansSilo();

                Console.WriteLine("启动成功");
                Console.Read();
            }
        }
    }
}
