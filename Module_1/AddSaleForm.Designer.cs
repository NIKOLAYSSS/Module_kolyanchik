namespace Module_1
{
    partial class AddSaleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.ComboBox cmbPartners;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Button btnAdd;

        private void InitializeComponent()
        {
            this.cmbPartners = new System.Windows.Forms.ComboBox();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();

            // ComboBox Партнеры
            this.cmbPartners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartners.FormattingEnabled = true;
            this.cmbPartners.Location = new System.Drawing.Point(20, 20);
            this.cmbPartners.Size = new System.Drawing.Size(200, 21);

            // ComboBox Продукты
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(20, 60);
            this.cmbProducts.Size = new System.Drawing.Size(200, 21);

            // NumericUpDown Количество
            this.numQuantity.Location = new System.Drawing.Point(20, 100);
            this.numQuantity.Maximum = 1000000;
            this.numQuantity.Minimum = 1;
            this.numQuantity.Size = new System.Drawing.Size(100, 20);
            this.numQuantity.ValueChanged += new System.EventHandler(this.numQuantity_ValueChanged);

            // Label Скидка
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(140, 102);
            this.lblDiscount.Size = new System.Drawing.Size(80, 13);
            this.lblDiscount.Text = "Скидка: 0%";

            // Кнопка Добавить
            this.btnAdd.Location = new System.Drawing.Point(20, 140);
            this.btnAdd.Size = new System.Drawing.Size(200, 30);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // Настройки формы
            this.ClientSize = new System.Drawing.Size(250, 200);
            this.Controls.Add(this.cmbPartners);
            this.Controls.Add(this.cmbProducts);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.btnAdd);
            this.Text = "Добавить продажу";

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

    }
}
#endregion