namespace Module_1
{
    partial class SalesHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvSalesHistory;
        private System.Windows.Forms.ComboBox cmbPartners;
        private System.Windows.Forms.Label lblFilter;

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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesHistory)).BeginInit();
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
            this.dgvSalesHistory.Location = new System.Drawing.Point(12, 50);
            this.dgvSalesHistory.Name = "dgvSalesHistory";
            this.dgvSalesHistory.ReadOnly = true;
            this.dgvSalesHistory.Size = new System.Drawing.Size(760, 400);
            this.dgvSalesHistory.TabIndex = 0;
            // 
            // cmbPartners
            // 
            this.cmbPartners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartners.FormattingEnabled = true;
            this.cmbPartners.Location = new System.Drawing.Point(100, 15);
            this.cmbPartners.Name = "cmbPartners";
            this.cmbPartners.Size = new System.Drawing.Size(200, 21);
            this.cmbPartners.TabIndex = 1;
            this.cmbPartners.SelectedIndexChanged += new System.EventHandler(this.cmbPartners_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(12, 18);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(82, 13);
            this.lblFilter.TabIndex = 2;
            this.lblFilter.Text = "Фильтр по партнеру:";
            // 
            // SalesHistoryForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.cmbPartners);
            this.Controls.Add(this.dgvSalesHistory);
            this.Name = "SalesHistoryForm";
            this.Text = "История продаж";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
