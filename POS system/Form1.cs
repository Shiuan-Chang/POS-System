using Newtonsoft.Json;
using POS_system;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_system
{
    public partial class Form1 : Form
    {
        Order order = new Order();
        FoodService service = null;
        public Form1()
        {
            InitializeComponent();
            InitPages();
        }


        //動態生成每個layout中的checkbox
        private void InitPages()
        {
            service = new FoodService(CheckChange,ValueChange);
            service.InitPanelList(foodPanels);
            order.orderChanged += UpdateFlowLayoutPanel5;//事件的註冊
            //order.discountStrategyChanged += UpdateFlowLayoutPanel5;
            comboBox1.DataSource = GenerateDiscounts(service.foodModel);
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
        }

        private List<KeyValueModel> GenerateDiscounts(FoodDataModel dataModel)
        {
            List<KeyValueModel> list = new List<KeyValueModel>();
            foreach (var data in dataModel.discount)
            {
                list.Add(new KeyValueModel(data.key, data.value));
            }
            return list;
        }

        private void CheckChange(object sender,EventArgs e)
        {
            CheckBox checkBoxItem = (CheckBox)sender;
            FlowLayoutPanel parentPanel = (FlowLayoutPanel)checkBoxItem.Parent;
            NumericUpDown relatedNumBox = (NumericUpDown)parentPanel.Controls[1];
            if (checkBoxItem.Checked)            
                relatedNumBox.Value = 1;
            else
                relatedNumBox.Value = 0;
            Item item = new Item(checkBoxItem.Text, (int)relatedNumBox.Value);
            order.PushItem(item,comboBox1.SelectedValue.ToString());
        }

        private void ValueChange(object sender,EventArgs e)
        {
            NumericUpDown numBox = (NumericUpDown)sender;//就是調數量的那個鈕
            FlowLayoutPanel parentPanel = (FlowLayoutPanel)numBox.Parent;
            CheckBox relatedCheckBox = (CheckBox)parentPanel.Controls[0];
            if((int)numBox.Value >= 1)
                relatedCheckBox.Checked = true;
            else
                relatedCheckBox.Checked = false;
            Item item = new Item(relatedCheckBox.Text,(int)numBox.Value);
            order.PushItem(item, comboBox1.SelectedValue.ToString());
        }

        private void button1_Click(object sender, EventArgs e)//sneder是觸發事件的因子，比方說一個按鈕；這裡的e是一般的EventArgs.Empty
        {

            int itemTotalPrice = order.GetTotalPrice();
         
            label2.Text = itemTotalPrice.ToString();
        }

        //若資訊有異動，重新生成totallist
        private void UpdateFlowLayoutPanel5(object sender, FlowLayoutPanel e)
        {
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(e);
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //order.UpdateNewDiscountStrategy(comboBox1.SelectedValue.ToString());
            if (comboBox1.SelectedValue is string)
                order.ChangeStrategy(comboBox1.SelectedValue.ToString());
        }

        private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
