using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_system.Strategies.Items
{
    internal class MainFoodWithEgg : DiscountStrategy
    {
        private FoodDataModel.Discount discountData;

        public MainFoodWithEgg(List<Item> items, FoodDataModel.Discount discountData) : base(items)
        {
            this.discountData = discountData;
        }

        public override void DiscountPolicyInterface()
        {
            var discountPrice = discountData.discount.discountPrice;

            var mainFoods = items.Where(x => discountData.food.mainFood.Contains(x.itemName))
                                 .SelectMany(x => Enumerable.Repeat(x, x.quantity))  
                                 .OrderBy(x => x.price)
                                 .ToList();

            var eggs = items.Where(x => discountData.food.subFood.Contains(x.itemName))
                            .SelectMany(x => Enumerable.Repeat(x, x.quantity))  
                            .OrderBy(x => x.price)
                            .ToList();

            int pairCount = Math.Min(mainFoods.Count, eggs.Count);
            int itemsProcessed = 0;

            while (itemsProcessed < pairCount)
            {
                var currentMainFood = mainFoods.Skip(itemsProcessed).FirstOrDefault();
                var currentDrink = eggs.Skip(itemsProcessed).FirstOrDefault();

                if (currentMainFood != null && currentDrink != null)
                {
                    int originalMatchPrice = currentMainFood.price + currentDrink.price;
                    int priceDifference = discountPrice - originalMatchPrice;

                    var combinedName = currentMainFood.itemName + "配" + currentDrink.itemName + " $" + priceDifference;

                    var mainFoodWithDrink = new Item("(折扣優惠價60元)" + combinedName, 1);
                    items.Add(mainFoodWithDrink);

                    itemsProcessed++;
                }
                else
                {                    
                    break;
                }
            }
        }
    }
}
