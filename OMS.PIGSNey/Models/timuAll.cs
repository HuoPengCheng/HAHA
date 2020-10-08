using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class timuAll
    {
        public int tmAid { get; set; }
        //题目编号
        public int tmid { get; set; }
        //题目标题
        public string biaoti { get; set; }
        //问卷id
        public int wj_id { get; set; }
        //选项编号
        public int xxid { get; set; }
        //选项内容
        public string xuanxiangneirong { get; set; }
        //票数
        public int piaoshu { get; set; }
        //题目id
        public int tm_id { get; set; }
        //问卷编号
        public int wjid { get; set; }
        //问卷名称
        public string mingcheng { get; set; }
        //发布时间
        public string shijian { get; set; }
    }
}
