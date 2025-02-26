using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Module_1.Repositories;

using System.Configuration;
using Module_1.Models;

namespace Module_1
{
    public partial class Form1 : Form
    {
        private PartnerRepository partnerRepository;
        private Color _accentColor = Color.FromArgb(103, 186, 128); // Акцентный цвет
        private Color _bgColor = Color.FromArgb(250, 250, 250); // Фон приложения

        public Form1()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            partnerRepository = new PartnerRepository(connectionString);

            InitializeComponent();
            ApplyModernStyle();
            InitializeUI();
            LoadPartners();
        }

        private void ApplyModernStyle()
        {
            // Настройки главной формы
            this.BackColor = _bgColor;
            this.Font = new Font("Segoe UI", 9);
            this.Padding = new Padding(20);
            this.DoubleBuffered = true;
        }

        private void InitializeUI()
        {

            // Заголовок
            lblTitle.Font = new Font("Segoe UI Semibold", 24, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Margin = new Padding(0, 0, 0, 30);

            // Контейнер для карточек
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.Padding = new Padding(0, 20, 0, 0);

            // Навигационная панель
            var navPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15)
            };

            // Контейнер для кнопок справа
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight, // Изменено направление
                Margin = new Padding(0),
                Padding = new Padding(0),
                WrapContents = false
            };


            // Стилизация кнопок
            StyleButton(Reload, "🔄 Обновить");
            StyleButton(button1, "➕ партнера");
            StyleButton(buttonAddProduct, "➕ продукт");

            // Добавляем кнопки в обратном порядке из-за FlowDirection
            buttonPanel.Controls.Add(Reload);
            buttonPanel.Controls.Add(button1);
            buttonPanel.Controls.Add(buttonAddProduct);

            // Добавляем элементы в навигационную панель
            navPanel.Controls.Add(lblTitle);
            navPanel.Controls.Add(buttonPanel);



            // Основной макет
            MainPanel.Controls.Add(navPanel, 0, 0);
            MainPanel.Controls.Add(flowLayoutPanel1, 0, 1);
        }

        private void StyleButton(Button btn, string text)
        {
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = _accentColor;
            btn.ForeColor = Color.White;
            btn.Size = new Size(160, 40);
            btn.Margin = new Padding(10, 0, 0, 0); // Отступ только слева
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(5);

            // Анимация при наведении
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Dark(_accentColor, 0.1f);
            btn.MouseLeave += (s, e) => btn.BackColor = _accentColor;
        }

        private void LoadPartners()
        {
            flowLayoutPanel1.Controls.Clear();
            var partners = partnerRepository.GetAllPartners();

            foreach (var partner in partners)
            {
                var card = new PartnerCard(partner, partnerRepository)
                {
                    Margin = new Padding(0, 0, 20, 20)
                };
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            LoadPartners();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var editForm = new AddPartnerForm(partnerRepository);
            editForm.ShowDialog();
        }
        private void buttonAddProduct_Click(object sender, EventArgs e)
        {

            var AddSaleForm = new SalesHistoryForm(partnerRepository);
            //var AddSaleForm = new AddSaleForm();
            //var editForm = new AddPartnerForm(partnerRepository);
            AddSaleForm.ShowDialog();
        }
    }
}
