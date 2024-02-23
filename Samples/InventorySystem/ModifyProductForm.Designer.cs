namespace InventorySystem
{
    partial class ModifyProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyProductForm));
            this.MProdSearchTextBox = new System.Windows.Forms.TextBox();
            this.MProdSearchButton = new System.Windows.Forms.Button();
            this.MProdDGVAssocPartsLabel = new System.Windows.Forms.Label();
            this.MProdDGVPartsLabel = new System.Windows.Forms.Label();
            this.MProdSaveButton = new System.Windows.Forms.Button();
            this.MProdCancelButton = new System.Windows.Forms.Button();
            this.MProdDeleteButton = new System.Windows.Forms.Button();
            this.MProdAddButton = new System.Windows.Forms.Button();
            this.MProdDGVAssocParts = new System.Windows.Forms.DataGridView();
            this.MProdDGVParts = new System.Windows.Forms.DataGridView();
            this.MProdNameTextBox = new System.Windows.Forms.TextBox();
            this.MProdNameLabel = new System.Windows.Forms.Label();
            this.MProdMinTextBox = new System.Windows.Forms.TextBox();
            this.MProdMinLabel = new System.Windows.Forms.Label();
            this.MProdMaxTextBox = new System.Windows.Forms.TextBox();
            this.MProdMaxLabel = new System.Windows.Forms.Label();
            this.MProdPriceTextBox = new System.Windows.Forms.TextBox();
            this.MProdPriceLabel = new System.Windows.Forms.Label();
            this.MProdInventoryTextBox = new System.Windows.Forms.TextBox();
            this.MProdInventoryLabel = new System.Windows.Forms.Label();
            this.MProdIDTextBox = new System.Windows.Forms.TextBox();
            this.MProdIDLabel = new System.Windows.Forms.Label();
            this.MProdHeaderLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MProdDGVAssocParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MProdDGVParts)).BeginInit();
            this.SuspendLayout();
            // 
            // MProdSearchTextBox
            // 
            this.MProdSearchTextBox.Location = new System.Drawing.Point(725, 47);
            this.MProdSearchTextBox.Name = "MProdSearchTextBox";
            this.MProdSearchTextBox.Size = new System.Drawing.Size(190, 22);
            this.MProdSearchTextBox.TabIndex = 82;
            // 
            // MProdSearchButton
            // 
            this.MProdSearchButton.Location = new System.Drawing.Point(644, 44);
            this.MProdSearchButton.Name = "MProdSearchButton";
            this.MProdSearchButton.Size = new System.Drawing.Size(75, 29);
            this.MProdSearchButton.TabIndex = 81;
            this.MProdSearchButton.Text = "Search";
            this.MProdSearchButton.UseVisualStyleBackColor = true;
            this.MProdSearchButton.Click += new System.EventHandler(this.MProdSearchButton_Click);
            // 
            // MProdDGVAssocPartsLabel
            // 
            this.MProdDGVAssocPartsLabel.AutoSize = true;
            this.MProdDGVAssocPartsLabel.Location = new System.Drawing.Point(422, 307);
            this.MProdDGVAssocPartsLabel.Name = "MProdDGVAssocPartsLabel";
            this.MProdDGVAssocPartsLabel.Size = new System.Drawing.Size(230, 17);
            this.MProdDGVAssocPartsLabel.TabIndex = 80;
            this.MProdDGVAssocPartsLabel.Text = "Parts Associated With This Product";
            // 
            // MProdDGVPartsLabel
            // 
            this.MProdDGVPartsLabel.AutoSize = true;
            this.MProdDGVPartsLabel.Location = new System.Drawing.Point(422, 76);
            this.MProdDGVPartsLabel.Name = "MProdDGVPartsLabel";
            this.MProdDGVPartsLabel.Size = new System.Drawing.Size(128, 17);
            this.MProdDGVPartsLabel.TabIndex = 79;
            this.MProdDGVPartsLabel.Text = "All Candidate Parts";
            // 
            // MProdSaveButton
            // 
            this.MProdSaveButton.Location = new System.Drawing.Point(736, 559);
            this.MProdSaveButton.Name = "MProdSaveButton";
            this.MProdSaveButton.Size = new System.Drawing.Size(75, 29);
            this.MProdSaveButton.TabIndex = 78;
            this.MProdSaveButton.Text = "Save";
            this.MProdSaveButton.UseVisualStyleBackColor = true;
            this.MProdSaveButton.Click += new System.EventHandler(this.MProdSaveButton_Click);
            // 
            // MProdCancelButton
            // 
            this.MProdCancelButton.Location = new System.Drawing.Point(827, 559);
            this.MProdCancelButton.Name = "MProdCancelButton";
            this.MProdCancelButton.Size = new System.Drawing.Size(75, 29);
            this.MProdCancelButton.TabIndex = 77;
            this.MProdCancelButton.Text = "Cancel";
            this.MProdCancelButton.UseVisualStyleBackColor = true;
            this.MProdCancelButton.Click += new System.EventHandler(this.MProdCancelButton_Click);
            // 
            // MProdDeleteButton
            // 
            this.MProdDeleteButton.Location = new System.Drawing.Point(827, 504);
            this.MProdDeleteButton.Name = "MProdDeleteButton";
            this.MProdDeleteButton.Size = new System.Drawing.Size(75, 29);
            this.MProdDeleteButton.TabIndex = 76;
            this.MProdDeleteButton.Text = "Delete";
            this.MProdDeleteButton.UseVisualStyleBackColor = true;
            this.MProdDeleteButton.Click += new System.EventHandler(this.MProdDeleteButton_Click);
            // 
            // MProdAddButton
            // 
            this.MProdAddButton.Location = new System.Drawing.Point(827, 273);
            this.MProdAddButton.Name = "MProdAddButton";
            this.MProdAddButton.Size = new System.Drawing.Size(75, 29);
            this.MProdAddButton.TabIndex = 75;
            this.MProdAddButton.Text = "Add";
            this.MProdAddButton.UseVisualStyleBackColor = true;
            this.MProdAddButton.Click += new System.EventHandler(this.MProdAddButton_Click);
            // 
            // MProdDGVAssocParts
            // 
            this.MProdDGVAssocParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MProdDGVAssocParts.Location = new System.Drawing.Point(425, 327);
            this.MProdDGVAssocParts.Name = "MProdDGVAssocParts";
            this.MProdDGVAssocParts.ReadOnly = true;
            this.MProdDGVAssocParts.RowHeadersWidth = 51;
            this.MProdDGVAssocParts.RowTemplate.Height = 24;
            this.MProdDGVAssocParts.Size = new System.Drawing.Size(490, 171);
            this.MProdDGVAssocParts.TabIndex = 74;
            this.MProdDGVAssocParts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MProdDGVAssocParts_CellClick);
            this.MProdDGVAssocParts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MProdDGVAssocParts_CellContentClick);
            // 
            // MProdDGVParts
            // 
            this.MProdDGVParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MProdDGVParts.Location = new System.Drawing.Point(425, 96);
            this.MProdDGVParts.Name = "MProdDGVParts";
            this.MProdDGVParts.ReadOnly = true;
            this.MProdDGVParts.RowHeadersWidth = 51;
            this.MProdDGVParts.RowTemplate.Height = 24;
            this.MProdDGVParts.Size = new System.Drawing.Size(490, 171);
            this.MProdDGVParts.TabIndex = 73;
            this.MProdDGVParts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MProdDGVParts_CellClick);
            this.MProdDGVParts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MProdDGVParts_CellContentClick);
            // 
            // MProdNameTextBox
            // 
            this.MProdNameTextBox.Location = new System.Drawing.Point(116, 207);
            this.MProdNameTextBox.Name = "MProdNameTextBox";
            this.MProdNameTextBox.Size = new System.Drawing.Size(190, 22);
            this.MProdNameTextBox.TabIndex = 72;
            // 
            // MProdNameLabel
            // 
            this.MProdNameLabel.AutoSize = true;
            this.MProdNameLabel.Location = new System.Drawing.Point(65, 208);
            this.MProdNameLabel.Name = "MProdNameLabel";
            this.MProdNameLabel.Size = new System.Drawing.Size(45, 17);
            this.MProdNameLabel.TabIndex = 71;
            this.MProdNameLabel.Text = "Name";
            // 
            // MProdMinTextBox
            // 
            this.MProdMinTextBox.Location = new System.Drawing.Point(262, 347);
            this.MProdMinTextBox.Name = "MProdMinTextBox";
            this.MProdMinTextBox.Size = new System.Drawing.Size(89, 22);
            this.MProdMinTextBox.TabIndex = 70;
            this.MProdMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MProdMinTextBox_KeyPress);
            // 
            // MProdMinLabel
            // 
            this.MProdMinLabel.AutoSize = true;
            this.MProdMinLabel.Location = new System.Drawing.Point(223, 350);
            this.MProdMinLabel.Name = "MProdMinLabel";
            this.MProdMinLabel.Size = new System.Drawing.Size(30, 17);
            this.MProdMinLabel.TabIndex = 69;
            this.MProdMinLabel.Text = "Min";
            // 
            // MProdMaxTextBox
            // 
            this.MProdMaxTextBox.Location = new System.Drawing.Point(116, 347);
            this.MProdMaxTextBox.Name = "MProdMaxTextBox";
            this.MProdMaxTextBox.Size = new System.Drawing.Size(89, 22);
            this.MProdMaxTextBox.TabIndex = 68;
            this.MProdMaxTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MProdMaxTextBox_KeyPress);
            // 
            // MProdMaxLabel
            // 
            this.MProdMaxLabel.AutoSize = true;
            this.MProdMaxLabel.Location = new System.Drawing.Point(77, 350);
            this.MProdMaxLabel.Name = "MProdMaxLabel";
            this.MProdMaxLabel.Size = new System.Drawing.Size(33, 17);
            this.MProdMaxLabel.TabIndex = 67;
            this.MProdMaxLabel.Text = "Max";
            // 
            // MProdPriceTextBox
            // 
            this.MProdPriceTextBox.Location = new System.Drawing.Point(116, 301);
            this.MProdPriceTextBox.Name = "MProdPriceTextBox";
            this.MProdPriceTextBox.Size = new System.Drawing.Size(190, 22);
            this.MProdPriceTextBox.TabIndex = 66;
            this.MProdPriceTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MProdPriceTextBox_KeyPress);
            // 
            // MProdPriceLabel
            // 
            this.MProdPriceLabel.AutoSize = true;
            this.MProdPriceLabel.Location = new System.Drawing.Point(70, 304);
            this.MProdPriceLabel.Name = "MProdPriceLabel";
            this.MProdPriceLabel.Size = new System.Drawing.Size(40, 17);
            this.MProdPriceLabel.TabIndex = 65;
            this.MProdPriceLabel.Text = "Price";
            // 
            // MProdInventoryTextBox
            // 
            this.MProdInventoryTextBox.Location = new System.Drawing.Point(116, 254);
            this.MProdInventoryTextBox.Name = "MProdInventoryTextBox";
            this.MProdInventoryTextBox.Size = new System.Drawing.Size(190, 22);
            this.MProdInventoryTextBox.TabIndex = 64;
            this.MProdInventoryTextBox.TextChanged += new System.EventHandler(this.MProdInventoryTextBox_TextChanged);
            this.MProdInventoryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MProdInventoryTextBox_KeyPress);
            // 
            // MProdInventoryLabel
            // 
            this.MProdInventoryLabel.AutoSize = true;
            this.MProdInventoryLabel.Location = new System.Drawing.Point(44, 257);
            this.MProdInventoryLabel.Name = "MProdInventoryLabel";
            this.MProdInventoryLabel.Size = new System.Drawing.Size(66, 17);
            this.MProdInventoryLabel.TabIndex = 63;
            this.MProdInventoryLabel.Text = "Inventory";
            // 
            // MProdIDTextBox
            // 
            this.MProdIDTextBox.Enabled = false;
            this.MProdIDTextBox.Location = new System.Drawing.Point(116, 161);
            this.MProdIDTextBox.Name = "MProdIDTextBox";
            this.MProdIDTextBox.Size = new System.Drawing.Size(190, 22);
            this.MProdIDTextBox.TabIndex = 62;
            // 
            // MProdIDLabel
            // 
            this.MProdIDLabel.AutoSize = true;
            this.MProdIDLabel.Location = new System.Drawing.Point(89, 161);
            this.MProdIDLabel.Name = "MProdIDLabel";
            this.MProdIDLabel.Size = new System.Drawing.Size(21, 17);
            this.MProdIDLabel.TabIndex = 61;
            this.MProdIDLabel.Text = "ID";
            // 
            // MProdHeaderLabel
            // 
            this.MProdHeaderLabel.AutoSize = true;
            this.MProdHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MProdHeaderLabel.Location = new System.Drawing.Point(22, 18);
            this.MProdHeaderLabel.Name = "MProdHeaderLabel";
            this.MProdHeaderLabel.Size = new System.Drawing.Size(137, 25);
            this.MProdHeaderLabel.TabIndex = 60;
            this.MProdHeaderLabel.Text = "ModifyProduct";
            // 
            // ModifyProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 606);
            this.Controls.Add(this.MProdSearchTextBox);
            this.Controls.Add(this.MProdSearchButton);
            this.Controls.Add(this.MProdDGVAssocPartsLabel);
            this.Controls.Add(this.MProdDGVPartsLabel);
            this.Controls.Add(this.MProdSaveButton);
            this.Controls.Add(this.MProdCancelButton);
            this.Controls.Add(this.MProdDeleteButton);
            this.Controls.Add(this.MProdAddButton);
            this.Controls.Add(this.MProdDGVAssocParts);
            this.Controls.Add(this.MProdDGVParts);
            this.Controls.Add(this.MProdNameTextBox);
            this.Controls.Add(this.MProdNameLabel);
            this.Controls.Add(this.MProdMinTextBox);
            this.Controls.Add(this.MProdMinLabel);
            this.Controls.Add(this.MProdMaxTextBox);
            this.Controls.Add(this.MProdMaxLabel);
            this.Controls.Add(this.MProdPriceTextBox);
            this.Controls.Add(this.MProdPriceLabel);
            this.Controls.Add(this.MProdInventoryTextBox);
            this.Controls.Add(this.MProdInventoryLabel);
            this.Controls.Add(this.MProdIDTextBox);
            this.Controls.Add(this.MProdIDLabel);
            this.Controls.Add(this.MProdHeaderLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifyProductForm";
            this.Text = "Product";
            ((System.ComponentModel.ISupportInitialize)(this.MProdDGVAssocParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MProdDGVParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MProdSearchTextBox;
        private System.Windows.Forms.Button MProdSearchButton;
        private System.Windows.Forms.Label MProdDGVAssocPartsLabel;
        private System.Windows.Forms.Label MProdDGVPartsLabel;
        private System.Windows.Forms.Button MProdSaveButton;
        private System.Windows.Forms.Button MProdCancelButton;
        private System.Windows.Forms.Button MProdDeleteButton;
        private System.Windows.Forms.Button MProdAddButton;
        private System.Windows.Forms.DataGridView MProdDGVAssocParts;
        private System.Windows.Forms.DataGridView MProdDGVParts;
        private System.Windows.Forms.TextBox MProdNameTextBox;
        private System.Windows.Forms.Label MProdNameLabel;
        private System.Windows.Forms.TextBox MProdMinTextBox;
        private System.Windows.Forms.Label MProdMinLabel;
        private System.Windows.Forms.TextBox MProdMaxTextBox;
        private System.Windows.Forms.Label MProdMaxLabel;
        private System.Windows.Forms.TextBox MProdPriceTextBox;
        private System.Windows.Forms.Label MProdPriceLabel;
        private System.Windows.Forms.TextBox MProdInventoryTextBox;
        private System.Windows.Forms.Label MProdInventoryLabel;
        private System.Windows.Forms.TextBox MProdIDTextBox;
        private System.Windows.Forms.Label MProdIDLabel;
        private System.Windows.Forms.Label MProdHeaderLabel;
    }
}