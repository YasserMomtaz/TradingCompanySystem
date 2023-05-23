using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WarehouseSystem
{
    public partial class Form1 : Form
    {
         public WareHouseSystemEntities ent;
        public Form1()
        {
            InitializeComponent();
            ent = new WareHouseSystemEntities();
            var war = from w in ent.WareHouses select w;
            var products = from p in ent.Products select p;
            foreach (var product in products)
            {
                comboBox2.Items.Add(product.Code);
                comboBox6.Items.Add(product.Code);
                comboBox20.Items.Add(product.Code);
            }
            foreach (WareHouse wareHouse in war)
            {
                listBox1.Items.Add(wareHouse.ID);
                listBox2.Items.Add(wareHouse.Name);
                comboBox1.Items.Add(wareHouse.ID);
                comboBox5.Items.Add(wareHouse.ID);
                comboBox18.Items.Add(wareHouse.ID);
                comboBox19.Items.Add(wareHouse.ID);
                listBox7.Items.Add(wareHouse.ID);
                listBox14.Items.Add(wareHouse.ID);
            }
            RefreshSuppliers();
            RefreshClients();
            RefreshProductEntry();
            RefreshProductRelease();
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            var index = ((ListBox)sender).SelectedIndices;

            if (index != null)
            {

                foreach (var item in index)
                {

                    listBox1.SelectedIndex = int.Parse(item.ToString());
                    listBox2.SelectedIndex = int.Parse(item.ToString());
                }

            }
        }

        private void DetailsBtn_Click(object sender, EventArgs e)
        {
            listBox6.Items.Clear();
            listBox5.Items.Clear();
            listBox4.Items.Clear();
            listBox3.Items.Clear();
            var selected = listBox1.SelectedItems;
            if (selected != null)
            {
                foreach (var index in selected)
                {
                    int ID = int.Parse(index.ToString());
                    var items = ent.Product_Warehouse.Where(d => d.WareHouse_ID == ID).Select(d => d);
                    foreach (Product_Warehouse item in items)
                    {
                        listBox4.Items.Add(item.Product.Name);
                        listBox3.Items.Add(item.Quantity);
                        listBox6.Items.Add(item.Unit);
                        listBox5.Items.Add(item.WareHouse.Name);
                    }
                }
            }
        }
        public void Refresh1()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            comboBox1.Items.Clear();
            comboBox5.Items.Clear();
            comboBox18.Items.Clear();
            comboBox19.Items.Clear();
            listBox14.Items.Clear();
            var war = from w in ent.WareHouses select w;
            foreach (WareHouse wareHouse in war)
            {
                listBox1.Items.Add(wareHouse.ID);
                listBox2.Items.Add(wareHouse.Name);
                comboBox1.Items.Add(wareHouse.ID);
                comboBox5.Items.Add(wareHouse.ID);
                comboBox18.Items.Add(wareHouse.ID);
                comboBox19.Items.Add(wareHouse.ID);
                listBox14.Items.Add(wareHouse.ID);
            }
        }

        private void addWareBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(textBox1.Text);
            var exist = ent.WareHouses.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            if (exist == null)
            {
                WareHouse addwarehouse = new WareHouse();
                addwarehouse.ID = ID;
                addwarehouse.Name = textBox2.Text;
                addwarehouse.Address = textBox3.Text;
                addwarehouse.Manger = textBox4.Text;
                ent.WareHouses.Add(addwarehouse);
                ent.SaveChanges();
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
                Refresh1();
            }
            else { MessageBox.Show("هذا الرقم التعريفي موجود بالفعل برجاء ادخال رقم تعريفي جديد"); }
        }

        private void UpdWareBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox1.SelectedItem.ToString());
            WareHouse update = ent.WareHouses.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            update.Name = textBox7.Text;
            update.Address = textBox6.Text;
            update.Manger = textBox5.Text;
            ent.SaveChanges();
            Refresh1();
            textBox7.Text = textBox6.Text = textBox5.Text = "";
        }


        private void AddPrdctBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(textBox11.Text);
            var exist = ent.Products.Where(j => j.Code == ID).Select(d => d).FirstOrDefault();
            if (exist == null)
            {
                Product addproduct = new Product();
                addproduct.Code = ID;
                addproduct.Name = textBox10.Text;
                addproduct.Unit = textBox9.Text;
                ent.Products.Add(addproduct);
                ent.SaveChanges();
                textBox11.Text = textBox10.Text = textBox9.Text = "";
                Refresh1();
            }
            else { MessageBox.Show("هذا الكود موجود بالفعل برجاء ادخال كود جديد"); }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox2.SelectedItem.ToString());
            Product update = ent.Products.Where(j => j.Code == ID).Select(d => d).FirstOrDefault();
            textBox12.Text = update.Name;
            textBox8.Text = update.Unit;
        }

        private void UpdPrdctBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox2.SelectedItem.ToString());
            Product update = ent.Products.Where(j => j.Code == ID).Select(d => d).FirstOrDefault();
            update.Name = textBox12.Text;
            update.Unit = textBox8.Text;
            ent.SaveChanges();
            RefreshProducts();
            textBox12.Text = textBox8.Text = "";
        }
        public void RefreshProducts()
        {
            comboBox2.Items.Clear();
            var products = from p in ent.Products select p;
            foreach (var product in products)
            {
                comboBox2.Items.Add(product.Code);
                comboBox6.Items.Add(product.Code);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox1.SelectedItem.ToString());
            WareHouse update = ent.WareHouses.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            textBox7.Text = update.Name;
            textBox6.Text = update.Address;
            textBox5.Text = update.Manger;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(textBox19.Text);
            var exist = ent.Suppliers.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            if (exist == null)
            {
                Supplier addSupplier = new Supplier();
                addSupplier.ID = ID;
                addSupplier.Name = textBox18.Text;
                addSupplier.Phone = textBox17.Text;
                addSupplier.Fax = textBox16.Text;
                addSupplier.Email = textBox21.Text;
                addSupplier.Location = textBox20.Text;
                ent.Suppliers.Add(addSupplier);
                ent.SaveChanges();
                textBox19.Text = textBox18.Text = textBox17.Text = textBox16.Text = textBox21.Text = textBox20.Text = "";
                RefreshSuppliers();
            }
            else { MessageBox.Show("هذا الرقم التعريفي موجود بالفعل برجاء ادخال رقم تعريفي جديد"); }
        }
        public void RefreshSuppliers()
        {
            comboBox3.Items.Clear();
            var suppliers = from p in ent.Suppliers select p;
            foreach (var supplier in suppliers)
            {
                comboBox3.Items.Add(supplier.ID);
                comboBox7.Items.Add(supplier.ID);
                comboBox8.Items.Add(supplier.ID);
            }
        }

        private void UpdSplBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox3.SelectedItem.ToString());
            Supplier update = ent.Suppliers.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            update.Name = textBox23.Text;
            update.Phone = textBox22.Text;
            update.Fax = textBox15.Text;
            update.Email = textBox13.Text;
            update.Location = textBox14.Text;
            ent.SaveChanges();
            RefreshSuppliers();
            textBox23.Text = textBox22.Text = textBox15.Text = textBox13.Text = textBox14.Text = "";

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox3.SelectedItem.ToString());
            Supplier update = ent.Suppliers.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            textBox23.Text = update.Name;
            textBox22.Text = update.Phone;
            textBox15.Text = update.Fax;
            textBox13.Text = update.Email;
            textBox14.Text = update.Location;
        }

        private void AddClntBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(textBox34.Text);
            var exist = ent.Clients.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            if (exist == null)
            {
                Client addClient = new Client();
                addClient.ID = ID;
                addClient.Name = textBox33.Text;
                addClient.Phone = textBox32.Text;
                addClient.Fax = textBox31.Text;
                addClient.Email = textBox29.Text;
                addClient.Location = textBox30.Text;
                ent.Clients.Add(addClient);
                ent.SaveChanges();
                textBox33.Text = textBox32.Text = textBox31.Text = textBox29.Text = textBox34.Text = textBox30.Text = "";
                RefreshClients();
            }
            else { MessageBox.Show("هذا الرقم التعريفي موجود بالفعل برجاء ادخال رقم تعريفي جديد"); }
        }
        public void RefreshClients()
        {
            comboBox4.Items.Clear();
            var clients = from p in ent.Clients select p;
            foreach (var client in clients)
            {
                comboBox4.Items.Add(client.ID);
                comboBox16.Items.Add(client.ID);
                comboBox13.Items.Add(client.ID);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox4.SelectedItem.ToString());
            Client update = ent.Clients.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            textBox28.Text = update.Name;
            textBox27.Text = update.Phone;
            textBox26.Text = update.Fax;
            textBox24.Text = update.Email;
            textBox25.Text = update.Location;
        }

        private void UpdCltBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox4.SelectedItem.ToString());
            Client update = ent.Clients.Where(j => j.ID == ID).Select(d => d).FirstOrDefault();
            update.Name = textBox28.Text;
            update.Phone = textBox27.Text;
            update.Fax = textBox26.Text;
            update.Email = textBox24.Text;
            update.Location = textBox25.Text;
            ent.SaveChanges();
            RefreshClients();
            textBox28.Text = textBox27.Text = textBox26.Text = textBox24.Text = textBox25.Text = "";
        }

        private void AddPEntrBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(textBox37.Text);
            Product_Entry product_Entry = new Product_Entry();
            product_Entry.ID = ID;
            product_Entry.Warehouse_ID = int.Parse(comboBox5.SelectedItem.ToString());
            product_Entry.Product_Code = int.Parse(comboBox6.SelectedItem.ToString());
            product_Entry.Supplier_ID = int.Parse(comboBox7.SelectedItem.ToString());
            product_Entry.Quantity = int.Parse(textBox38.Text);
            string date = $"{textBox45.Text}-{textBox41.Text}-{textBox36.Text}";
            string dateProduction = $"{textBox35.Text}-{textBox43.Text}-{textBox46.Text}";
            string dateEXP = $"{textBox47.Text}-{textBox48.Text}-{textBox49.Text}";
            DateTime date2;
            if (DateTime.TryParse(date, out date2) && DateTime.TryParse(dateProduction, out date2) && DateTime.TryParse(dateEXP, out date2))
            {
                product_Entry.Date = DateTime.Parse(date);
                product_Entry.ProductionDate = DateTime.Parse(dateProduction);
                product_Entry.EXPDate = DateTime.Parse(dateEXP);
                Product_Warehouse update_quantity = ent.Product_Warehouse.Where(d => d.Product_Code == product_Entry.Product_Code && d.WareHouse_ID == product_Entry.Warehouse_ID).FirstOrDefault();
                if (update_quantity == null)
                {
                    update_quantity = new Product_Warehouse();
                    update_quantity.Product_Code = product_Entry.Product_Code;
                    update_quantity.WareHouse_ID = product_Entry.Warehouse_ID;
                    update_quantity.Unit = ent.Products.Where(d => d.Code == product_Entry.Product_Code).Select(d => d.Unit).FirstOrDefault();
                    update_quantity.Quantity = 0;
                    update_quantity.Quantity += product_Entry.Quantity;
                    ent.Product_Warehouse.Add(update_quantity);
                    ent.SaveChanges();
                }
                else
                {
                    update_quantity.Quantity += product_Entry.Quantity;
                }
                ent.Product_Entry.Add(product_Entry);
                ent.SaveChanges();
                RefreshProductEntry();
                textBox37.Text = textBox36.Text = textBox38.Text = textBox45.Text = textBox41.Text = textBox35.Text = textBox43.Text = textBox46.Text = textBox47.Text = textBox48.Text = textBox49.Text = "";
            }
            else
            {
                MessageBox.Show("صيغة التاريخ غير صحيحة برجاء ادخال التاريخ بصيغة 01-01-2023");
            }

        }

        public void RefreshProductEntry()
        {
            comboBox11.Items.Clear();
            var Entry = from p in ent.Product_Entry select p;
            foreach (var item in Entry)
            {
                if (!comboBox11.Items.Contains(item.ID))
                comboBox11.Items.Add(item.ID);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox11.SelectedItem.ToString());
            int warehouse_ID = int.Parse(comboBox10.SelectedItem.ToString());
            int product_code = int.Parse(comboBox9.SelectedItem.ToString());
            Product_Entry update = ent.Product_Entry.Where(j => j.ID == ID&&j.Product_Code==product_code&&j.Warehouse_ID==warehouse_ID).Select(d => d).FirstOrDefault();
            update.Supplier_ID = int.Parse(comboBox8.SelectedItem.ToString());
            string date = $"{textBox53.Text}-{textBox54.Text}-{textBox55.Text}";
            string dateProduction = $"{textBox50.Text}-{textBox51.Text}-{textBox52.Text}";
            string dateEXP = $"{textBox39.Text}-{textBox42.Text}-{textBox44.Text}";
            DateTime date2;
            if (DateTime.TryParse(date, out date2) && DateTime.TryParse(dateProduction, out date2) && DateTime.TryParse(dateEXP, out date2))
            {
                update.Date = DateTime.Parse(date);
                update.ProductionDate = DateTime.Parse(dateProduction);
                update.EXPDate = DateTime.Parse(dateEXP);
                Product_Warehouse update_quantity = ent.Product_Warehouse.Where(d => d.Product_Code == update.Product_Code && d.WareHouse_ID == update.Warehouse_ID).FirstOrDefault();
                update_quantity.Quantity -= update.Quantity;
                update.Quantity = int.Parse(textBox40.Text);
                update_quantity.Quantity += update.Quantity;
                ent.SaveChanges();
                RefreshProductEntry();
                 textBox40.Text = textBox53.Text = textBox54.Text = textBox55.Text = textBox50.Text = textBox51.Text = textBox52.Text = textBox39.Text = textBox42.Text = textBox44.Text = "";

            }
            else
            {
                MessageBox.Show("صيغة التاريخ غير صحيحة برجاء ادخال التاريخ بصيغة 01-01-2023");
            }

        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox11.SelectedItem.ToString());
            var update = ent.Product_Entry.Where(j => j.ID == ID).Select(d => d);
            comboBox10.Items.Clear();
            foreach (Product_Entry item in update) 
            {
                if (!comboBox10.Items.Contains(item.Warehouse_ID))
                    comboBox10.Items.Add(item.Warehouse_ID);
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox11.SelectedItem.ToString());
            int warehouse_ID = int.Parse(comboBox10.SelectedItem.ToString());
            int product_code = int.Parse(comboBox9.SelectedItem.ToString());
            Product_Entry update = ent.Product_Entry.Where(j => j.ID == ID && j.Product_Code == product_code && j.Warehouse_ID == warehouse_ID).Select(d => d).FirstOrDefault();
            comboBox8.SelectedItem = update.Supplier_ID;
            DateTime date2 = update.Date.Value;
            textBox53.Text = date2.Year.ToString();
            textBox54.Text =date2.Month.ToString();
            textBox55.Text=date2.Day.ToString();
            DateTime datePRD = (DateTime)update.ProductionDate;
            textBox50.Text = datePRD.Year.ToString();
            textBox51.Text = datePRD.Month.ToString();
            textBox52.Text = datePRD.Day.ToString();
            DateTime dateEXP1 = (DateTime)update.EXPDate;
            textBox39.Text = dateEXP1.Year.ToString();
            textBox42.Text = dateEXP1.Month.ToString();
            textBox44.Text = dateEXP1.Day.ToString();
            textBox40.Text=update.Quantity.ToString();
            comboBox8.SelectedItem=update.Supplier_ID.ToString();
            textBox58.Text= update.Quantity.ToString();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox11.SelectedItem.ToString());
            int warehouse_ID = int.Parse(comboBox10.SelectedItem.ToString());
            var update = ent.Product_Entry.Where(j => j.ID == ID && j.Warehouse_ID == warehouse_ID).Select(d => d);
            foreach (Product_Entry item in update)
            {
                if (!comboBox9.Items.Contains(item.Product_Code))
                    comboBox9.Items.Add(item.Product_Code);
            }
        }

        private void comboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            var products = from p in ent.Products select p;
            comboBox17.Items.Clear();
            foreach (var product in products)
            {
                comboBox17.Items.Add(product.Code);
            }
        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox74.ReadOnly = false;
            int ID = int.Parse(comboBox18.SelectedItem.ToString());
            int Code = int.Parse(comboBox17.SelectedItem.ToString());
            Product_Warehouse item = ent.Product_Warehouse.Where(d=>d.WareHouse_ID==ID&&d.Product_Code==Code).FirstOrDefault();
            if (item != null)
                textBox56.Text = item.Quantity.ToString();
            else 
            {
                textBox56.Text = "No Items";
                textBox74.ReadOnly = true;
            }
                

        }

        private void ReleaseBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(textBox75.Text);
            Product_Release product_Release = new Product_Release();
            product_Release.ID = ID;
            product_Release.Warehouse_ID = int.Parse(comboBox18.SelectedItem.ToString());
            product_Release.Product_Code = int.Parse(comboBox17.SelectedItem.ToString());
            product_Release.Client_ID = int.Parse(comboBox16.SelectedItem.ToString());
            product_Release.Quantity = int.Parse(textBox74.Text);
            string date = $"{textBox71.Text}-{textBox72.Text}-{textBox76.Text}";
            DateTime date2;
            if (DateTime.TryParse(date, out date2) )
            {
                product_Release.Date = DateTime.Parse(date);
                Product_Warehouse update_quantity = ent.Product_Warehouse.Where(d => d.Product_Code == product_Release.Product_Code && d.WareHouse_ID == product_Release.Warehouse_ID).FirstOrDefault();
                if (update_quantity.Quantity>=product_Release.Quantity)
                {
                    update_quantity.Quantity -= product_Release.Quantity;
                    ent.Product_Release.Add(product_Release);
                    ent.SaveChanges();
                    textBox75.Text = textBox74.Text = textBox71.Text = textBox72.Text = textBox76.Text =  "";

                }
                else
                {
                    MessageBox.Show("لا يوجد هذه الكمية من الصنف المختار ");
                }
                
                RefreshProductRelease();

            }
            else
            {
                MessageBox.Show("صيغة التاريخ غير صحيحة برجاء ادخال التاريخ بصيغة 01-01-2023");
            }
        }
        public void RefreshProductRelease()
        {
            comboBox12.Items.Clear();
            var Entry = from p in ent.Product_Release select p;
            foreach (var item in Entry)
            {
                if (!comboBox12.Items.Contains(item.ID))
                    comboBox12.Items.Add(item.ID);
            }
        }

        private void UpdateReleaseBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox12.SelectedItem.ToString());
            int warehouse_ID = int.Parse(comboBox15.SelectedItem.ToString());
            int product_code = int.Parse(comboBox14.SelectedItem.ToString());
            Product_Release update = ent.Product_Release.Where(j => j.ID == ID && j.Product_Code == product_code && j.Warehouse_ID == warehouse_ID).Select(d => d).FirstOrDefault();
            update.Client_ID = int.Parse(comboBox13.SelectedItem.ToString());
            string date = $"{textBox62.Text}-{textBox63.Text}-{textBox64.Text}";
            DateTime date2;
            if (DateTime.TryParse(date, out date2))
            {
                update.Date = DateTime.Parse(date);
                Product_Warehouse update_quantity = ent.Product_Warehouse.Where(d => d.Product_Code == update.Product_Code && d.WareHouse_ID == update.Warehouse_ID).FirstOrDefault();
                update_quantity.Quantity += update.Quantity;
                update.Quantity = int.Parse(textBox58.Text);
                if (update_quantity.Quantity >= update.Quantity)
                {
                    update_quantity.Quantity -= update.Quantity;
                    ent.SaveChanges();
                    RefreshProductRelease();
                   textBox58.Text = textBox62.Text = textBox63.Text = textBox64.Text = "";
                }
                else
                {
                    MessageBox.Show("لا يوجد هذه الكمية من الصنف المختار ");
                }

            }
            else
            {
                MessageBox.Show("صيغة التاريخ غير صحيحة برجاء ادخال التاريخ بصيغة 01-01-2023");
            }
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox12.SelectedItem.ToString());
            int warehouse_ID = int.Parse(comboBox15.SelectedItem.ToString());
            var update = ent.Product_Release.Where(j => j.ID == ID && j.Warehouse_ID == warehouse_ID).Select(d => d);
            foreach (Product_Release item in update)
            {
                if (!comboBox14.Items.Contains(item.Product_Code))
                    comboBox14.Items.Add(item.Product_Code);
            }
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox12.SelectedItem.ToString());
            int warehouse_ID = int.Parse(comboBox15.SelectedItem.ToString());
            int product_code = int.Parse(comboBox14.SelectedItem.ToString());
            Product_Release update = ent.Product_Release.Where(j => j.ID == ID && j.Product_Code == product_code && j.Warehouse_ID == warehouse_ID).Select(d => d).FirstOrDefault();
            comboBox8.SelectedItem = update.Client_ID;
            DateTime date2 = update.Date.Value;
            textBox62.Text = date2.Year.ToString();
            textBox63.Text = date2.Month.ToString();
            textBox64.Text = date2.Day.ToString();
            comboBox8.SelectedItem = update.Client_ID.ToString();
            textBox58.Text=update.Quantity.ToString();
            Product_Warehouse item = ent.Product_Warehouse.Where(d => d.WareHouse_ID == ID && d.Product_Code == product_code).FirstOrDefault();
            textBox57.Text = item.Quantity.ToString();
            
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox12.SelectedItem.ToString());
            var update = ent.Product_Release.Where(j => j.ID == ID).Select(d => d);
            comboBox15.Items.Clear();
            foreach (Product_Release item in update)
            {
                if (!comboBox15.Items.Contains(item.Warehouse_ID))
                    comboBox15.Items.Add(item.Warehouse_ID);
            }
        }

        private void AddWarehouse_Click(object sender, EventArgs e)
        {
            transferProduct TP = new transferProduct();
            TP.ShowDialog();
            Application.Restart();
        }

        private void wareRprtBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox19.SelectedItem.ToString());
            var meta = ent.Product_Warehouse.Where(d => d.WareHouse_ID == ID).FirstOrDefault();
            label69.Text = "ID : " + meta.WareHouse.ID.ToString();
            label74.Text = "Name : " + meta.WareHouse.Name;
            label77.Text = "Address : " + meta.WareHouse.Address;
            label78.Text = "Manger : " + meta.WareHouse.Manger;
            var data = from pe in ent.Product_Entry
                       where pe.Warehouse_ID == ID 
                       select new
                       {
                           productName = pe.Product.Name,
                           Quantity = pe.Quantity,
                           Suplier = pe.Supplier.Name,
                           date = pe.Date,
                           ProductionDate = pe.ProductionDate,
                           EXPDate = pe.EXPDate

                       };
            dataGridView1.DataSource = data.ToList();
        }

        private void prdctRprBtn_Click(object sender, EventArgs e)
        {
            listBox10.Items.Clear();
            listBox9.Items.Clear();
            listBox8.Items.Clear();
            var selectedW = listBox7.SelectedItems;
            if (selectedW != null && comboBox20.SelectedItem != null)
            {
                int prodctSelected = int.Parse(comboBox20.SelectedItem.ToString());
                foreach (var item in selectedW)
                {
                    int ID = int.Parse(item.ToString());
                    Product_Warehouse product_Warehouse = ent.Product_Warehouse.Where(d=>d.WareHouse_ID==ID&& d.Product_Code==prodctSelected).FirstOrDefault();
                    if (product_Warehouse != null)
                    {
                        listBox10.Items.Add(product_Warehouse.Quantity);
                        listBox9.Items.Add(product_Warehouse.Unit);
                        listBox8.Items.Add(product_Warehouse.WareHouse.Name);
                    }
                }
            }
            
        }

        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse((string)comboBox20.SelectedItem.ToString());
            Product product = ent.Products.Where(d=>d.Code==ID).FirstOrDefault();
             label83.Text=product.Name;
        }

        private void prdctRprtBtn_Click(object sender, EventArgs e)
        {
            listBox16.Items.Clear();
            listBox19.Items.Clear();
            listBox18.Items.Clear();
            listBox17.Items.Clear();
            listBox20.Items.Clear();
            listBox15.Items.Clear();
            listBox13.Items.Clear();
            listBox12.Items.Clear();
            listBox11.Items.Clear();
            listBox21.Items.Clear();
            var warehouseIDs = listBox14.SelectedItems;
            string fromDate = $"{textBox65.Text}-{textBox66.Text}-{textBox67.Text}";
            string toDate = $"{textBox59.Text}-{textBox60.Text}-{textBox61.Text}";
            DateTime FromDate;
            DateTime ToDate;
            if(DateTime.TryParse(fromDate,out FromDate)&&DateTime.TryParse(toDate,out ToDate)&&warehouseIDs!=null)
            {
                foreach(var w in warehouseIDs)
                {
                    int ID = int.Parse(w.ToString());
                    var Entry = ent.Product_Entry.Where(d => d.Warehouse_ID == ID&&d.Date>=FromDate&&d.Date<=ToDate);
                    var Release = ent.Product_Release.Where(d=>d.Warehouse_ID==ID && d.Date >= FromDate && d.Date <= ToDate);
                    foreach(Product_Entry PE in Entry)
                    {
                        listBox15.Items.Add(PE.Product.Name);
                        listBox13.Items.Add(PE.Quantity);
                        listBox12.Items.Add(PE.Product.Unit);
                        listBox11.Items.Add(PE.WareHouse.Name);
                        listBox21.Items.Add(PE.Date);
                    }
                    foreach(Product_Release PR in Release)
                    {
                        listBox16.Items.Add(PR.Product.Name);
                        listBox19.Items.Add(PR.Quantity);
                        listBox18.Items.Add(PR.Product.Unit);
                        listBox17.Items.Add(PR.WareHouse.Name);
                        listBox20.Items.Add(PR.Date);
                    }
                }
            }
            else
            {
                MessageBox.Show("برجاء ادخال تواريخ صحيحة على صيغة 01-01-2023");
            }
        }

        private void StoreDateBtn_Click(object sender, EventArgs e)
        {
            listBox23.Items.Clear();
            listBox26.Items.Clear();
            listBox25.Items.Clear();
            listBox24.Items.Clear();
            listBox22.Items.Clear();
            string fromDate = $"{textBox68.Text}-{textBox69.Text}-{textBox70.Text}";
            DateTime FromDate;
            if (DateTime.TryParse(fromDate, out FromDate))
            {
                var Entry = ent.Product_Entry.Where(d=>d.Date<= FromDate);
                foreach(Product_Entry PE in Entry)
                {
                    listBox23.Items.Add(PE.Product.Name);
                    listBox26.Items.Add(PE.Quantity);
                    listBox25.Items.Add(PE.Product.Unit);
                    listBox24.Items.Add(PE.WareHouse.Name);
                    listBox22.Items.Add((DateTime.Now - (DateTime)PE.Date).Days);
                }
            }
            else
            {
                MessageBox.Show("برجاء ادخال تواريخ صحيحة على صيغة 01-01-2023");
            }

        }

        private void EXPdateBtn_Click(object sender, EventArgs e)
        {
            listBox28.Items.Clear();
            listBox31.Items.Clear();
            listBox30.Items.Clear();
            listBox29.Items.Clear();
            listBox27.Items.Clear();
            string eXPDate = $"{textBox73.Text}-{textBox77.Text}-{textBox78.Text}";
            DateTime EXPDate;
            if (DateTime.TryParse(eXPDate, out EXPDate))
            {
                var Entry = ent.Product_Entry.Where(d => d.EXPDate <= EXPDate);
                foreach (Product_Entry PE in Entry)
                {
                    listBox28.Items.Add(PE.Product.Name);
                    listBox31.Items.Add(PE.Quantity);
                    listBox30.Items.Add(PE.Product.Unit);
                    listBox29.Items.Add(PE.WareHouse.Name);
                    listBox27.Items.Add(((DateTime)PE.EXPDate- DateTime.Now).Days);
                }
            }
            else
            {
                MessageBox.Show("برجاء ادخال تواريخ صحيحة على صيغة 01-01-2023");
            }

        }

       
    }
}
