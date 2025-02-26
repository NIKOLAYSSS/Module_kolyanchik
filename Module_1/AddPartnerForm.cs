using Module_1.Models;
using Module_1.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Module_1
{
    public partial class AddPartnerForm : Form
    {
        private readonly IPartnerRepository _partnerRepository;
        private Partner _partner;

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

            // Заполняем комбобокс типами партнеров
            LoadPartnerTypes();
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
