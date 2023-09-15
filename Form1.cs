using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        Product _product = new Product();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProduct();   
        }
        private void LoadProduct()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = txtbName.Text,
                StockAmount = Convert.ToInt32(txtbStockAmount.Text),
                UnitPrice = Convert.ToDecimal(txtbUnitPrice.Text)
            });
            MessageBox.Show("Product Added!");
            LoadProduct();
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("You are clicked to cell");
            txtbNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            txtbUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            txtbStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _product = new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = txtbNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(txtbUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(txtbStockAmountUpdate.Text)
            };
            _productDal.Update(_product);
            LoadProduct();
            MessageBox.Show("Updated!");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);
            _productDal.Delete(id);
            MessageBox.Show("Deleted!");
            LoadProduct();
        }
    }
}
