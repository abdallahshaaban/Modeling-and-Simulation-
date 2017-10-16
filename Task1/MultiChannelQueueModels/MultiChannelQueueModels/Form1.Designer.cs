namespace MultiChannelQueueModels
{
    partial class Form1
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
            this.txtnservers = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlservers = new System.Windows.Forms.Panel();
            this.btnadd = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Probability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnservers = new System.Windows.Forms.Button();
            this.btninterarrival = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlservers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.Size = new System.Drawing.Size(1192, 687);
            this.dataGridView1.TabIndex = 0;
            // 
            // txtnservers
            // 
            this.txtnservers.Location = new System.Drawing.Point(1225, 53);
            this.txtnservers.Name = "txtnservers";
            this.txtnservers.Size = new System.Drawing.Size(115, 24);
            this.txtnservers.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1222, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Servers Number";
            // 
            // pnlservers
            // 
            this.pnlservers.Controls.Add(this.btnadd);
            this.pnlservers.Controls.Add(this.dataGridView2);
            this.pnlservers.Controls.Add(this.label2);
            this.pnlservers.Location = new System.Drawing.Point(1228, 146);
            this.pnlservers.Name = "pnlservers";
            this.pnlservers.Size = new System.Drawing.Size(441, 388);
            this.pnlservers.TabIndex = 3;
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(15, 345);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(115, 31);
            this.btnadd.TabIndex = 2;
            this.btnadd.Text = "Add Distribution";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Probability});
            this.dataGridView2.Location = new System.Drawing.Point(15, 37);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 26;
            this.dataGridView2.Size = new System.Drawing.Size(408, 302);
            this.dataGridView2.TabIndex = 1;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // Probability
            // 
            this.Probability.HeaderText = "Probability";
            this.Probability.Name = "Probability";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // btnservers
            // 
            this.btnservers.Location = new System.Drawing.Point(1225, 94);
            this.btnservers.Name = "btnservers";
            this.btnservers.Size = new System.Drawing.Size(195, 30);
            this.btnservers.TabIndex = 4;
            this.btnservers.Text = "Add Service Time Distribution";
            this.btnservers.UseVisualStyleBackColor = true;
            this.btnservers.Click += new System.EventHandler(this.btnservers_Click);
            // 
            // btninterarrival
            // 
            this.btninterarrival.Location = new System.Drawing.Point(1476, 94);
            this.btninterarrival.Name = "btninterarrival";
            this.btninterarrival.Size = new System.Drawing.Size(193, 30);
            this.btninterarrival.TabIndex = 5;
            this.btninterarrival.Text = "Add Interarrival Distribution";
            this.btninterarrival.UseVisualStyleBackColor = true;
            this.btninterarrival.Click += new System.EventHandler(this.btninterarrival_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1681, 712);
            this.Controls.Add(this.btninterarrival);
            this.Controls.Add(this.btnservers);
            this.Controls.Add(this.pnlservers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtnservers);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlservers.ResumeLayout(false);
            this.pnlservers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtnservers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlservers;
        private System.Windows.Forms.Button btnservers;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btninterarrival;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Probability;
    }
}

