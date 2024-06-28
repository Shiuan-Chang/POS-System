using Newtonsoft.Json;
using POS_system.Strategies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace POS_system
{
    internal class Discount
    {
        private FoodDataModel _discountData;

        string option {  get; set; }
        //List<Item>是勾選的商品
        List<Item> items { get; set; }
        public EventHandler<FlowLayoutPanel> EventHandler { get; set; }

        //建構式
        public Discount(List<Item> list,string option, EventHandler<FlowLayoutPanel> eventHandler) {
            //每次都重新生成list
            this.items = ReSetList(list);  
            this.option = option;
            this.EventHandler = eventHandler;
        }

        public List<Item> ReSetList(List<Item>  list)
        {
            list.RemoveAll(x => x.itemName.Contains("贈送") || x.itemName.Contains("折"));

            return list;
        }

        // 解析JSON文件
        public FoodDataModel ParseJsonData()
        {
            // 序列化 => 將物件轉變為字串
            // 反序列化 => 將字串轉變為物件
            string filePath = ConfigurationManager.AppSettings["foods"];
            string data = File.ReadAllText(filePath);
            _discountData = JsonConvert.DeserializeObject<FoodDataModel>(data);
            return _discountData;
        }


        public void DiscountItemPrice()
        {
            if (_discountData == null)
            {
                ParseJsonData();
            }
            var discountInfo = _discountData.discount.FirstOrDefault(x => x.value == option);


            DiscountContext policies = new DiscountContext(option, items, discountInfo);

            policies.ApplyDiscount();

            new ShowPanel().RefreshLayout(items, EventHandler);
        }
    }
}
