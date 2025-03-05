using Module_1.Repositories;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Module_1
{
    public partial class SalesHistoryForm : Form
    {
        private readonly IPartnerRepository _partnerRepository;
        private DatabaseHelper dbHelper;
        private Color _accentColor = Color.FromArgb(103, 186, 128); // Акцентный цвет
        private Color _bgColor = Color.FromArgb(250, 250, 250); // Фон приложения
        public SalesHistoryForm(IPartnerRepository partnerRepository)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            dbHelper = new DatabaseHelper(connectionString);
            _partnerRepository = partnerRepository;
            ApplyModernStyle();
            LoadPartners();
            LoadProducts();
            LoadSalesHistory();
        }
        private void ApplyModernStyle()
        {
            // Настройки главной формы
            this.BackColor = _bgColor;
            this.Font = new Font("Segoe UI", 9);
            this.Padding = new Padding(20);
            this.DoubleBuffered = true;

            // Стилизация всех элементов
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.BackColor = Color.White;
                    comboBox.ForeColor = Color.FromArgb(64, 64, 64);
                    comboBox.Font = new Font("Segoe UI", 10);
                    comboBox.Margin = new Padding(5);
                }
                else if (control is Label label)
                {
                    label.ForeColor = Color.FromArgb(64, 64, 64);
                    label.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
                else if (control is DataGridView dataGridView)
                {
                    dataGridView.BackgroundColor = Color.White;
                    dataGridView.BorderStyle = BorderStyle.None;
                    dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = _accentColor;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dataGridView.EnableHeadersVisualStyles = false;
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }
        // Загрузка списка партнеров в ComboBox
        private void LoadPartners()
        {
            var partners = _partnerRepository.GetAllPartners();
            cmbPartners.Items.Add(new { Name = "Все партнеры", ID = 0 }); // Добавляем элемент "Все партнеры" с ID = -1
            cmbPartners.Items.AddRange(partners.Select(p => new { p.Name, p.PartnerID }).ToArray());
            cmbPartners.DisplayMember = "Name"; // Отображаем название партнера
            cmbPartners.ValueMember = "PartnerID"; // Используем ID партнера как значение
            cmbPartners.SelectedIndex = 0;
        }
        private void LoadProducts()
        {
            var products = _partnerRepository.GetAllProducts();
            cmbProducts.Items.Add(new { ProductName = "Все продукты", ID = 0 });
            cmbProducts.Items.AddRange(products.Select(pr => new { pr.ProductName, pr.ProductID }).ToArray());
            cmbProducts.DisplayMember = "ProductName";
            cmbProducts.ValueMember = "ProductID";
            cmbProducts.SelectedIndex = 0;
        }


        // Загрузка истории продаж
        private void LoadSalesHistory()
        {
            int? partnerId = null;
            int? productId = null;

            if (cmbPartners.SelectedIndex > 0) // Если выбран конкретный партнер
            {
                var selectedItem = cmbPartners.SelectedItem as dynamic; // Используем dynamic для доступа к свойствам
                if (selectedItem != null)
                {
                    partnerId = selectedItem.PartnerID; // Получаем ID партнера
                }
            }

            if (cmbProducts.SelectedIndex > 0)
            {
                var selectedItem = cmbProducts.SelectedItem as dynamic; // Используем dynamic для доступа к свойствам
                if (selectedItem != null)
                {
                    productId = selectedItem.ProductID; // Получаем ID партнера
                }
            }
                var sales = _partnerRepository.GetSalesHistory(partnerId, productId);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID продажи", typeof(int));
            dt.Columns.Add("Название партнера", typeof(string));
            dt.Columns.Add("Название продукта", typeof(string));
            dt.Columns.Add("Артикул", typeof(string));
            dt.Columns.Add("Количество", typeof(int));
            dt.Columns.Add("Дата", typeof(DateTime));

            foreach (var sale in sales)
            {
                // Получаем название партнёра и товара
                var partner = _partnerRepository.GetPartnerById(sale.PartnerID);  // Используем GetPartnerById
                var product = dbHelper.GetProductById(sale.ProductID);

                dt.Rows.Add(sale.SaleID, partner?.Name ?? "Неизвестно", product?.ProductName ?? "Неизвестно",
                            product?.Article, sale.Quantity, sale.SaleDate);
            }

            dgvSalesHistory.DataSource = dt;
        }


        // Фильтр по партнеру
        private void cmbPartners_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesHistory();
        }
        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesHistory();
        }
    }
}
