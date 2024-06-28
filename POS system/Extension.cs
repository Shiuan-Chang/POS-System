using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_system
{
    internal static class Extension
    {

        //後端的運算、資料處理
        // 取得數字中的金額
        public static int GetPrice(this string item)//this是擴展的使用，在原有物件使用該方法(必須是靜態)
        {
            return int.Parse(item.Split('$')[1]);
        }

        //偏前端
        //左方選單：傳入陣列，擴充在flowLayoutPanel1-4
        public static void SetDatas(this FlowLayoutPanel itemLists, string[] items, EventHandler checkChange, EventHandler valueChange) //擴充，第一個參數一定是設定this參數，而且只能是靜態方法
        {
            foreach (string item in items)
            {
                FlowLayoutPanel group = new FlowLayoutPanel();
                group.Height = 30;
                CheckBox checkBox = new CheckBox();
                checkBox.CheckedChanged += checkChange;//自定義事件：註冊checkChange事件
                NumericUpDown numBox = new NumericUpDown();
                numBox.ValueChanged += valueChange;//這邊的+=是事件綁定，不是計算
                numBox.Width = 50;
                numBox.Height = 20;
                numBox.Minimum = 0;
                numBox.Maximum = 100;
                checkBox.Text = item;
                group.Controls.Add(checkBox);//用control.add增加checkbox
                group.Controls.Add(numBox);
                itemLists.Controls.Add(group);
            }
        }
    }
}
