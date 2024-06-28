using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS_system.Discount;

namespace POS_system
{
    public class FoodDataModel
    {
        //拿到整包list的資料
        public Food[] foods { get; set; }
        public Discount[] discount { get; set; }


        //把food裡面的資料拆開來
        public class Food
        {
            public int id { get; set; }
            public string type { get; set; }
            public string[] items { get; set; }
        }

        //把discount裡面的資料拆開來
        public class Discount
        {
            public string key { get; set; }
            public string value { get; set; }

            public bool isActive { get; set; }

            public FoodCategory food { get; set; }
            public Giftnumber giftnumber { get; set; }
            public DiscountDetail discount { get; set; }
        }

        public class FoodCategory
        {
            public List<string> mainFood { get; set; }
            public List<string> subFood { get; set; }
            public List<string> drink { get; set; }
            public List<string> dessert { get; set; }
        }

        public class Giftnumber
        {
            public int buyCount { get; set; }
            public int giftCount { get; set; }
        }

        public class DiscountDetail
        {
            public int discountPrice { get; set; }
            public double discountPercentage { get; set; }
            public double allDiscountPercentage { get; set; }
        }

    }
}
