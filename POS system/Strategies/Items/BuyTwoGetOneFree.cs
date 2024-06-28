using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_system.Strategies.Items
{
    internal class BuyTwoGetOneFree : DiscountStrategy
    {
        private FoodDataModel.Discount discountData;

        public BuyTwoGetOneFree(List<Item> items, FoodDataModel.Discount discountData) : base(items)
        {
            this.discountData = discountData;
        }

        public override void DiscountPolicyInterface()
        {
            int buyCount = discountData.giftnumber.buyCount;
            int giftCount = discountData.giftnumber.giftCount;
            int giftNumber = 0;

            var result = items.Where(x => discountData.food.mainFood.Contains(x.itemName) && x.quantity/ buyCount > 0)
                .Select(x => new
                {
                    Name = x.itemName,
                    Count = (x.quantity / buyCount) * giftCount
                }).ToList();
                result.ForEach(x => items.Add(new Item($"(贈送){x.Name}$0", x.Count)));



            ////把相同商品的名稱做好分類
            //var groupedItems = items.GroupBy(x => x.itemName);


            ////計算每個分類的數量
            //var itemCounts = new List<Item>();
            //foreach (var group in groupedItems)
            //{
            //    string itemName = group.Key;
            //    int totalQuantity = group.Sum(x => x.quantity);               
            //    int price = group.First().price;
            //    string itemNotes = group.First().itemNotes;

            //    itemCounts.Add(Item.CreateItem(itemName, totalQuantity, price, itemNotes));
            //}

            ////根據每個數量去計算要送的贈品
            //int buyCount = discountData.
            //foreach (var item in itemCounts)
            //{
            //    if (item.quantity >= buyCount)
            //    {
            //        int giftedItemCount = (item.quantity / buyCount) * giftCount;
            //        if (giftedItemCount > 0)
            //        {
            //            // 檢查是否已經存在相同名稱的贈品項目
            //            var existingGiftItem = giftItems.FirstOrDefault(x => x.itemName == $"(贈送){item.itemName}");
            //            if (existingGiftItem != null)
            //            {
            //                // 更新已存在的贈品項目數量
            //                existingGiftItem.quantity = giftedItemCount;
            //            }
            //            else
            //            {
            //                // 創建新的贈品項目並添加到giftItems
            //                Item giftItem = Item.CreateItem($"(贈送){item.itemName}", giftedItemCount, 0, item.itemNotes);
            //                giftItems.Add(giftItem);
            //            }
            //        }
            //    }

            //    // 清空 items 列表並添加所有原有項目
            //    var updatedItems = new List<Item>(items);

            //    // 添加所有贈送品項目到原有項目列表中
            //    updatedItems.AddRange(giftItems);

            //    // 更新 items 列表
            //    items.Clear();
            //    items.AddRange(updatedItems);

        }

        }
     }
    

