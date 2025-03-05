using System.Drawing;
using System.Windows.Forms;

namespace Module_1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private TableLayoutPanel MainPanel;

        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Reload = new System.Windows.Forms.Button();
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            this.button1 = new System.Windows.Forms.Button();
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.buttonSaleHistory = new System.Windows.Forms.Button();
            this.buttonSaleHistory.Click += new System.EventHandler(this.buttonSaleHistory_Click);
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.buttonAddProduct.Click += new System.EventHandler(this.buttonAddProduct_Click);
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // MainPanel
            this.MainPanel.ColumnCount = 1;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(20, 20);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 2;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.Size = new System.Drawing.Size(760, 560);
            this.MainPanel.TabIndex = 3;

            // flowLayoutPanel1
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 80);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(760, 480);
            this.flowLayoutPanel1.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(259, 37);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Учет партнеров и скидок";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.MainPanel);
            this.Name = "Form1";
            this.Text = "Партнерская система";
            this.ResumeLayout(false);
        }

        #region Windows Form Designer generated variables
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Reload;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAddProduct;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button buttonSaleHistory;
        #endregion
    } 
}

#endregion