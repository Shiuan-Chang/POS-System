using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS_system.FoodDataModel;

namespace POS_system.Strategies.Items
{
    internal class TwoMainFoodWithMochi:DiscountStrategy
    {
        //discountData已經透過discount初始化並找到對應方案的資料，故在這邊無須判斷有沒有對應到相對方案
        private FoodDataModel.Discount discountData;

        public TwoMainFoodWithMochi(List<Item> items, FoodDataModel.Discount discountData) : base(items)
        {
            this.discountData = discountData;
        }
        public override void DiscountPolicyInterface() 
        { 
            int buyCount = discountData.giftnumber.buyCount;
            int giftCount= discountData.giftnumber.giftCount;
            int giftNumber = 0;
            
            int total = items.Where(x => discountData.food.mainFood.Contains(x.itemName) && x.quantity / buyCount > 0)
                             .Sum(x => (x.quantity / buyCount) * giftCount);
            Item giftedMochi = new Item("(贈送)麻糬$0", total);
            //每次list都會刷新，所以不用擔心重複加入的問題
            items.Add(giftedMochi);
        }



    }
}
//1.檢查點選的選項(list)是否為主餐
//foreach (Item item in items) 
//{
//    if (discountData.food.mainFood.Contains(item.itemName)) 
//    {
//        if(item.quantity >= buyCount) 
//        {
//            giftNumber += item.quantity / buyCount * giftCount;
//        }
//    }            
//}
//Item giftedMochi = new Item("(贈送)麻糬$0", giftNumber);
//items.Add(giftedMochi);

// 寫法1: 將符合的條件抓出來之後，跑迴圈去動態加入到items中
//var result = items.Where(x => discountData.food.mainFood.Contains(x.itemName) && x.quantity / buyCount > 0)
//     .Select(x => new
//     {
//         Name = x.itemName,
//         Count = (x.quantity / buyCount)* giftCount
//     }).ToList();

//result.ForEach(x=> items.Add(new Item("(贈送)麻糬$0", x.Count)));→會變成每筆去跑一次，這邊不適用

//寫法2: 將符合的條件抓出來之後，透過sum函數 統一計算數量加總
//int total = items.Where(x => discountData.food.mainFood.Contains(x.itemName) && x.quantity / buyCount > 0)
//                 .Select(x => new→創建符合需求的欄位
//                 {
//                     Name = x.itemName,
//                     Count = (x.quantity / buyCount) * giftCount
//                 })
//                .Sum(x => x.Count);
//Item giftedMochi = new Item("(贈送)麻糬$0", total);
//items.Add(giftedMochi);

// 寫法3: 不需要select將資料挑出來，直接進行sum轉換
// 直接設定在mainfood尋找，就能限定在mainfood