using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WarehouseSystem
{
    public partial class transferProduct : Form
    {
        WareHouseSystemEntities ent;
        public transferProduct()
        {
            InitializeComponent();
            ent = new WareHouseSystemEntities();
            var warhouses = from d in ent.WareHouses select d;
            foreach ( var w in warhouses )
            {
                comboBox1.Items.Add( w.ID );
                comboBox2.Items.Add( w.ID );
            }
        }

        private void transferBtn_Click(object sender, EventArgs e)
        {
            int warehouseIDOld = int.Parse(comboBox1.SelectedItem.ToString());
            int warehouseIDNew = int.Parse(comboBox2.SelectedItem.ToString());
            int productCode = int.Parse(comboBox3.SelectedItem.ToString());
            int SupplierID = int.Parse(comboBox4.SelectedItem.ToString());
            int QuantityOld = int.Parse(comboBox5.SelectedItem.ToString());
            int QuantityNew = int.Parse(textBox2.Text.ToString());
            if (QuantityNew <= QuantityOld)
            {
                Product_Entry product_Entry = ent.Product_Entry.Where(d => d.Warehouse_ID == warehouseIDOld && d.Product_Code == productCode && d.Supplier_ID == SupplierID && d.Quantity == QuantityOld).FirstOrDefault();
                Product_Warehouse wareHouseold = ent.Product_Warehouse.Where(d => d.WareHouse_ID == warehouseIDOld&&d.Product_Code==productCode).FirstOrDefault();
                Product_Warehouse wareHousenew = ent.Product_Warehouse.Where(d => d.WareHouse_ID == warehouseIDNew&& d.Product_Code == productCode).FirstOrDefault();
                if (wareHousenew == null)
                {
                    wareHousenew = new Product_Warehouse();
                    wareHousenew.Unit = wareHouseold.Unit;
                    wareHousenew.WareHouse_ID = warehouseIDNew;
                    wareHousenew.Product_Code = productCode;
                    wareHousenew.Quantity = QuantityNew;
                    ent.Product_Warehouse.Add(wareHousenew);
                    ent.SaveChanges(); 
                }
                else
                wareHousenew.Quantity += QuantityNew;
                product_Entry.Quantity -= QuantityNew;
                wareHouseold.Quantity -= QuantityNew;
                Product_Entry product_EntryNew = new Product_Entry();
                product_EntryNew.ID = product_Entry.ID;
                product_EntryNew.Supplier_ID = product_Entry.Supplier_ID;
                product_EntryNew.Date= DateTime.Now;
                product_EntryNew.ProductionDate= product_Entry.ProductionDate;
                product_EntryNew.EXPDate=product_Entry.EXPDate;
                product_EntryNew.Warehouse_ID = warehouseIDNew;
                product_EntryNew.Product_Code = productCode;
                product_EntryNew.Quantity = QuantityNew;
                ent.Product_Entry.Add(product_EntryNew);
                ent.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("لا يوجد هذه الكمية من الصنف المختار ");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int warehouseID = int.Parse(comboBox1.SelectedItem.ToString());
            WareHouse wareHouse = ent.WareHouses.Where(d=>d.ID== warehouseID).FirstOrDefault();
            label3.Text= wareHouse.Name;
            comboBox3.Items.Clear();
            foreach(Product_Warehouse product in wareHouse.Product_Warehouse) 
            {
                comboBox3.Items.Add(product.Product_Code);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int warehouseID = int.Parse(comboBox1.SelectedItem.ToString());
            int productCode = int.Parse(comboBox3.SelectedItem.ToString());
            comboBox4.Items.Clear();
            Product_Warehouse PW = ent.Product_Warehouse.Where(d=>d.WareHouse_ID== warehouseID&&d.Product_Code== productCode).FirstOrDefault();
            label11.Text = PW.Product.Name;
            label11.Visible= true; 
            var PE = ent.Product_Entry.Where(d => d.Warehouse_ID == warehouseID && d.Product_Code == productCode);
            foreach(Product_Entry item in PE) 
            {
                comboBox4.Items.Add(item.Supplier_ID);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int warehouseID = int.Parse(comboBox1.SelectedItem.ToString());
            int productCode = int.Parse(comboBox3.SelectedItem.ToString());
            int SupplierID = int.Parse(comboBox4.SelectedItem.ToString());
            var PE = ent.Product_Entry.Where(d => d.Warehouse_ID == warehouseID && d.Product_Code == productCode&&d.Supplier_ID==SupplierID).Select(d=>d);
            comboBox5.Items.Clear();
            foreach (Product_Entry item in PE)
            {
                comboBox5.Items.Add(item.Quantity);
            }

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int warehouseID = int.Parse(comboBox1.SelectedItem.ToString());
            int productCode = int.Parse(comboBox3.SelectedItem.ToString());
            int SupplierID = int.Parse(comboBox4.SelectedItem.ToString());
            int Quantity = int.Parse(comboBox5.SelectedItem.ToString());
            var PE = ent.Product_Entry.Where(d => d.Warehouse_ID == warehouseID && d.Product_Code == productCode && d.Supplier_ID == SupplierID&&d.Quantity==Quantity).Select(d => d).FirstOrDefault();

            DateTime datePRD = (DateTime)PE.ProductionDate;
            textBox5.Text = datePRD.Year.ToString();
            textBox4.Text = datePRD.Month.ToString();
            textBox3.Text = datePRD.Day.ToString();
            DateTime dateEXP1 = (DateTime)PE.EXPDate;
            textBox6.Text = dateEXP1.Year.ToString();
            textBox7.Text = dateEXP1.Month.ToString();
            textBox8.Text = dateEXP1.Day.ToString();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int warehouseID = int.Parse(comboBox2.SelectedItem.ToString());
            WareHouse wareHouse = ent.WareHouses.Where(d => d.ID == warehouseID).FirstOrDefault();
            label4.Text = wareHouse.Name;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
