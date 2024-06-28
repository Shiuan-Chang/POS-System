using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS_system.FoodDataModel;

namespace POS_system.Strategies.Items
{
    internal class ThreeMainFoodWith85discount : DiscountStrategy
    {
        private FoodDataModel.Discount discountData;

        public ThreeMainFoodWith85discount(List<Item> items, FoodDataModel.Discount discountData) : base(items)
        {
            this.discountData = discountData;
        }
        public override void DiscountPolicyInterface()
        {
            double discountRate = discountData.discount.discountPercentage;
            bool threeItems = false;
            var mainFoodItems = items.Where(x => discountData.food.mainFood.Contains(x.itemName));

            // 計算符合條件的主餐總數量
            int totalMainFoodQuantity = mainFoodItems.Sum(x => x.quantity);

            // 當主餐的總數量能被3整除時，對所有主餐項目應用折扣,但無法被整除的部分，不能使用折扣

            // linq: 找到主餐的數量，若滿足三個同品項一筆，可以匯入到折扣當中；多出的那部份要額外做計算，直到滿足3個一筆，或是不能打折




            // 結果：匯成一筆折扣的資訊$"(贈送)三項主餐打{discountRate}折$-{result}", 1)


            // / => 商除 %=>餘除
            // 7/3 = 2 


            var threeSameMainFoodItems = items.Where(x => discountData.food.mainFood.Contains(x.itemName) && x.quantity / 3 > 0)//這個是商除，算出來是2
                .Select(x => ((x.quantity / 3)*3) * x.price * (1 - discountRate));
            int discounts = threeSameMainFoodItems.Sum(x => (int)x);

            //foreach (Item item in mainFoodItems)
            //{
            //    if(!item.IsDiscountApplied) 
            //    {
            //        item.itemNotes = $"主餐打{discountRate}折";
            //        item.totalPrice = (int)(item.totalPrice);
            //        item.IsDiscountApplied = true;
            //    }                   
            //}

            if(discounts>0)
            {
                var result = items.Sum(x => x.price * (1 - discountRate));
                items.Add(new Item($"(贈送)三項主餐打{discountRate}折$-{discounts}", 1));
            }
       

        }
    }
}
