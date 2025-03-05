namespace Module_1
{
    partial class SalesHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvSalesHistory;
        private System.Windows.Forms.ComboBox cmbPartners;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.Label lblFilter1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvSalesHistory = new System.Windows.Forms.DataGridView();
            this.cmbPartners = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.lblFilter1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesHistory)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSalesHistory
            // 
            this.dgvSalesHistory.AllowUserToAddRows = false;
            this.dgvSalesHistory.AllowUserToDeleteRows = false;
            this.dgvSalesHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalesHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesHistory.Location = new System.Drawing.Point(12, 100);
            this.dgvSalesHistory.Name = "dgvSalesHistory";
            this.dgvSalesHistory.ReadOnly = true;
            this.dgvSalesHistory.Size = new System.Drawing.Size(760, 350);
            this.dgvSalesHistory.TabIndex = 0;
            // 
            // cmbPartners
            // 
            this.cmbPartners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPartners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartners.FormattingEnabled = true;
            this.cmbPartners.Location = new System.Drawing.Point(133, 3);
            this.cmbPartners.Name = "cmbPartners";
            this.cmbPartners.Size = new System.Drawing.Size(200, 21);
            this.cmbPartners.TabIndex = 1;
            this.cmbPartners.SelectedIndexChanged += new System.EventHandler(this.cmbPartners_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilter.Location = new System.Drawing.Point(3, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(124, 30);
            this.lblFilter.TabIndex = 2;
            this.lblFilter.Text = "Фильтр по партнеру:";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbProducts
            // 
            this.cmbProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(133, 33);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(200, 21);
            this.cmbProducts.TabIndex = 3;
            this.cmbProducts.SelectedIndexChanged += new System.EventHandler(this.cmbProducts_SelectedIndexChanged);
            // 
            // lblFilter1
            // 
            this.lblFilter1.AutoSize = true;
            this.lblFilter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilter1.Location = new System.Drawing.Point(3, 30);
            this.lblFilter1.Name = "lblFilter1";
            this.lblFilter1.Size = new System.Drawing.Size(124, 30);
            this.lblFilter1.TabIndex = 4;
            this.lblFilter1.Text = "Фильтр по продукции:";
            this.lblFilter1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lblFilter, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.cmbPartners, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblFilter1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.cmbProducts, 1, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(760, 60);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // SalesHistoryForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.dgvSalesHistory);
            this.Name = "SalesHistoryForm";
            this.Text = "История продаж";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesHistory)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
