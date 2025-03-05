using Module_1.Models;
using Module_1.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Module_1
{
    public partial class AddPartnerForm : Form
    {
        private readonly IPartnerRepository _partnerRepository;
        private Partner _partner;
        private Color _accentColor = Color.FromArgb(103, 186, 128); // Акцентный цвет
        private Color _bgColor = Color.FromArgb(250, 250, 250); // Фон приложения

        public AddPartnerForm(IPartnerRepository partnerRepository, Partner partner = null)
        {
            InitializeComponent();
            _partnerRepository = partnerRepository;
            _partner = partner;

            if (_partner != null)
            {
                // Если это форма редактирования, заполняем поля
                textBoxName.Text = _partner.Name;
                textBoxDirectorName.Text = _partner.DirectorName;
                textBoxEmail.Text = _partner.Email;
                textBoxPhone.Text = _partner.Phone;
                textBoxLegalAddress.Text = _partner.LegalAddress;
                textBoxINN.Text = _partner.INN;
                textBoxRating.Text = _partner.Rating?.ToString();
                comboBoxPartnerType.SelectedItem = _partner.PartnerType?.TypeName; // Устанавливаем текущий тип партнера
            }
            ApplyModernStyle();
            InitializeUI();
            // Заполняем комбобокс типами партнеров
            LoadPartnerTypes();
        }
        private void ApplyModernStyle()
        {
            // Настройки главной формы
            this.BackColor = _bgColor;
            this.Font = new Font("Segoe UI", 9);
            this.Padding = new Padding(20);
            this.DoubleBuffered = true;

            // Стилизация всех текстовых полей
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.FromArgb(64, 64, 64);
                    textBox.Font = new Font("Segoe UI", 10);
                    textBox.Margin = new Padding(5);
                }
                else if (control is ComboBox comboBox)
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
            }
        }

        private void InitializeUI()
        {
            // Заголовок
            lblTitle.Font = new Font("Segoe UI Semibold", 24, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.AutoSize = true;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Margin = new Padding(10); 

            // Основной макет
            MainPanel.BackColor = Color.Transparent;
            MainPanel.ColumnCount = 2;
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Лейблы
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F)); // Поля ввода
            MainPanel.RowCount = 10; // Количество строк для каждого элемента + заголовок

            // Устанавливаем одинаковую высоту для всех строк
            for (int i = 0; i < MainPanel.RowCount; i++)
            {
                MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Высота строки 40 пикселей
            }

            // Добавляем заголовок в MainPanel
            MainPanel.Controls.Add(lblTitle, 0, 0);
            MainPanel.SetColumnSpan(lblTitle, 2);

            // Добавляем элементы в MainPanel
            MainPanel.Controls.Add(label1, 0, 1);
            MainPanel.Controls.Add(comboBoxPartnerType, 1, 1);
            MainPanel.Controls.Add(label2, 0, 2);
            MainPanel.Controls.Add(textBoxName, 1, 2);
            MainPanel.Controls.Add(label3, 0, 3);
            MainPanel.Controls.Add(textBoxDirectorName, 1, 3);
            MainPanel.Controls.Add(label4, 0, 4);
            MainPanel.Controls.Add(textBoxEmail, 1, 4);
            MainPanel.Controls.Add(label5, 0, 5);
            MainPanel.Controls.Add(textBoxPhone, 1, 5);
            MainPanel.Controls.Add(label6, 0, 6);
            MainPanel.Controls.Add(textBoxLegalAddress, 1, 6);
            MainPanel.Controls.Add(label7, 0, 7);
            MainPanel.Controls.Add(textBoxINN, 1, 7);
            MainPanel.Controls.Add(label8, 0, 8);
            MainPanel.Controls.Add(textBoxRating, 1, 8);

            // Создаем панель для кнопок
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80, // Фиксированная высота для кнопок (увеличена)
                BackColor = Color.Transparent,
                Padding = new Padding(20, 10, 20, 10) // Отступы внутри панели
            };

            // Стилизация кнопок
            StyleButton(btnSave, "Сохранить");
            StyleButton(btnCancel, "Отмена");

            // Размещаем кнопки в buttonPanel
            btnSave.Anchor = AnchorStyles.Right; // Закрепляем кнопку справа
            btnCancel.Anchor = AnchorStyles.Right; // Закрепляем кнопку справа
            btnSave.Location = new Point(buttonPanel.Width - btnSave.Width - 10, 10); // Позиция кнопки "Сохранить"
            btnCancel.Location = new Point(buttonPanel.Width - btnSave.Width - btnCancel.Width - 20, 10); // Позиция кнопки "Отмена"

            // Добавляем кнопки в buttonPanel
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);

            // Добавляем buttonPanel в MainPanel
            MainPanel.Controls.Add(buttonPanel, 0, 9);
            MainPanel.SetColumnSpan(buttonPanel, 2);

            // Закрепляем MainPanel
            MainPanel.Dock = DockStyle.Fill;

            // Устанавливаем минимальную высоту формы
            this.MinimumSize = new Size(400, 400); // Минимальная ширина и высота формы (увеличена)
        }

        private void StyleButton(Button btn, string text)
        {
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = _accentColor;
            btn.ForeColor = Color.White;
            btn.Size = new Size(120, 60);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(5);

            // Анимация при наведении
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Dark(_accentColor, 0.1f);
            btn.MouseLeave += (s, e) => btn.BackColor = _accentColor;
        }


        private void LoadPartnerTypes()
        {
            var partnerTypes = _partnerRepository.GetPartnerTypes();
            comboBoxPartnerType.DataSource = partnerTypes;
            comboBoxPartnerType.DisplayMember = "TypeName"; // Это поле отображаем в ComboBox
            comboBoxPartnerType.ValueMember = "PartnerTypeID"; // Это значение будет храниться в ComboBox
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидируем введенные данные
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxDirectorName.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            if (_partner == null)
            {
                // Если это добавление, создаем новый объект партнера
                _partner = new Partner
                {
                    PartnerID = 0, // Обеспечиваем, что это новый объект (ID = 0)
                    Name = textBoxName.Text,
                    DirectorName = textBoxDirectorName.Text,
                    Email = textBoxEmail.Text,
                    Phone = textBoxPhone.Text,
                    LegalAddress = textBoxLegalAddress.Text,
                    INN = textBoxINN.Text,
                    Rating = string.IsNullOrEmpty(textBoxRating.Text) ? (int?)null : int.Parse(textBoxRating.Text),
                    PartnerTypeID = (int)comboBoxPartnerType.SelectedValue
                };

                // Добавляем партнера в базу данных
                _partnerRepository.AddPartner(_partner);
            }
            else
            {
                // Если это редактирование, обновляем данные партнера
                _partner.Name = textBoxName.Text;
                _partner.DirectorName = textBoxDirectorName.Text;
                _partner.Email = textBoxEmail.Text;
                _partner.Phone = textBoxPhone.Text;
                _partner.LegalAddress = textBoxLegalAddress.Text;
                _partner.INN = textBoxINN.Text;
                _partner.Rating = string.IsNullOrEmpty(textBoxRating.Text) ? (int?)null : int.Parse(textBoxRating.Text);
                _partner.PartnerTypeID = (int)comboBoxPartnerType.SelectedValue;

                // Обновляем данные в базе данных
                _partnerRepository.UpdatePartner(_partner);
            }

            MessageBox.Show("Данные партнера сохранены.");
            this.Close(); // Закрываем форму
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем форму без сохранения
        }
    }
}
