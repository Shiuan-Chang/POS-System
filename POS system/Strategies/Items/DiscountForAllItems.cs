using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_system.Strategies.Items
{
    internal class DiscountForAllItems : DiscountStrategy
    {
        private FoodDataModel.Discount discountData;

        public DiscountForAllItems(List<Item> items, FoodDataModel.Discount discountData) : base(items)
        {
            this.discountData = discountData;
        }

        public override void DiscountPolicyInterface() 
        {
            double discountRate = discountData.discount.allDiscountPercentage;

            foreach (Item item in items)
            {
                if(!item.IsDiscountApplied)
                {
                    item.itemNotes = $"{discountRate}折優惠";
                    item.totalPrice = (int)(item.totalPrice);
                    item.IsDiscountApplied = true;
                }               
            }

            var result = items.Sum(x=>x.price*(1-discountRate));
            items.Add(new Item($"(贈送)全品項打{discountRate}折$-{result}", 1));
        }
    }
}
