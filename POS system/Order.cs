using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace POS_system
{

    internal class Order
    {
        List<Item> items = new List<Item>();
        List<Item> originalItemsWithoutDiscount = new List<Item>();
        public event EventHandler<FlowLayoutPanel> orderChanged;
        //public event EventHandler<FlowLayoutPanel> discountStrategyChanged;

        public void PushItem(Item item,string option)//自定義Item類
        {
            // 新增:名稱數量不存在原本的list
            // 修改:用name搜尋是否有該筆資料，若有item的數量修改成最新的數量
            // 刪除:數量歸零
            //originalItemsWithoutDiscount.Clear();

            //foreach (var originalItem in items)
            //{
            //    originalItemsWithoutDiscount.Add(originalItem);
            //}

            var anItem = items.FirstOrDefault(x => x.itemName == item.itemName);
            if (anItem == null)//新增
            {
                items.Add(item);
                return;
            }

            if (anItem.quantity == 0)//刪除
            {
                items.Remove(item);
                //return;
            }
            anItem.quantity = item.quantity;//修改，顯示會刷新數量
            anItem.totalPrice =item.totalPrice;//修改

            //DiscountItemPrice
            new Discount(items,option,orderChanged).DiscountItemPrice();
        }

        public void ChangeStrategy(string option)
        {
            new Discount(items, option, orderChanged).DiscountItemPrice();

        }

        public int GetTotalPrice() 
        {
            int totalPrice = 0;
            foreach (var item in items) 
            {
                totalPrice += item.totalPrice;
            }
            return totalPrice;
        }
    }
}
