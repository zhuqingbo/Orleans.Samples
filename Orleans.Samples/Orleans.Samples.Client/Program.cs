using Orleans.Samples.DataAccess.Entities;
using Orleans.Samples.Grains.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Samples.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GrainClient.Initialize("ClientConfiguration.xml");


            var grain = GrainClient.GrainFactory.GetGrain<IGoodsInfoGrain>("suiji");

            var goodsList = grain.GetAllGoods().Result;
            if (goodsList != null && goodsList.Count > 0)
            {
                Task tsk1 = new Task(() =>
                {
                    //Random rd = new Random(10);
                    for (int i = 0; i < 3000; i++)
                    {
                        foreach (var item in goodsList)
                        {
                            BuyGoods(item, 1, "张三");
                        }
                    }
                    Console.WriteLine("李四购买结束");
                });

                Task tsk2 = new Task(() =>
                {
                    //Random rd = new Random(10);
                    for (int i = 0; i < 3000; i++)
                    {
                        foreach (var item in goodsList)
                        {
                            BuyGoods(item, 1, "李四");
                        }
                    }
                    Console.WriteLine("李四购买结束");
                });

                Task tsk3 = new Task(() =>
                {
                    Random rd = new Random(10);
                    for (int i = 0; i < 1000; i++)
                    {
                        foreach (var item in goodsList)
                        {
                            BuyGoods(item, rd.Next(1, 10), "王二");
                        }
                    }

                    Console.WriteLine("王二购买结束");
                });
                tsk1.Start();
                tsk2.Start();
                tsk3.Start();

            }
            Console.ReadLine();
            Console.Write(strBuild);
        }

        public static void BuyGoods(GoodsInfo goods, int count, string buyerUser)
        {
            var grain = GrainClient.GrainFactory.GetGrain<IGoodsInfoGrain>(goods.GoodsNo);
            bool result = grain.BuyGoods(count, buyerUser).Result;
            if (result)
            {
                Addmsg(buyerUser + "--购买商品" + goods.GoodsName + "    " + count + "个");
            }
            else
            {
                Addmsg(buyerUser + "--购买商品" + goods.GoodsName + "    库存不足");
            }
        }
        static StringBuilder strBuild = new StringBuilder();
        private static void Addmsg(string msg)
        {
            msg = string.Format("{0}：{1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg, System.Environment.NewLine);
            lock (strBuild)
            {
                strBuild.Append(msg);

                if (strBuild.Length > 5000)
                {
                    Console.Write(strBuild);
                    strBuild.Clear();
                }
            }
        }


    }
}
