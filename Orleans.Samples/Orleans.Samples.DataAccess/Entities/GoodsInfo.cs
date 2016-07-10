using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Samples.DataAccess.Entities
{
    [Table("GoodsInfo")]
    public class GoodsInfo
    {
        /// <summary>
        /// 数据主键
        /// </summary>
        [Key]
        public int TranNum { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsNo { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
    }
}
