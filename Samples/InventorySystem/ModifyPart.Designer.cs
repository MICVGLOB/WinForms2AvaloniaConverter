namespace InventorySystem
{
    partial class ModifyPartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyPartForm));
            this.MPNameTextBox = new System.Windows.Forms.TextBox();
            this.MPNameLabel = new System.Windows.Forms.Label();
            this.MPCancelButton = new System.Windows.Forms.Button();
            this.MPSaveButton = new System.Windows.Forms.Button();
            this.MPMachineIDTextBox = new System.Windows.Forms.TextBox();
            this.MPMachineIDLabel = new System.Windows.Forms.Label();
            this.MPMinTextBox = new System.Windows.Forms.TextBox();
            this.MPMinLabel = new System.Windows.Forms.Label();
            this.MPMaxTextBox = new System.Windows.Forms.TextBox();
            this.MPMaxLabel = new System.Windows.Forms.Label();
            this.MPPriceTextBox = new System.Windows.Forms.TextBox();
            this.MPPriceLabel = new System.Windows.Forms.Label();
            this.MPInventoryTextBox = new System.Windows.Forms.TextBox();
            this.MPInventoryLabel = new System.Windows.Forms.Label();
            this.MPIDTextBox = new System.Windows.Forms.TextBox();
            this.MPIDLabel = new System.Windows.Forms.Label();
            this.MPOutsourcedRadio = new System.Windows.Forms.RadioButton();
            this.MPInhouseRadio = new System.Windows.Forms.RadioButton();
            this.MPHeaderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MPNameTextBox
            // 
            this.MPNameTextBox.Location = new System.Drawing.Point(139, 171);
            this.MPNameTextBox.Name = "MPNameTextBox";
            this.MPNameTextBox.Size = new System.Drawing.Size(190, 22);
            this.MPNameTextBox.TabIndex = 37;
            this.MPNameTextBox.TextChanged += new System.EventHandler(this.MPNameTextBox_TextChanged);
            // 
            // MPNameLabel
            // 
            this.MPNameLabel.AutoSize = true;
            this.MPNameLabel.Location = new System.Drawing.Point(88, 172);
            this.MPNameLabel.Name = "MPNameLabel";
            this.MPNameLabel.Size = new System.Drawing.Size(45, 17);
            this.MPNameLabel.TabIndex = 36;
            this.MPNameLabel.Text = "Name";
            // 
            // MPCancelButton
            // 
            this.MPCancelButton.Location = new System.Drawing.Point(316, 427);
            this.MPCancelButton.Name = "MPCancelButton";
            this.MPCancelButton.Size = new System.Drawing.Size(75, 38);
            this.MPCancelButton.TabIndex = 35;
            this.MPCancelButton.Text = "Cancel";
            this.MPCancelButton.UseVisualStyleBackColor = true;
            this.MPCancelButton.Click += new System.EventHandler(this.MPCancelButton_Click);
            // 
            // MPSaveButton
            // 
            this.MPSaveButton.Location = new System.Drawing.Point(221, 427);
            this.MPSaveButton.Name = "MPSaveButton";
            this.MPSaveButton.Size = new System.Drawing.Size(75, 38);
            this.MPSaveButton.TabIndex = 34;
            this.MPSaveButton.Text = "Save";
            this.MPSaveButton.UseVisualStyleBackColor = true;
            this.MPSaveButton.Click += new System.EventHandler(this.MPSaveButton_Click);
            // 
            // MPMachineIDTextBox
            // 
            this.MPMachineIDTextBox.Location = new System.Drawing.Point(139, 353);
            this.MPMachineIDTextBox.Name = "MPMachineIDTextBox";
            this.MPMachineIDTextBox.Size = new System.Drawing.Size(190, 22);
            this.MPMachineIDTextBox.TabIndex = 33;
            this.MPMachineIDTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MPMachineIDTextBox_KeyPress);
            // 
            // MPMachineIDLabel
            // 
            this.MPMachineIDLabel.Location = new System.Drawing.Point(23, 356);
            this.MPMachineIDLabel.Name = "MPMachineIDLabel";
            this.MPMachineIDLabel.Size = new System.Drawing.Size(110, 19);
            this.MPMachineIDLabel.TabIndex = 32;
            this.MPMachineIDLabel.Text = "Machine ID";
            this.MPMachineIDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MPMinTextBox
            // 
            this.MPMinTextBox.Location = new System.Drawing.Point(285, 311);
            this.MPMinTextBox.Name = "MPMinTextBox";
            this.MPMinTextBox.Size = new System.Drawing.Size(89, 22);
            this.MPMinTextBox.TabIndex = 31;
            this.MPMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MPMinTextBox_KeyPress);
            // 
            // MPMinLabel
            // 
            this.MPMinLabel.AutoSize = true;
            this.MPMinLabel.Location = new System.Drawing.Point(246, 314);
            this.MPMinLabel.Name = "MPMinLabel";
            this.MPMinLabel.Size = new System.Drawing.Size(30, 17);
            this.MPMinLabel.TabIndex = 30;
            this.MPMinLabel.Text = "Min";
            // 
            // MPMaxTextBox
            // 
            this.MPMaxTextBox.Location = new System.Drawing.Point(139, 311);
            this.MPMaxTextBox.Name = "MPMaxTextBox";
            this.MPMaxTextBox.Size = new System.Drawing.Size(89, 22);
            this.MPMaxTextBox.TabIndex = 29;
            this.MPMaxTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MPMaxTextBox_KeyPress);
            // 
            // MPMaxLabel
            // 
            this.MPMaxLabel.AutoSize = true;
            this.MPMaxLabel.Location = new System.Drawing.Point(100, 314);
            this.MPMaxLabel.Name = "MPMaxLabel";
            this.MPMaxLabel.Size = new System.Drawing.Size(33, 17);
            this.MPMaxLabel.TabIndex = 28;
            this.MPMaxLabel.Text = "Max";
            // 
            // MPPriceTextBox
            // 
            this.MPPriceTextBox.Location = new System.Drawing.Point(139, 265);
            this.MPPriceTextBox.Name = "MPPriceTextBox";
            this.MPPriceTextBox.Size = new System.Drawing.Size(190, 22);
            this.MPPriceTextBox.TabIndex = 27;
            this.MPPriceTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MPPriceTextBox_KeyPress);
            // 
            // MPPriceLabel
            // 
            this.MPPriceLabel.AutoSize = true;
            this.MPPriceLabel.Location = new System.Drawing.Point(61, 268);
            this.MPPriceLabel.Name = "MPPriceLabel";
            this.MPPriceLabel.Size = new System.Drawing.Size(72, 17);
            this.MPPriceLabel.TabIndex = 26;
            this.MPPriceLabel.Text = "Price/Cost";
            // 
            // MPInventoryTextBox
            // 
            this.MPInventoryTextBox.Location = new System.Drawing.Point(139, 218);
            this.MPInventoryTextBox.Name = "MPInventoryTextBox";
            this.MPInventoryTextBox.Size = new System.Drawing.Size(190, 22);
            this.MPInventoryTextBox.TabIndex = 25;
            this.MPInventoryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MPInventoryTextBox_KeyPress);
            // 
            // MPInventoryLabel
            // 
            this.MPInventoryLabel.AutoSize = true;
            this.MPInventoryLabel.Location = new System.Drawing.Point(67, 221);
            this.MPInventoryLabel.Name = "MPInventoryLabel";
            this.MPInventoryLabel.Size = new System.Drawing.Size(66, 17);
            this.MPInventoryLabel.TabIndex = 24;
            this.MPInventoryLabel.Text = "Inventory";
            // 
            // MPIDTextBox
            // 
            this.MPIDTextBox.Enabled = false;
            this.MPIDTextBox.Location = new System.Drawing.Point(139, 125);
            this.MPIDTextBox.Name = "MPIDTextBox";
            this.MPIDTextBox.Size = new System.Drawing.Size(190, 22);
            this.MPIDTextBox.TabIndex = 23;
            // 
            // MPIDLabel
            // 
            this.MPIDLabel.AutoSize = true;
            this.MPIDLabel.Location = new System.Drawing.Point(112, 125);
            this.MPIDLabel.Name = "MPIDLabel";
            this.MPIDLabel.Size = new System.Drawing.Size(21, 17);
            this.MPIDLabel.TabIndex = 22;
            this.MPIDLabel.Text = "ID";
            // 
            // MPOutsourcedRadio
            // 
            this.MPOutsourcedRadio.AutoSize = true;
            this.MPOutsourcedRadio.Location = new System.Drawing.Point(288, 36);
            this.MPOutsourcedRadio.Name = "MPOutsourcedRadio";
            this.MPOutsourcedRadio.Size = new System.Drawing.Size(103, 21);
            this.MPOutsourcedRadio.TabIndex = 21;
            this.MPOutsourcedRadio.TabStop = true;
            this.MPOutsourcedRadio.Text = "Outsourced";
            this.MPOutsourcedRadio.UseVisualStyleBackColor = true;
            this.MPOutsourcedRadio.CheckedChanged += new System.EventHandler(this.MPOutsourcedRadio_CheckedChanged);
            // 
            // MPInhouseRadio
            // 
            this.MPInhouseRadio.AutoSize = true;
            this.MPInhouseRadio.Location = new System.Drawing.Point(166, 36);
            this.MPInhouseRadio.Name = "MPInhouseRadio";
            this.MPInhouseRadio.Size = new System.Drawing.Size(86, 21);
            this.MPInhouseRadio.TabIndex = 20;
            this.MPInhouseRadio.TabStop = true;
            this.MPInhouseRadio.Text = "In-House";
            this.MPInhouseRadio.UseVisualStyleBackColor = true;
            this.MPInhouseRadio.CheckedChanged += new System.EventHandler(this.MPInhouseRadio_CheckedChanged);
            // 
            // MPHeaderLabel
            // 
            this.MPHeaderLabel.AutoSize = true;
            this.MPHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MPHeaderLabel.Location = new System.Drawing.Point(18, 19);
            this.MPHeaderLabel.Name = "MPHeaderLabel";
            this.MPHeaderLabel.Size = new System.Drawing.Size(110, 25);
            this.MPHeaderLabel.TabIndex = 19;
            this.MPHeaderLabel.Text = "Modify Part";
            // 
            // ModifyPartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 502);
            this.Controls.Add(this.MPNameTextBox);
            this.Controls.Add(this.MPNameLabel);
            this.Controls.Add(this.MPCancelButton);
            this.Controls.Add(this.MPSaveButton);
            this.Controls.Add(this.MPMachineIDTextBox);
            this.Controls.Add(this.MPMachineIDLabel);
            this.Controls.Add(this.MPMinTextBox);
            this.Controls.Add(this.MPMinLabel);
            this.Controls.Add(this.MPMaxTextBox);
            this.Controls.Add(this.MPMaxLabel);
            this.Controls.Add(this.MPPriceTextBox);
            this.Controls.Add(this.MPPriceLabel);
            this.Controls.Add(this.MPInventoryTextBox);
            this.Controls.Add(this.MPInventoryLabel);
            this.Controls.Add(this.MPIDTextBox);
            this.Controls.Add(this.MPIDLabel);
            this.Controls.Add(this.MPOutsourcedRadio);
            this.Controls.Add(this.MPInhouseRadio);
            this.Controls.Add(this.MPHeaderLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifyPartForm";
            this.Text = "Part";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MPNameTextBox;
        private System.Windows.Forms.Label MPNameLabel;
        private System.Windows.Forms.Button MPCancelButton;
        private System.Windows.Forms.Button MPSaveButton;
        private System.Windows.Forms.TextBox MPMachineIDTextBox;
        private System.Windows.Forms.Label MPMachineIDLabel;
        private System.Windows.Forms.TextBox MPMinTextBox;
        private System.Windows.Forms.Label MPMinLabel;
        private System.Windows.Forms.TextBox MPMaxTextBox;
        private System.Windows.Forms.Label MPMaxLabel;
        private System.Windows.Forms.TextBox MPPriceTextBox;
        private System.Windows.Forms.Label MPPriceLabel;
        private System.Windows.Forms.TextBox MPInventoryTextBox;
        private System.Windows.Forms.Label MPInventoryLabel;
        private System.Windows.Forms.TextBox MPIDTextBox;
        private System.Windows.Forms.Label MPIDLabel;
        private System.Windows.Forms.RadioButton MPOutsourcedRadio;
        private System.Windows.Forms.RadioButton MPInhouseRadio;
        private System.Windows.Forms.Label MPHeaderLabel;
    }
}