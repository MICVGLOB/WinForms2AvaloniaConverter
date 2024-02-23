namespace InventorySystem
{
    partial class Mainscreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainscreen));
            this.MSDGVParts = new System.Windows.Forms.DataGridView();
            this.MSDGVProducts = new System.Windows.Forms.DataGridView();
            this.MSHeaderLabel = new System.Windows.Forms.Label();
            this.MSPartsSearchBox = new System.Windows.Forms.TextBox();
            this.MSProductSearchBox = new System.Windows.Forms.TextBox();
            this.MSSearchPartsButton = new System.Windows.Forms.Button();
            this.MSProductSearchButton = new System.Windows.Forms.Button();
            this.MSDGVPartsLabel = new System.Windows.Forms.Label();
            this.MSDGVProductsLabel = new System.Windows.Forms.Label();
            this.MSAddPartButton = new System.Windows.Forms.Button();
            this.MSAddProductButton = new System.Windows.Forms.Button();
            this.MSModifyPartButton = new System.Windows.Forms.Button();
            this.MSModifyProductButton = new System.Windows.Forms.Button();
            this.MSDeletePartButton = new System.Windows.Forms.Button();
            this.MSDeleteProductButton = new System.Windows.Forms.Button();
            this.MSExitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MSDGVParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSDGVProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // MSDGVParts
            // 
            this.MSDGVParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MSDGVParts.Location = new System.Drawing.Point(23, 167);
            this.MSDGVParts.Name = "MSDGVParts";
            this.MSDGVParts.ReadOnly = true;
            this.MSDGVParts.RowHeadersWidth = 51;
            this.MSDGVParts.RowTemplate.Height = 24;
            this.MSDGVParts.Size = new System.Drawing.Size(490, 250);
            this.MSDGVParts.TabIndex = 0;
            this.MSDGVParts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVCellClick);
            this.MSDGVParts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MSDGVParts_CellContentClick);
            // 
            // MSDGVProducts
            // 
            this.MSDGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MSDGVProducts.Location = new System.Drawing.Point(545, 167);
            this.MSDGVProducts.Name = "MSDGVProducts";
            this.MSDGVProducts.ReadOnly = true;
            this.MSDGVProducts.RowHeadersWidth = 51;
            this.MSDGVProducts.RowTemplate.Height = 24;
            this.MSDGVProducts.Size = new System.Drawing.Size(490, 250);
            this.MSDGVProducts.TabIndex = 1;
            this.MSDGVProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProd_CellClick);
            // 
            // MSHeaderLabel
            // 
            this.MSHeaderLabel.AutoSize = true;
            this.MSHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSHeaderLabel.Location = new System.Drawing.Point(12, 18);
            this.MSHeaderLabel.Name = "MSHeaderLabel";
            this.MSHeaderLabel.Size = new System.Drawing.Size(340, 29);
            this.MSHeaderLabel.TabIndex = 2;
            this.MSHeaderLabel.Text = "Inventory Management System";
            // 
            // MSPartsSearchBox
            // 
            this.MSPartsSearchBox.Location = new System.Drawing.Point(327, 114);
            this.MSPartsSearchBox.Name = "MSPartsSearchBox";
            this.MSPartsSearchBox.Size = new System.Drawing.Size(186, 22);
            this.MSPartsSearchBox.TabIndex = 3;
            this.MSPartsSearchBox.TextChanged += new System.EventHandler(this.MSPartsSearchBox_TextChanged);
            // 
            // MSProductSearchBox
            // 
            this.MSProductSearchBox.Location = new System.Drawing.Point(849, 114);
            this.MSProductSearchBox.Name = "MSProductSearchBox";
            this.MSProductSearchBox.Size = new System.Drawing.Size(186, 22);
            this.MSProductSearchBox.TabIndex = 4;
            this.MSProductSearchBox.TextChanged += new System.EventHandler(this.MSProductSearchBox_TextChanged);
            // 
            // MSSearchPartsButton
            // 
            this.MSSearchPartsButton.Location = new System.Drawing.Point(246, 109);
            this.MSSearchPartsButton.Name = "MSSearchPartsButton";
            this.MSSearchPartsButton.Size = new System.Drawing.Size(75, 32);
            this.MSSearchPartsButton.TabIndex = 5;
            this.MSSearchPartsButton.Text = "Search";
            this.MSSearchPartsButton.UseVisualStyleBackColor = true;
            this.MSSearchPartsButton.Click += new System.EventHandler(this.MSSearchPartsButton_Click);
            // 
            // MSProductSearchButton
            // 
            this.MSProductSearchButton.Location = new System.Drawing.Point(768, 109);
            this.MSProductSearchButton.Name = "MSProductSearchButton";
            this.MSProductSearchButton.Size = new System.Drawing.Size(75, 32);
            this.MSProductSearchButton.TabIndex = 6;
            this.MSProductSearchButton.Text = "Search";
            this.MSProductSearchButton.UseVisualStyleBackColor = true;
            this.MSProductSearchButton.Click += new System.EventHandler(this.MSProductSearchButton_Click);
            // 
            // MSDGVPartsLabel
            // 
            this.MSDGVPartsLabel.AutoSize = true;
            this.MSDGVPartsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSDGVPartsLabel.Location = new System.Drawing.Point(19, 144);
            this.MSDGVPartsLabel.Name = "MSDGVPartsLabel";
            this.MSDGVPartsLabel.Size = new System.Drawing.Size(49, 20);
            this.MSDGVPartsLabel.TabIndex = 7;
            this.MSDGVPartsLabel.Text = "Parts";
            // 
            // MSDGVProductsLabel
            // 
            this.MSDGVProductsLabel.AutoSize = true;
            this.MSDGVProductsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSDGVProductsLabel.Location = new System.Drawing.Point(541, 144);
            this.MSDGVProductsLabel.Name = "MSDGVProductsLabel";
            this.MSDGVProductsLabel.Size = new System.Drawing.Size(76, 20);
            this.MSDGVProductsLabel.TabIndex = 8;
            this.MSDGVProductsLabel.Text = "Products";
            // 
            // MSAddPartButton
            // 
            this.MSAddPartButton.Location = new System.Drawing.Point(276, 435);
            this.MSAddPartButton.Name = "MSAddPartButton";
            this.MSAddPartButton.Size = new System.Drawing.Size(75, 30);
            this.MSAddPartButton.TabIndex = 9;
            this.MSAddPartButton.Text = "Add";
            this.MSAddPartButton.UseVisualStyleBackColor = true;
            this.MSAddPartButton.Click += new System.EventHandler(this.MSAddPartButton_Click);
            // 
            // MSAddProductButton
            // 
            this.MSAddProductButton.Location = new System.Drawing.Point(798, 435);
            this.MSAddProductButton.Name = "MSAddProductButton";
            this.MSAddProductButton.Size = new System.Drawing.Size(75, 30);
            this.MSAddProductButton.TabIndex = 10;
            this.MSAddProductButton.Text = "Add";
            this.MSAddProductButton.UseVisualStyleBackColor = true;
            this.MSAddProductButton.Click += new System.EventHandler(this.MSAddProductButton_Click);
            // 
            // MSModifyPartButton
            // 
            this.MSModifyPartButton.Location = new System.Drawing.Point(357, 435);
            this.MSModifyPartButton.Name = "MSModifyPartButton";
            this.MSModifyPartButton.Size = new System.Drawing.Size(75, 30);
            this.MSModifyPartButton.TabIndex = 11;
            this.MSModifyPartButton.Text = "Modify";
            this.MSModifyPartButton.UseVisualStyleBackColor = true;
            this.MSModifyPartButton.Click += new System.EventHandler(this.MSModifyPartButton_Click);
            // 
            // MSModifyProductButton
            // 
            this.MSModifyProductButton.Location = new System.Drawing.Point(879, 435);
            this.MSModifyProductButton.Name = "MSModifyProductButton";
            this.MSModifyProductButton.Size = new System.Drawing.Size(75, 30);
            this.MSModifyProductButton.TabIndex = 12;
            this.MSModifyProductButton.Text = "Modify";
            this.MSModifyProductButton.UseVisualStyleBackColor = true;
            this.MSModifyProductButton.Click += new System.EventHandler(this.MSModifyProductButton_Click);
            // 
            // MSDeletePartButton
            // 
            this.MSDeletePartButton.Location = new System.Drawing.Point(438, 435);
            this.MSDeletePartButton.Name = "MSDeletePartButton";
            this.MSDeletePartButton.Size = new System.Drawing.Size(75, 30);
            this.MSDeletePartButton.TabIndex = 13;
            this.MSDeletePartButton.Text = "Delete";
            this.MSDeletePartButton.UseVisualStyleBackColor = true;
            this.MSDeletePartButton.Click += new System.EventHandler(this.MSDeletePartButton_Click);
            // 
            // MSDeleteProductButton
            // 
            this.MSDeleteProductButton.Location = new System.Drawing.Point(960, 435);
            this.MSDeleteProductButton.Name = "MSDeleteProductButton";
            this.MSDeleteProductButton.Size = new System.Drawing.Size(75, 30);
            this.MSDeleteProductButton.TabIndex = 14;
            this.MSDeleteProductButton.Text = "Delete";
            this.MSDeleteProductButton.UseVisualStyleBackColor = true;
            this.MSDeleteProductButton.Click += new System.EventHandler(this.MSDeleteProductButton_Click);
            // 
            // MSExitButton
            // 
            this.MSExitButton.Location = new System.Drawing.Point(960, 504);
            this.MSExitButton.Name = "MSExitButton";
            this.MSExitButton.Size = new System.Drawing.Size(75, 32);
            this.MSExitButton.TabIndex = 15;
            this.MSExitButton.Text = "Exit";
            this.MSExitButton.UseVisualStyleBackColor = true;
            this.MSExitButton.Click += new System.EventHandler(this.MSExitButton_Click);
            // 
            // Mainscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 568);
            this.Controls.Add(this.MSExitButton);
            this.Controls.Add(this.MSDeleteProductButton);
            this.Controls.Add(this.MSDeletePartButton);
            this.Controls.Add(this.MSModifyProductButton);
            this.Controls.Add(this.MSModifyPartButton);
            this.Controls.Add(this.MSAddProductButton);
            this.Controls.Add(this.MSAddPartButton);
            this.Controls.Add(this.MSDGVProductsLabel);
            this.Controls.Add(this.MSDGVPartsLabel);
            this.Controls.Add(this.MSProductSearchButton);
            this.Controls.Add(this.MSSearchPartsButton);
            this.Controls.Add(this.MSProductSearchBox);
            this.Controls.Add(this.MSPartsSearchBox);
            this.Controls.Add(this.MSHeaderLabel);
            this.Controls.Add(this.MSDGVProducts);
            this.Controls.Add(this.MSDGVParts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Mainscreen";
            this.Text = "Main Screen";
            this.Load += new System.EventHandler(this.Mainscreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MSDGVParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSDGVProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView MSDGVParts;
        private System.Windows.Forms.DataGridView MSDGVProducts;
        private System.Windows.Forms.Label MSHeaderLabel;
        private System.Windows.Forms.TextBox MSPartsSearchBox;
        private System.Windows.Forms.TextBox MSProductSearchBox;
        private System.Windows.Forms.Button MSSearchPartsButton;
        private System.Windows.Forms.Button MSProductSearchButton;
        private System.Windows.Forms.Label MSDGVPartsLabel;
        private System.Windows.Forms.Label MSDGVProductsLabel;
        private System.Windows.Forms.Button MSAddPartButton;
        private System.Windows.Forms.Button MSAddProductButton;
        private System.Windows.Forms.Button MSModifyPartButton;
        private System.Windows.Forms.Button MSModifyProductButton;
        private System.Windows.Forms.Button MSDeletePartButton;
        private System.Windows.Forms.Button MSDeleteProductButton;
        private System.Windows.Forms.Button MSExitButton;
    }
}

