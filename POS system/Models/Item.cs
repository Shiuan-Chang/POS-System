using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace POS_system
{
    //把拿到的資料作處理，取出需要的資訊
    public class Item
    {
        //這邊定義的變數，是Item類自己要處理的，不是從外部傳進來的數據。get是讀取，set是寫入，拿到一包資料讀取並寫入
        public string itemName { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int totalPrice { get; set; }
        public string itemNotes { get; set; }//備註

        public bool IsDiscountApplied = false; 

        public Item(string name, int chosenQuantity)//Item建構式
        {
            itemName = name.Split('$')[0];
            price = int.Parse(name.Split('$')[1]);
            quantity = chosenQuantity;
            totalPrice = price * quantity;
        }

        public Item() { }

    }
}
