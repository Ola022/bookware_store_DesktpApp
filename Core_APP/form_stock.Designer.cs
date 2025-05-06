namespace cosmesticClinic.Core_APP
{
    partial class form_stock
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
            this.panDgv = new System.Windows.Forms.Panel();
            this.lblClinicName = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.num_sellPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.num_costPrice = new System.Windows.Forms.NumericUpDown();
            this.cmb_category = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_description = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_author = new System.Windows.Forms.TextBox();
            this.txt_title = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sellPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_costPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // panDgv
            // 
            this.panDgv.AllowDrop = true;
            this.panDgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panDgv.AutoScroll = true;
            this.panDgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panDgv.Controls.Add(this.lblClinicName);
            this.panDgv.Controls.Add(this.dataGridView1);
            this.panDgv.Location = new System.Drawing.Point(158, 272);
            this.panDgv.Name = "panDgv";
            this.panDgv.Size = new System.Drawing.Size(945, 254);
            this.panDgv.TabIndex = 12;
            // 
            // lblClinicName
            // 
            this.lblClinicName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClinicName.AutoSize = true;
            this.lblClinicName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblClinicName.Location = new System.Drawing.Point(385, 2);
            this.lblClinicName.Name = "lblClinicName";
            this.lblClinicName.Size = new System.Drawing.Size(151, 21);
            this.lblClinicName.TabIndex = 67;
            this.lblClinicName.Text = "ALL PRODUCT LIST";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(935, 191);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnDelete);
            this.panel4.Controls.Add(this.btnUpdate);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Location = new System.Drawing.Point(933, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(170, 222);
            this.panel4.TabIndex = 10;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(17, 163);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(133, 42);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Location = new System.Drawing.Point(17, 89);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(133, 42);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(17, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 42);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Add Book";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.numQty);
            this.panel2.Controls.Add(this.num_sellPrice);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.num_costPrice);
            this.panel2.Controls.Add(this.cmb_category);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txt_description);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txt_author);
            this.panel2.Controls.Add(this.txt_title);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(158, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(749, 222);
            this.panel2.TabIndex = 9;
            // 
            // numQty
            // 
            this.numQty.Location = new System.Drawing.Point(593, 119);
            this.numQty.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(119, 23);
            this.numQty.TabIndex = 83;
            // 
            // num_sellPrice
            // 
            this.num_sellPrice.Location = new System.Drawing.Point(370, 119);
            this.num_sellPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.num_sellPrice.Name = "num_sellPrice";
            this.num_sellPrice.Size = new System.Drawing.Size(119, 23);
            this.num_sellPrice.TabIndex = 82;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 81;
            this.label6.Text = "Selling Price";
            // 
            // num_costPrice
            // 
            this.num_costPrice.Location = new System.Drawing.Point(139, 119);
            this.num_costPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.num_costPrice.Name = "num_costPrice";
            this.num_costPrice.Size = new System.Drawing.Size(119, 23);
            this.num_costPrice.TabIndex = 80;
            // 
            // cmb_category
            // 
            this.cmb_category.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_category.FormattingEnabled = true;
            this.cmb_category.Items.AddRange(new object[] {
            "Single",
            "Dating",
            "Married",
            "Divorce"});
            this.cmb_category.Location = new System.Drawing.Point(137, 72);
            this.cmb_category.Name = "cmb_category";
            this.cmb_category.Size = new System.Drawing.Size(236, 23);
            this.cmb_category.TabIndex = 78;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Description";
            // 
            // txt_description
            // 
            this.txt_description.Location = new System.Drawing.Point(137, 163);
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(575, 52);
            this.txt_description.TabIndex = 17;
            this.txt_description.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Quantity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Cost Price";
            // 
            // txt_author
            // 
            this.txt_author.BackColor = System.Drawing.SystemColors.Control;
            this.txt_author.Location = new System.Drawing.Point(486, 68);
            this.txt_author.Name = "txt_author";
            this.txt_author.Size = new System.Drawing.Size(226, 23);
            this.txt_author.TabIndex = 10;
            // 
            // txt_title
            // 
            this.txt_title.BackColor = System.Drawing.SystemColors.Control;
            this.txt_title.Location = new System.Drawing.Point(137, 27);
            this.txt_title.Name = "txt_title";
            this.txt_title.Size = new System.Drawing.Size(575, 23);
            this.txt_title.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(421, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Author";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Category";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Book Titles";
            // 
            // form_stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 556);
            this.Controls.Add(this.panDgv);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Name = "form_stock";
            this.Text = "form_stock";
            this.Load += new System.EventHandler(this.form_stock_Load);
            this.panDgv.ResumeLayout(false);
            this.panDgv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sellPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_costPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panDgv;
        private Label lblClinicName;
        private DataGridView dataGridView1;
        private Panel panel4;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnSave;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private TextBox txt_author;
        private TextBox txt_title;
        private Label label7;
        private Label label4;
        private Label label3;
        private Label label5;
        private RichTextBox txt_description;
        private ComboBox cmb_category;
        private NumericUpDown num_costPrice;
        private NumericUpDown numQty;
        private NumericUpDown num_sellPrice;
        private Label label6;
    }
}