namespace Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.categoryGrid = new System.Windows.Forms.DataGridView();
            this.categoryIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.updateButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.categoryNameInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.descriptionInput = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.imageButton = new System.Windows.Forms.Button();
            this.imagePreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.categoryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryGrid
            // 
            this.categoryGrid.AllowUserToAddRows = false;
            this.categoryGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.categoryGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.categoryGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.categoryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryIDColumn,
            this.categoryNameColumn,
            this.descriptionColumn,
            this.pictureColumn,
            this.deleteButton,
            this.updateButton});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.categoryGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.categoryGrid.Dock = System.Windows.Forms.DockStyle.Top;

            this.categoryGrid.Location = new System.Drawing.Point(0, 0);
            this.categoryGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryGrid.Name = "categoryGrid";
            this.categoryGrid.ReadOnly = true;
            this.categoryGrid.RowHeadersWidth = 51;
            this.categoryGrid.RowTemplate.Height = 100;
            this.categoryGrid.Size = new System.Drawing.Size(1074, 392);
            this.categoryGrid.TabIndex = 2;
            this.categoryGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.categoryGrid_CellContentClick);
            this.categoryGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.categoryGrid_CellContentClick_1);

            this.categoryGrid.RowsDefaultCellStyle.ForeColor = Color.Black;
            // 
            // categoryIDColumn
            // 
            this.categoryIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.categoryIDColumn.DataPropertyName = "CategoryID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.categoryIDColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.categoryIDColumn.DividerWidth = 1;
            this.categoryIDColumn.HeaderText = "Id";
            this.categoryIDColumn.MinimumWidth = 6;
            this.categoryIDColumn.Name = "categoryIDColumn";
            this.categoryIDColumn.ReadOnly = true;
            this.categoryIDColumn.Width = 43;
            // 
            // categoryNameColumn
            // 
            this.categoryNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.categoryNameColumn.DataPropertyName = "CategoryName";
            this.categoryNameColumn.DividerWidth = 1;
            this.categoryNameColumn.HeaderText = "Category Name";
            this.categoryNameColumn.MinimumWidth = 6;
            this.categoryNameColumn.Name = "categoryNameColumn";
            this.categoryNameColumn.ReadOnly = true;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionColumn.DataPropertyName = "Description";
            this.descriptionColumn.DividerWidth = 1;
            this.descriptionColumn.HeaderText = "Description";
            this.descriptionColumn.MinimumWidth = 6;
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            // 
            // pictureColumn
            // 
            this.pictureColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pictureColumn.DataPropertyName = "Picture";
            this.pictureColumn.DividerWidth = 1;
            this.pictureColumn.HeaderText = "Picture";
            this.pictureColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.pictureColumn.MinimumWidth = 6;
            this.pictureColumn.Name = "pictureColumn";
            this.pictureColumn.ReadOnly = true;
            // 
            // deleteButton
            // 
            this.deleteButton.DividerWidth = 1;
            this.deleteButton.HeaderText = "Delete";
            this.deleteButton.MinimumWidth = 6;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.ReadOnly = true;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseColumnTextForButtonValue = true;
            this.deleteButton.Width = 125;
            this.deleteButton.DefaultCellStyle.BackColor = Color.Red;
            // 
            // updateButton
            // 
            this.updateButton.DividerWidth = 1;
            this.updateButton.HeaderText = "Update";
            this.updateButton.MinimumWidth = 6;
            this.updateButton.Name = "updateButton";
            this.updateButton.ReadOnly = true;
            this.updateButton.Text = "Update";
            this.updateButton.UseColumnTextForButtonValue = true;
            this.updateButton.Width = 125;
            // 
            // categoryNameInput
            // 
            this.categoryNameInput.Location = new System.Drawing.Point(27, 468);
            this.categoryNameInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryNameInput.Name = "categoryNameInput";
            this.categoryNameInput.Size = new System.Drawing.Size(162, 23);
            this.categoryNameInput.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Category Name";
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(27, 659);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 22);
            this.button2.TabIndex = 5;
            this.button2.Text = "Add Category";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.AddCategory);
            // 
            // descriptionInput
            // 
            this.descriptionInput.Location = new System.Drawing.Point(26, 556);
            this.descriptionInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.descriptionInput.Multiline = true;
            this.descriptionInput.Name = "descriptionInput";
            this.descriptionInput.Size = new System.Drawing.Size(162, 45);
            this.descriptionInput.TabIndex = 6;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(27, 523);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(67, 15);
            this.descriptionLabel.TabIndex = 7;
            this.descriptionLabel.Text = "Description";
            // 
            // imageButton
            // 
            this.imageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageButton.Location = new System.Drawing.Point(27, 618);
            this.imageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageButton.Name = "imageButton";
            this.imageButton.Size = new System.Drawing.Size(82, 22);
            this.imageButton.TabIndex = 8;
            this.imageButton.Text = "Add Image";
            this.imageButton.UseVisualStyleBackColor = true;
            this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
            // 
            // imagePreview
            // 
            this.imagePreview.Location = new System.Drawing.Point(252, 436);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(356, 210);
            this.imagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagePreview.TabIndex = 9;
            this.imagePreview.TabStop = false;
            this.imagePreview.Click += new System.EventHandler(this.imagePreview_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(1074, 942);
            this.Controls.Add(this.imagePreview);
            this.Controls.Add(this.imageButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.descriptionInput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.categoryNameInput);
            this.Controls.Add(this.categoryGrid);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontDialog fontDialog1;
        private Button button1;
        private Label label1;
        private DataGridView categoryGrid;
        private TextBox categoryNameInput;
        private Label label2;
        private Button button2;
        private TextBox descriptionInput;
        private Label descriptionLabel;
        private Button imageButton;
        private PictureBox imagePreview;
        private DataGridViewTextBoxColumn categoryIDColumn;
        private DataGridViewTextBoxColumn categoryNameColumn;
        private DataGridViewTextBoxColumn descriptionColumn;
        private DataGridViewImageColumn pictureColumn;
        private DataGridViewButtonColumn deleteButton;
        private DataGridViewButtonColumn updateButton;
    }
}