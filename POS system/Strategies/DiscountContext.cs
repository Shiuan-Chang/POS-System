using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS_system.Strategies
{
    internal class DiscountContext
    {
        private DiscountStrategy strategy = null;

        public DiscountContext(string type, List<Item> items, FoodDataModel.Discount discountData)
        {
            //反向查詢，後端的查詢，非前端畫面生成
            //根據動態傳入的類型名稱創建對應的折扣策略，這樣可以靈活地支持多種不同的折扣策略
            Type t = Type.GetType(type);
            strategy = (DiscountStrategy)Activator.CreateInstance(t, items, discountData);
        }

        public void ApplyDiscount()
        {
            strategy?.DiscountPolicyInterface();
        }
    }
}
