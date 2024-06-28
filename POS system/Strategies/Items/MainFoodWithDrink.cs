using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_system.Strategies.Items
{
    internal class MainFoodWithDrink : DiscountStrategy
    {
        private FoodDataModel.Discount discountData;

        public MainFoodWithDrink(List<Item> items, FoodDataModel.Discount discountData) : base(items)
        {
            this.discountData = discountData;
        }

        public override void DiscountPolicyInterface()
        {
            var discountPrice = discountData.discount.discountPrice;

            // 找出主食類項目，按照價格由低排到高，列出每筆項目
            var mainFoods = items.Where(x => discountData.food.mainFood.Contains(x.itemName))
                                 .SelectMany(x => Enumerable.Repeat(x, x.quantity))  
                                 .OrderBy(x => x.price)
                                 .ToList();

            // 找出飲品類項目，按照價格由低排到高，列出每筆項目
            var drinks = items.Where(x => discountData.food.drink.Contains(x.itemName))
                     .SelectMany(x => Enumerable.Repeat(x, x.quantity)) 
                     .OrderBy(x => x.price)
                     .ToList();

            // 進行配對
            int pairCount = Math.Min(mainFoods.Count, drinks.Count);
            int itemsProcessed = 0;


            while (itemsProcessed < pairCount)
            {
                var currentMainFood = mainFoods.Skip(itemsProcessed).FirstOrDefault();
                var currentDrink = drinks.Skip(itemsProcessed).FirstOrDefault();

                if (currentMainFood != null && currentDrink != null)
                {
                    int originalMatchPrice = currentMainFood.price + currentDrink.price;
                    int priceDifference = discountPrice - originalMatchPrice;

                    // 生成配對名稱
                    var combinedName = currentMainFood.itemName + "配" + currentDrink.itemName + " $" + priceDifference;

                    // 生成新的項目
                    var mainFoodWithDrink = new Item("(折扣優惠價80元)" + combinedName, 1);
                    items.Add(mainFoodWithDrink);

                    itemsProcessed++;  // 更新已處理項目，用來skip
                }
                else
                {
                    // 若其中一個為空，無法配對，則跳出循環
                    break;
                }
            }
        }


        //public override void DiscountPolicyInterface()
        //{
        //var discountPrice = discountData.discount.discountPrice;

        //// 计算每种主食的总数量
        //var mainFoodCounts = items.Where(x => discountData.food.mainFood.Contains(x.itemName))
        //                          .GroupBy(x => x.itemName)
        //                          .Select(g => new { ItemName = g.Key, TotalCount = g.Sum(x => x.quantity), LowestPrice = g.Min(x => x.price) })
        //                          .ToList();

        //// 计算每种饮料的总数量
        //var drinkCounts = items.Where(x => discountData.food.drink.Contains(x.itemName))
        //                       .GroupBy(x => x.itemName)
        //                       .Select(g => new { ItemName = g.Key, TotalCount = g.Sum(x => x.quantity), LowestPrice = g.Min(x => x.price) })
        //                       .ToList();







        //    foreach (var mainFood in mainFoodCounts)
        //    {
        //        foreach (var drink in drinkCounts)
        //        {
        //            int pairCount = Math.Min(mainFood.TotalCount, drink.TotalCount);

        //            if (pairCount > 0)  // 有可配对的数量
        //            {
        //                int originalMatchPrice = mainFood.LowestPrice + drink.LowestPrice;
        //                int priceDifference = discountPrice - originalMatchPrice;

        //                // 生成配对名称
        //                var combinedName = mainFood.ItemName + "配" + drink.ItemName + " $" + priceDifference;

        //                // 生成新的项目表示配对
        //                var mainFoodWithDrink = new Item("(折扣优惠价80元)" + combinedName, pairCount);
        //                items.Add(mainFoodWithDrink);
        //            }
        //        }
        //    }
        //}





        //public override void DiscountPolicyInterface()
        //{
        //    var discountPrice = discountData.discount.discountPrice;

        //    var mainFood = items.Where(x => discountData.food.mainFood.Contains(x.itemName))
        //                        .OrderBy(x => x.price)
        //                        .ToList();

        //    var mainFoodName = mainFood.Select(x => x.itemName).ToList();

        //    int mainFoodTotal = mainFood.Sum(x => x.quantity);

        //    var drink = items.Where(x => discountData.food.drink.Contains(x.itemName))
        //                     .OrderBy(x => x.price)
        //                     .ToList();

        //    var drinkName = drink.Select(x => x.itemName).ToList();

        //    int drinkTotal = drink.Sum(x => x.quantity);

        //    int matchPair = Math.Min(mainFoodTotal, drinkTotal);

        //    1.先找出是否有主餐和飲料的區塊，有進行配對
        //    1.1 有一組的就新增一欄
        //    1.2 沒一組skip加到另一個list

        //    for (int i = 0; i < matchPair; i++)
        //    {
        //        int originalMatchPrice = mainFood[i].price + drink[i].price;
        //        int priceDifference = discountPrice - originalMatchPrice;

        //        生成配对名称
        //       var combinedName = mainFood[i].itemName + "配" + drink[i].itemName + " $" + priceDifference * matchPair;

        //        生成新的项目表示配对
        //       var mainFoodWithDrink = new Item("(折扣優惠價80元)" + combinedName, matchPair);
        //        items.Add(mainFoodWithDrink);
        //    }

        //}
    }
}
