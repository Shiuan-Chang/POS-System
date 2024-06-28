using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static POS_system.FoodDataModel;

namespace POS_system
{
    internal class FoodService
    {

        List<FlowLayoutPanel> foodTypePanels = new List<FlowLayoutPanel>();
        EventHandler CheckChange;
        EventHandler ValueChange;
        private FoodDataModel _foodData;
        public FoodDataModel foodModel
        {
            get
            {
                return _foodData;
            }
        }
        public FoodService(EventHandler CheckChange, EventHandler ValueChange)
        {
            this.CheckChange = CheckChange;
            this.ValueChange = ValueChange;

        }
        // 得到food資料包
        private FoodDataModel InitFoods()
        {
            string filePath = ConfigurationManager.AppSettings["foods"];
            string data = File.ReadAllText(filePath);
            // 序列化 => 將物件轉變為字串
            // 反序列化 => 將字串轉變為物件
            _foodData = JsonConvert.DeserializeObject<FoodDataModel>(data);
            return _foodData;
        }
        // 生成菜單畫面
        public void InitPanelList(FlowLayoutPanel foodPanels)
        {
            FoodDataModel foodData = InitFoods();
            GenerateFoodPanel(foodData.foods, foodPanels);
            GenerateFoods(foodData.foods);
        }

        // 每區食物的panel
        private void GenerateFoodPanel(Food[] foods, FlowLayoutPanel foodPanels)
        {
            for (int i = 0; i < foods.Length; i++)
            {
                FlowLayoutPanel aTypeofFoodPanel = new FlowLayoutPanel();
                aTypeofFoodPanel.Width = 180;
                aTypeofFoodPanel.Height = 200;
                Label foodType = new Label();
                foodType.Text = foods[i].type;
                foodType.Width = 180;
                FlowLayoutPanel foodItems = new FlowLayoutPanel();
                foodItems.Height = 200;
                aTypeofFoodPanel.Controls.Add(foodType);
                aTypeofFoodPanel.Controls.Add(foodItems);
                foodTypePanels.Add(foodItems);

                foodPanels.Controls.Add(aTypeofFoodPanel);

            }
        }

       // 生成選項
        private void GenerateFoods(Food[] foods)
        {
            for (int i = 0; i < foods.Length; i++)
            {
                foodTypePanels[i].SetDatas(foods[i].items, CheckChange, ValueChange);
            }
        }

        // 
    }
}
