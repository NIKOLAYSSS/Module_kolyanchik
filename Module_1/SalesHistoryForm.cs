using Module_1.Repositories;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Module_1
{
    public partial class SalesHistoryForm : Form
    {
        private readonly IPartnerRepository _partnerRepository;
        private DatabaseHelper dbHelper;
        public SalesHistoryForm(IPartnerRepository partnerRepository)
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            dbHelper = new DatabaseHelper(connectionString);
            _partnerRepository = partnerRepository;
            LoadPartners();
            LoadSalesHistory();
        }

        // Загрузка списка партнеров в ComboBox
        private void LoadPartners()
        {
            var partners = _partnerRepository.GetAllPartners();
            cmbPartners.Items.Add("Все партнеры");
            cmbPartners.Items.AddRange(partners.Select(p => p.Name).ToArray());
            cmbPartners.SelectedIndex = 0;
        }


        // Загрузка истории продаж
        private void LoadSalesHistory()
        {
            int? partnerId = null;

            if (cmbPartners.SelectedIndex > 0) // Если выбран конкретный партнер
            {
                partnerId = (int)cmbPartners.SelectedItem;  // Получаем ID партнера из ComboBox (предполагается, что это ID, а не имя)
            }

            var sales = _partnerRepository.GetSalesHistory(partnerId);

            DataTable dt = new DataTable();
            dt.Columns.Add("SaleID", typeof(int));
            dt.Columns.Add("PartnerName", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("SaleDate", typeof(DateTime));

            foreach (var sale in sales)
            {
                // Получаем название партнёра и товара
                var partner = _partnerRepository.GetPartnerById(sale.PartnerID);  // Используем GetPartnerById
                var product = dbHelper.GetProductById(sale.ProductID);

                dt.Rows.Add(sale.SaleID, partner?.Name ?? "Неизвестно", product?.ProductName ?? "Неизвестно",
                            product?.MinimalPrice ?? 0, sale.Quantity, sale.SaleDate);
            }

            dgvSalesHistory.DataSource = dt;
        }


        // Фильтр по партнеру
        private void cmbPartners_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesHistory();
        }
    }
}
