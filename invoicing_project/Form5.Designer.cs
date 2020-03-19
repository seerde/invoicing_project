namespace invoicing_project
{
    partial class Form5
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
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.N_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.N_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.N_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.N_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.N_no,
            this.N_date,
            this.N_total,
            this.N_state});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(800, 448);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // N_no
            // 
            this.N_no.HeaderText = "رقم الفاتورة";
            this.N_no.Name = "N_no";
            this.N_no.ReadOnly = true;
            // 
            // N_date
            // 
            this.N_date.HeaderText = "تاريخ الفاتورة";
            this.N_date.Name = "N_date";
            this.N_date.ReadOnly = true;
            // 
            // N_total
            // 
            this.N_total.HeaderText = "السعر";
            this.N_total.Name = "N_total";
            this.N_total.ReadOnly = true;
            // 
            // N_state
            // 
            this.N_state.HeaderText = "حالة الفاتورة";
            this.N_state.Name = "N_state";
            this.N_state.ReadOnly = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn N_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn N_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn N_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn N_state;
    }
}