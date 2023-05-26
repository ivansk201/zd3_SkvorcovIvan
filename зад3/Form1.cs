using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace зад3
{
    public partial class Form1 :Form
    {
        private string dataFilePath = "products.txt";
        private List<Tovar> products;
        private List<Tovar> filteredProducts;

        public Form1 ()
        {
            InitializeComponent( );
            products = new List<Tovar>( );
            filteredProducts = new List<Tovar>( );
            Tovar.AddProduct(products, new Tovar("Телефон", 14999, 5345));
            Tovar.AddProduct(products, new Tovar("Ноутбук", 35000, 5345));
            Tovar.AddProduct(products, new Tovar("Планшет", 34500, 3452));
            Tovar.AddProduct(products, new Tovar("Мышка", 1000, 2352));
            Tovar.AddProduct(products, new Tovar("Клавитура", 500, 2345));
            Tovar.AddProduct(products, new Tovar("Коврик", 1000, 2353));
            TovarNaSkalde.AddProduct(products, new TovarNaSkalde("Монитор", 300, 34533, 2022));
            TovarNaSkalde.AddProduct(products, new TovarNaSkalde("Принтер", 200, 3453, 2019));
            TovarNaSkalde.AddProduct(products, new TovarNaSkalde("Сканер", 300, 4234, 2021));
            TovarNaSkalde.AddProduct(products, new TovarNaSkalde("3D-Ручка", 200, 234, 2020));
            TovarNaSkalde.AddProduct(products, new TovarNaSkalde("Чехол на телефон", 300, 4234, 2022));
            TovarNaSkalde.AddProduct(products, new TovarNaSkalde("Чехол на планшет", 200, 421, 2023));

        }

        private void button1_Click (object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string priceText = textBox2.Text;
            string quantityText = textBox3.Text;
            string releaseYearText = textBox4.Text;
            double price;
            int quantity;
            int releaseYear;

            if (string.IsNullOrWhiteSpace(name))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                MessageBox.Show("Пожалуйста, введите имя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            if (!double.TryParse(priceText, out price))
            {
                MessageBox.Show("Неверное значение цены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(quantityText, out quantity))
            {
                MessageBox.Show("Неверное значение количества.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(releaseYearText, out releaseYear))
            {
                MessageBox.Show("Неверное значение года выпуска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TovarNaSkalde newProduct = new TovarNaSkalde(name, price, quantity, releaseYear);
            Tovar.AddProduct(products, newProduct);
            listBox1.Items.Add(newProduct.GetInfo( ));

            MessageBox.Show("Вы успешно добавили новый товар.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
        }

        private void button2_Click (object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < products.Count)
            {
                Tovar selectedProduct = products [ selectedIndex ];
                Tovar.RemoveProduct(products, selectedProduct);
                listBox1.Items.RemoveAt(selectedIndex);

                MessageBox.Show("Вы удалили товар из списка.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load (object sender, EventArgs e)
        {
            foreach (Tovar product in products)
            {
                listBox1.Items.Add(product.GetInfo( ));
            }
        }

        private void button3_Click (object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < products.Count)
            {
                Tovar selectedProduct = products [ selectedIndex ];
                label2.Text = selectedProduct.GetInfo( );
                if (selectedProduct is TovarNaSkalde warehouseProduct)
                {
                    label3.Text = warehouseProduct.GetInfoWithYear( );
                }
                else
                {
                    label3.Text = string.Empty;
                }
            }
        }
        private void UpdateProductList (IEnumerable<Tovar> productList)
        {
            listBox1.Items.Clear( );
            foreach (Tovar product in productList)
            {
                listBox1.Items.Add(product.GetInfo( ));
            }
        }
        private void button5_Click (object sender, EventArgs e)
        {
            string countText = textBox5.Text;
            int count;


            if (!int.TryParse(countText, out count))
            {
                textBox5.Visible = true;
                label9.Visible = true;
                MessageBox.Show("Вы не ввели как хотите отфильтровать цену товаров", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            filteredProducts = products.Where(p => p.Price > count).ToList( );
            UpdateProductList(filteredProducts);
            textBox5.Visible = false;
            textBox5.Text = "";
            label9.Visible = false;
        }

        private void button4_Click (object sender, EventArgs e)
        {
            filteredProducts = products.OrderBy(p => p.Name).ToList( );
            UpdateProductList(filteredProducts);
            MessageBox.Show("Вы отсортировали по алфовиту список товаров", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
