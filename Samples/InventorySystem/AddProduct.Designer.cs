namespace InventorySystem
{
    partial class AddProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProductForm));
            this.AProdHeaderLabel = new System.Windows.Forms.Label();
            this.AProdNameTextBox = new System.Windows.Forms.TextBox();
            this.AProdNameLabel = new System.Windows.Forms.Label();
            this.AProdMinTextBox = new System.Windows.Forms.TextBox();
            this.AProdMinLabel = new System.Windows.Forms.Label();
            this.AProdMaxTextBox = new System.Windows.Forms.TextBox();
            this.AProdMaxLabel = new System.Windows.Forms.Label();
            this.AProdPriceTextBox = new System.Windows.Forms.TextBox();
            this.AProdPriceLabel = new System.Windows.Forms.Label();
            this.AProdInventoryTextBox = new System.Windows.Forms.TextBox();
            this.AProdInventoryLabel = new System.Windows.Forms.Label();
            this.AProdIDTextBox = new System.Windows.Forms.TextBox();
            this.AProdIDLabel = new System.Windows.Forms.Label();
            this.AProdDGVParts = new System.Windows.Forms.DataGridView();
            this.AProdDGVAssocParts = new System.Windows.Forms.DataGridView();
            this.AProdAddButton = new System.Windows.Forms.Button();
            this.AProdDeleteButton = new System.Windows.Forms.Button();
            this.AProdCancelButton = new System.Windows.Forms.Button();
            this.AProdSaveButton = new System.Windows.Forms.Button();
            this.AProdDGVPartsLabel = new System.Windows.Forms.Label();
            this.MProdDGVAssocPartsLabel = new System.Windows.Forms.Label();
            this.AProdSearchButton = new System.Windows.Forms.Button();
            this.AProdSearchTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.AProdDGVParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AProdDGVAssocParts)).BeginInit();
            this.SuspendLayout();
            // 
            // AProdHeaderLabel
            // 
            this.AProdHeaderLabel.AutoSize = true;
            this.AProdHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AProdHeaderLabel.Location = new System.Drawing.Point(13, 13);
            this.AProdHeaderLabel.Name = "AProdHeaderLabel";
            this.AProdHeaderLabel.Size = new System.Drawing.Size(120, 25);
            this.AProdHeaderLabel.TabIndex = 0;
            this.AProdHeaderLabel.Text = "Add Product";
            // 
            // AProdNameTextBox
            // 
            this.AProdNameTextBox.Location = new System.Drawing.Point(107, 202);
            this.AProdNameTextBox.Name = "AProdNameTextBox";
            this.AProdNameTextBox.Size = new System.Drawing.Size(190, 22);
            this.AProdNameTextBox.TabIndex = 49;
            // 
            // AProdNameLabel
            // 
            this.AProdNameLabel.AutoSize = true;
            this.AProdNameLabel.Location = new System.Drawing.Point(56, 203);
            this.AProdNameLabel.Name = "AProdNameLabel";
            this.AProdNameLabel.Size = new System.Drawing.Size(45, 17);
            this.AProdNameLabel.TabIndex = 48;
            this.AProdNameLabel.Text = "Name";
            // 
            // AProdMinTextBox
            // 
            this.AProdMinTextBox.Location = new System.Drawing.Point(253, 342);
            this.AProdMinTextBox.Name = "AProdMinTextBox";
            this.AProdMinTextBox.Size = new System.Drawing.Size(89, 22);
            this.AProdMinTextBox.TabIndex = 47;
            this.AProdMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AProdMinTextBox_KeyPress);
            // 
            // AProdMinLabel
            // 
            this.AProdMinLabel.AutoSize = true;
            this.AProdMinLabel.Location = new System.Drawing.Point(214, 345);
            this.AProdMinLabel.Name = "AProdMinLabel";
            this.AProdMinLabel.Size = new System.Drawing.Size(30, 17);
            this.AProdMinLabel.TabIndex = 46;
            this.AProdMinLabel.Text = "Min";
            // 
            // AProdMaxTextBox
            // 
            this.AProdMaxTextBox.Location = new System.Drawing.Point(107, 342);
            this.AProdMaxTextBox.Name = "AProdMaxTextBox";
            this.AProdMaxTextBox.Size = new System.Drawing.Size(89, 22);
            this.AProdMaxTextBox.TabIndex = 45;
            this.AProdMaxTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AProdMaxTextBox_KeyPress);
            // 
            // AProdMaxLabel
            // 
            this.AProdMaxLabel.AutoSize = true;
            this.AProdMaxLabel.Location = new System.Drawing.Point(68, 345);
            this.AProdMaxLabel.Name = "AProdMaxLabel";
            this.AProdMaxLabel.Size = new System.Drawing.Size(33, 17);
            this.AProdMaxLabel.TabIndex = 44;
            this.AProdMaxLabel.Text = "Max";
            // 
            // AProdPriceTextBox
            // 
            this.AProdPriceTextBox.Location = new System.Drawing.Point(107, 296);
            this.AProdPriceTextBox.Name = "AProdPriceTextBox";
            this.AProdPriceTextBox.Size = new System.Drawing.Size(190, 22);
            this.AProdPriceTextBox.TabIndex = 43;
            this.AProdPriceTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AProdPriceTextBox_KeyPress);
            // 
            // AProdPriceLabel
            // 
            this.AProdPriceLabel.AutoSize = true;
            this.AProdPriceLabel.Location = new System.Drawing.Point(61, 299);
            this.AProdPriceLabel.Name = "AProdPriceLabel";
            this.AProdPriceLabel.Size = new System.Drawing.Size(40, 17);
            this.AProdPriceLabel.TabIndex = 42;
            this.AProdPriceLabel.Text = "Price";
            // 
            // AProdInventoryTextBox
            // 
            this.AProdInventoryTextBox.Location = new System.Drawing.Point(107, 249);
            this.AProdInventoryTextBox.Name = "AProdInventoryTextBox";
            this.AProdInventoryTextBox.Size = new System.Drawing.Size(190, 22);
            this.AProdInventoryTextBox.TabIndex = 41;
            this.AProdInventoryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AProdInventoryTextBox_KeyPress);
            // 
            // AProdInventoryLabel
            // 
            this.AProdInventoryLabel.AutoSize = true;
            this.AProdInventoryLabel.Location = new System.Drawing.Point(35, 252);
            this.AProdInventoryLabel.Name = "AProdInventoryLabel";
            this.AProdInventoryLabel.Size = new System.Drawing.Size(66, 17);
            this.AProdInventoryLabel.TabIndex = 40;
            this.AProdInventoryLabel.Text = "Inventory";
            // 
            // AProdIDTextBox
            // 
            this.AProdIDTextBox.Enabled = false;
            this.AProdIDTextBox.Location = new System.Drawing.Point(107, 156);
            this.AProdIDTextBox.Name = "AProdIDTextBox";
            this.AProdIDTextBox.Size = new System.Drawing.Size(190, 22);
            this.AProdIDTextBox.TabIndex = 39;
            // 
            // AProdIDLabel
            // 
            this.AProdIDLabel.AutoSize = true;
            this.AProdIDLabel.Location = new System.Drawing.Point(80, 156);
            this.AProdIDLabel.Name = "AProdIDLabel";
            this.AProdIDLabel.Size = new System.Drawing.Size(21, 17);
            this.AProdIDLabel.TabIndex = 38;
            this.AProdIDLabel.Text = "ID";
            // 
            // AProdDGVParts
            // 
            this.AProdDGVParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AProdDGVParts.Location = new System.Drawing.Point(416, 91);
            this.AProdDGVParts.Name = "AProdDGVParts";
            this.AProdDGVParts.ReadOnly = true;
            this.AProdDGVParts.RowHeadersWidth = 51;
            this.AProdDGVParts.RowTemplate.Height = 24;
            this.AProdDGVParts.Size = new System.Drawing.Size(490, 171);
            this.AProdDGVParts.TabIndex = 50;
            this.AProdDGVParts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AProdDGVParts_CellClick);
            this.AProdDGVParts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AProdDGVParts_CellContentClick);
            // 
            // AProdDGVAssocParts
            // 
            this.AProdDGVAssocParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AProdDGVAssocParts.Location = new System.Drawing.Point(416, 322);
            this.AProdDGVAssocParts.Name = "AProdDGVAssocParts";
            this.AProdDGVAssocParts.ReadOnly = true;
            this.AProdDGVAssocParts.RowHeadersWidth = 51;
            this.AProdDGVAssocParts.RowTemplate.Height = 24;
            this.AProdDGVAssocParts.Size = new System.Drawing.Size(490, 171);
            this.AProdDGVAssocParts.TabIndex = 51;
            this.AProdDGVAssocParts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AProdDGVAssocParts_CellClick);
            this.AProdDGVAssocParts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AProdDGVAssocParts_CellContentClick);
            // 
            // AProdAddButton
            // 
            this.AProdAddButton.Location = new System.Drawing.Point(818, 268);
            this.AProdAddButton.Name = "AProdAddButton";
            this.AProdAddButton.Size = new System.Drawing.Size(75, 29);
            this.AProdAddButton.TabIndex = 52;
            this.AProdAddButton.Text = "Add";
            this.AProdAddButton.UseVisualStyleBackColor = true;
            this.AProdAddButton.Click += new System.EventHandler(this.AProdAddButton_Click);
            // 
            // AProdDeleteButton
            // 
            this.AProdDeleteButton.Location = new System.Drawing.Point(818, 499);
            this.AProdDeleteButton.Name = "AProdDeleteButton";
            this.AProdDeleteButton.Size = new System.Drawing.Size(75, 29);
            this.AProdDeleteButton.TabIndex = 53;
            this.AProdDeleteButton.Text = "Delete";
            this.AProdDeleteButton.UseVisualStyleBackColor = true;
            this.AProdDeleteButton.Click += new System.EventHandler(this.AProdDeleteButton_Click);
            // 
            // AProdCancelButton
            // 
            this.AProdCancelButton.Location = new System.Drawing.Point(818, 554);
            this.AProdCancelButton.Name = "AProdCancelButton";
            this.AProdCancelButton.Size = new System.Drawing.Size(75, 29);
            this.AProdCancelButton.TabIndex = 54;
            this.AProdCancelButton.Text = "Cancel";
            this.AProdCancelButton.UseVisualStyleBackColor = true;
            this.AProdCancelButton.Click += new System.EventHandler(this.AProdCancelButton_Click);
            // 
            // AProdSaveButton
            // 
            this.AProdSaveButton.Location = new System.Drawing.Point(727, 554);
            this.AProdSaveButton.Name = "AProdSaveButton";
            this.AProdSaveButton.Size = new System.Drawing.Size(75, 29);
            this.AProdSaveButton.TabIndex = 55;
            this.AProdSaveButton.Text = "Save";
            this.AProdSaveButton.UseVisualStyleBackColor = true;
            this.AProdSaveButton.Click += new System.EventHandler(this.AProdSaveButton_Click);
            // 
            // AProdDGVPartsLabel
            // 
            this.AProdDGVPartsLabel.AutoSize = true;
            this.AProdDGVPartsLabel.Location = new System.Drawing.Point(413, 71);
            this.AProdDGVPartsLabel.Name = "AProdDGVPartsLabel";
            this.AProdDGVPartsLabel.Size = new System.Drawing.Size(128, 17);
            this.AProdDGVPartsLabel.TabIndex = 56;
            this.AProdDGVPartsLabel.Text = "All Candidate Parts";
            // 
            // MProdDGVAssocPartsLabel
            // 
            this.MProdDGVAssocPartsLabel.AutoSize = true;
            this.MProdDGVAssocPartsLabel.Location = new System.Drawing.Point(413, 302);
            this.MProdDGVAssocPartsLabel.Name = "MProdDGVAssocPartsLabel";
            this.MProdDGVAssocPartsLabel.Size = new System.Drawing.Size(230, 17);
            this.MProdDGVAssocPartsLabel.TabIndex = 57;
            this.MProdDGVAssocPartsLabel.Text = "Parts Associated With This Product";
            // 
            // AProdSearchButton
            // 
            this.AProdSearchButton.Location = new System.Drawing.Point(635, 39);
            this.AProdSearchButton.Name = "AProdSearchButton";
            this.AProdSearchButton.Size = new System.Drawing.Size(75, 29);
            this.AProdSearchButton.TabIndex = 58;
            this.AProdSearchButton.Text = "Search";
            this.AProdSearchButton.UseVisualStyleBackColor = true;
            this.AProdSearchButton.Click += new System.EventHandler(this.AProdSearchButton_Click);
            // 
            // AProdSearchTextBox
            // 
            this.AProdSearchTextBox.Location = new System.Drawing.Point(716, 42);
            this.AProdSearchTextBox.Name = "AProdSearchTextBox";
            this.AProdSearchTextBox.Size = new System.Drawing.Size(190, 22);
            this.AProdSearchTextBox.TabIndex = 59;
            // 
            // AddProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 606);
            this.Controls.Add(this.AProdSearchTextBox);
            this.Controls.Add(this.AProdSearchButton);
            this.Controls.Add(this.MProdDGVAssocPartsLabel);
            this.Controls.Add(this.AProdDGVPartsLabel);
            this.Controls.Add(this.AProdSaveButton);
            this.Controls.Add(this.AProdCancelButton);
            this.Controls.Add(this.AProdDeleteButton);
            this.Controls.Add(this.AProdAddButton);
            this.Controls.Add(this.AProdDGVAssocParts);
            this.Controls.Add(this.AProdDGVParts);
            this.Controls.Add(this.AProdNameTextBox);
            this.Controls.Add(this.AProdNameLabel);
            this.Controls.Add(this.AProdMinTextBox);
            this.Controls.Add(this.AProdMinLabel);
            this.Controls.Add(this.AProdMaxTextBox);
            this.Controls.Add(this.AProdMaxLabel);
            this.Controls.Add(this.AProdPriceTextBox);
            this.Controls.Add(this.AProdPriceLabel);
            this.Controls.Add(this.AProdInventoryTextBox);
            this.Controls.Add(this.AProdInventoryLabel);
            this.Controls.Add(this.AProdIDTextBox);
            this.Controls.Add(this.AProdIDLabel);
            this.Controls.Add(this.AProdHeaderLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddProductForm";
            this.Text = "Product";
            ((System.ComponentModel.ISupportInitialize)(this.AProdDGVParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AProdDGVAssocParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AProdHeaderLabel;
        private System.Windows.Forms.TextBox AProdNameTextBox;
        private System.Windows.Forms.Label AProdNameLabel;
        private System.Windows.Forms.TextBox AProdMinTextBox;
        private System.Windows.Forms.Label AProdMinLabel;
        private System.Windows.Forms.TextBox AProdMaxTextBox;
        private System.Windows.Forms.Label AProdMaxLabel;
        private System.Windows.Forms.TextBox AProdPriceTextBox;
        private System.Windows.Forms.Label AProdPriceLabel;
        private System.Windows.Forms.TextBox AProdInventoryTextBox;
        private System.Windows.Forms.Label AProdInventoryLabel;
        private System.Windows.Forms.TextBox AProdIDTextBox;
        private System.Windows.Forms.Label AProdIDLabel;
        private System.Windows.Forms.DataGridView AProdDGVParts;
        private System.Windows.Forms.DataGridView AProdDGVAssocParts;
        private System.Windows.Forms.Button AProdAddButton;
        private System.Windows.Forms.Button AProdDeleteButton;
        private System.Windows.Forms.Button AProdCancelButton;
        private System.Windows.Forms.Button AProdSaveButton;
        private System.Windows.Forms.Label AProdDGVPartsLabel;
        private System.Windows.Forms.Label MProdDGVAssocPartsLabel;
        private System.Windows.Forms.Button AProdSearchButton;
        private System.Windows.Forms.TextBox AProdSearchTextBox;
    }
}