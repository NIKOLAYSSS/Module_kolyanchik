using Module_1.Models;
using Module_1.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Module_1
{
    public partial class AddSaleForm : Form
    {
        private DatabaseHelper dbHelper;
        private List<Partner> partners;
        private List<Product> products;

        public AddSaleForm()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            dbHelper = new DatabaseHelper(connectionString);
            LoadPartners();
            LoadProducts();
        }

        private void LoadPartners()
        {
            partners = dbHelper.GetPartners();
            cmbPartners.DataSource = partners;
            cmbPartners.DisplayMember = "Name";
            cmbPartners.ValueMember = "PartnerID";
        }

        private void LoadProducts()
        {
            products = dbHelper.GetProducts();
            cmbProducts.DataSource = products;
            cmbProducts.DisplayMember = "ProductName";
            cmbProducts.ValueMember = "ProductID";
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            int quantity = (int)numQuantity.Value;
            lblDiscount.Text = $"Скидка: {CalculateDiscount(quantity)}%";
        }

        private int CalculateDiscount(int quantity)
        {
            if (quantity < 10000) return 0;
            if (quantity < 50000) return 5;
            if (quantity < 300000) return 10;
            return 15;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbPartners.SelectedItem == null || cmbProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнера и продукт.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int partnerId = (int)cmbPartners.SelectedValue;
            int productId = (int)cmbProducts.SelectedValue;
            int quantity = (int)numQuantity.Value;

            dbHelper.AddSaleHistory(partnerId, productId, quantity);
            MessageBox.Show("Продажа успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
