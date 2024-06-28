using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace POS_system
{
    public class ShowPanel
    {//右方資訊欄的顯示

        //資訊欄的表頭
        private FlowLayoutPanel AddTotalListHeader()
        {
            FlowLayoutPanel headerPanel = new FlowLayoutPanel();
            headerPanel.Width = 900;
            headerPanel.Height = 30;
            Label label3 = new Label();
            label3.Width = 220;
            label3.Text = "品名";
            Label label4 = new Label();
            label4.Width = 40;
            label4.Text = "單價";
            Label label5 = new Label();
            label5.Width = 40;
            label5.Text = "數量";
            Label label6 = new Label();
            label6.Width = 40;
            label6.Text = "小計";
            Label label7 = new Label();
            label7.Width = 250;
            label7.Text = "備註";
            headerPanel.Controls.Add(label3);
            headerPanel.Controls.Add(label4);
            headerPanel.Controls.Add(label5);
            headerPanel.Controls.Add(label6);
            headerPanel.Controls.Add(label7);
            return headerPanel;
        }

        //資訊欄的內容，大包小
        public void RefreshLayout(List<Item> items, EventHandler<FlowLayoutPanel> e) 
        {
            FlowLayoutPanel biggerPanel = new FlowLayoutPanel();
            biggerPanel.Width = 900;
            biggerPanel.Height = 500;
            biggerPanel.Controls.Add(AddTotalListHeader());
            
            foreach (Item item in items.Where(i => i.quantity > 0))
            {
                FlowLayoutPanel minorPanel = new FlowLayoutPanel();
                minorPanel.Width = 900;
                minorPanel.Height = 30;
                string name = item.itemName;
                int price = item.price;
                int quantity = item.quantity;
                int totalPrice = item.totalPrice;
                string note = item.itemNotes;

                Label nameLabel = new Label();
                nameLabel.Width = 220;
                nameLabel.Text = name;

                Label priceLabel = new Label();
                priceLabel.Width = 40;
                priceLabel.Text = price.ToString();

                Label quantityLabel = new Label();
                quantityLabel.Width = 40;
                quantityLabel.Text = quantity.ToString();

                Label subtotalLabel = new Label();
                subtotalLabel.Width = 40;
                subtotalLabel.Text = totalPrice.ToString();

                Label noteLabel = new Label();
                noteLabel.Width = 250;
                noteLabel.Height = 30;
                noteLabel.Text = note;
                minorPanel.Controls.Add(nameLabel);
                minorPanel.Controls.Add(priceLabel);
                minorPanel.Controls.Add(quantityLabel);
                minorPanel.Controls.Add(subtotalLabel);
                minorPanel.Controls.Add(noteLabel);
                biggerPanel.Controls.Add(minorPanel);
            }
            e.Invoke(this, biggerPanel);
        }
    }
}
