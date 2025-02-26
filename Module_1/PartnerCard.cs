using Module_1.Models;
using Module_1.Repositories;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Module_1
{
    public partial class PartnerCard : UserControl
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly Partner _partner;
        private Label lblRating;
        private Label lblPhone;
        private Label lblDirector;
        private Label lblName;
        private Label lblType;
        private Button buttonDelete;
        private Panel panel1;
        private Label labelSeparator;
        private Label lblDiscount;

        // Стилевые настройки
        private Color _borderColor = Color.FromArgb(224, 224, 224);
        private const int CornerRadius = 12;
        private Color _hoverColor = Color.FromArgb(245, 245, 245);
        private Color _textColor = Color.FromArgb(64, 64, 64);
        private Color _accentColor = Color.FromArgb(103, 186, 128);

        private Timer _hoverTimer;
        private float _hoverProgress = 0f;
        private const int ShadowSize = 5;

        public PartnerCard(Partner partner, IPartnerRepository partnerRepository)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            _partnerRepository = partnerRepository;
            _partner = partner;
            InitializeComponent();
            SetupAnimations();
            InitializeStyles();
            LoadData();
            SubscribeAllControls(this);
            this.UpdateStyles();
            this.ResumeLayout(true);
        }

        private void SetupAnimations()
        {
            _hoverTimer = new Timer { Interval = 16 }; // ~60 FPS
            _hoverTimer.Tick += HoverTimer_Tick;

            // Явная подписка на события мыши
            this.MouseEnter += (s, e) => _hoverTimer.Start();
            this.MouseLeave += (s, e) => _hoverTimer.Start();
        }


        private void InitializeStyles()
        {
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            // Основные настройки
            this.BackColor = Color.White;
            this.Padding = new Padding(15);
            this.Margin = new Padding(10, 15, 10, 15);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(340, 140);

            // Настройка шрифтов
            lblType.Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold);
            lblName.Font = new Font("Segoe UI", 11);
            lblDirector.Font = new Font("Segoe UI", 9);
            lblPhone.Font = new Font("Segoe UI", 9);
            lblRating.Font = new Font("Segoe UI", 9);

            // Цвета текста
            lblType.ForeColor = _accentColor;
            lblName.ForeColor = _textColor;
            lblDirector.ForeColor = _textColor;
            lblPhone.ForeColor = _textColor;
            lblRating.ForeColor = _textColor;

            // Стиль кнопки удаления
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.BackColor = Color.Transparent;
            buttonDelete.ForeColor = Color.FromArgb(180, 180, 180);
            buttonDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonDelete.Text = "×";
            buttonDelete.Size = new Size(28, 28);
            buttonDelete.Cursor = Cursors.Hand;
            buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDelete.MouseEnter += (s, e) => buttonDelete.ForeColor = Color.FromArgb(220, 50, 50);
            buttonDelete.MouseLeave += (s, e) => buttonDelete.ForeColor = Color.FromArgb(180, 180, 180);
        }

        private void LoadData()
        {
            lblType.Text = _partner.PartnerType?.TypeName?.ToUpper();
            lblName.Text = _partner.Name;
            lblDirector.Text = _partner.DirectorName;
            lblPhone.Text = FormatPhoneNumber(_partner.Phone);
            lblRating.Text = $"{_partner.Rating}/10";
            // Получаем сумму продаж партнера
            decimal totalSales = _partnerRepository.GetTotalSalesQuantity(_partner.PartnerID);

            // Вычисляем скидку
            int discount = CalculateDiscount(totalSales);

            // Отображаем скидку
            lblDiscount.Text = $"Скидка: {discount}%";
        }
        private int CalculateDiscount(decimal totalSales)
        {
            if (totalSales >= 300000) return 15;
            if (totalSales >= 50000) return 10;
            if (totalSales >= 10000) return 5;
            return 0;
        }
        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return phone;

            if (phone.Length == 11)
            {
                return $"+7 ({phone.Substring(1, 3)}) {phone.Substring(4, 3)}-{phone.Substring(7, 2)}-{phone.Substring(9)}";
            }
            if (phone.Length == 10)
            {
                return $"+7 ({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 2)}-{phone.Substring(8)}";
            }
            return phone;
        }

        private void InitializeComponent()
        {
            this.lblDiscount = new System.Windows.Forms.Label();


            // Настройки lblDiscount
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(150, 90);  // Задайте позицию
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(100, 13); // Убедитесь, что задается размер
            this.lblDiscount.TabIndex = 7;
            this.lblDiscount.ForeColor = _textColor;
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDirector = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.labelSeparator);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblDirector);
            this.panel1.Controls.Add(this.lblPhone);
            this.panel1.Controls.Add(this.lblRating);
            this.panel1.Controls.Add(this.lblDiscount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15);
            this.panel1.Size = new System.Drawing.Size(340, 140);
            this.panel1.TabIndex = 0;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(279, 10);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(43, 23);
            this.buttonDelete.TabIndex = 0;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.labelSeparator.Location = new System.Drawing.Point(105, 20);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(1, 20);
            this.labelSeparator.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(20, 20);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(0, 13);
            this.lblType.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(115, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 3;
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.Location = new System.Drawing.Point(20, 50);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(0, 13);
            this.lblDirector.TabIndex = 4;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 70);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(0, 13);
            this.lblPhone.TabIndex = 5;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(20, 90);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(0, 13);
            this.lblRating.TabIndex = 6;
            // 
            // PartnerCard
            // 
            this.Controls.Add(this.panel1);
            this.Name = "PartnerCard";
            this.Size = new System.Drawing.Size(340, 140);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Создаем буфер для двойной буферизации
            using (var buffer = new Bitmap(Width, Height))
            using (var g = Graphics.FromImage(buffer))
            {
                var shadowRect = new Rectangle(
                    ShadowSize,
                    ShadowSize,
                    Width - ShadowSize * 2,
                    Height - ShadowSize * 2);

                // Рисуем тень
                using (var path = GetRoundedRectanglePath(shadowRect, CornerRadius))
                using (var shadowBrush = new SolidBrush(Color.FromArgb((int)(30 * _hoverProgress), Color.Black)))
                {
                    g.FillPath(shadowBrush, path);
                }

                // Основной фон
                var mainRect = new Rectangle(0, 0, Width - ShadowSize, Height - ShadowSize);
                using (var path = GetRoundedRectanglePath(mainRect, CornerRadius))
                {
                    // Заливка
                    using (var brush = new SolidBrush(InterpolateColor(Color.White, _hoverColor, _hoverProgress)))
                    {
                        g.FillPath(brush, path);
                    }

                    // Обводка
                    using (var pen = new Pen(InterpolateColor(_borderColor, Color.FromArgb(200, 200, 200), _hoverProgress), 1))
                    {
                        g.DrawPath(pen, path);
                    }
                }

                // Копируем буфер на экран
                e.Graphics.DrawImage(buffer, 0, 0);
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void HoverTimer_Tick(object sender, EventArgs e)
        {
            var target = this.ClientRectangle.Contains(this.PointToClient(MousePosition)) ? 1f : 0f;
            var step = 0.15f;

            _hoverProgress = Math.Min(Math.Max(_hoverProgress + (target == 1f ? step : -step), 0f), 1f);

            if (Math.Abs(_hoverProgress - target) < 0.01f)
            {
                _hoverProgress = target;
                _hoverTimer.Stop();
            }

            this.Invalidate();
        }

        private Color InterpolateColor(Color start, Color end, float progress)
        {
            return Color.FromArgb(
                (int)(start.R + (end.R - start.R) * progress),
                (int)(start.G + (end.G - start.G) * progress),
                (int)(start.B + (end.B - start.B) * progress));
        }

        private void SubscribeAllControls(Control parent)
        {
            parent.Click += PartnerCard_Click;
            foreach (Control child in parent.Controls)
            {
                SubscribeAllControls(child);
                child.MouseEnter += (s, e) => OnMouseEnter(e);
                child.MouseLeave += (s, e) => OnMouseLeave(e);
            }
        }

        private void PartnerCard_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn == buttonDelete) return;
            new AddPartnerForm(_partnerRepository, _partner).ShowDialog();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этого партнера?",
                "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _partnerRepository.DeletePartner(_partner.PartnerID);
                    Parent?.Controls.Remove(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Region = new Region(GetRoundedRectanglePath(ClientRectangle, CornerRadius));
        }
    }
}