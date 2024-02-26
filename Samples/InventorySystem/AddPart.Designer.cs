namespace InventorySystem
{
    partial class AddPartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPartForm));
            this.APHeaderLabel = new System.Windows.Forms.Label();
            this.APInhouseRadio = new System.Windows.Forms.RadioButton();
            this.APOutsourcedRadio = new System.Windows.Forms.RadioButton();
            this.APIDLabel = new System.Windows.Forms.Label();
            this.APIDTextBox = new System.Windows.Forms.TextBox();
            this.APInventoryLabel = new System.Windows.Forms.Label();
            this.APInventoryTextBox = new System.Windows.Forms.TextBox();
            this.APPriceLabel = new System.Windows.Forms.Label();
            this.APPriceTextBox = new System.Windows.Forms.TextBox();
            this.APMaxLabel = new System.Windows.Forms.Label();
            this.APMaxTextBox = new System.Windows.Forms.TextBox();
            this.APMinLabel = new System.Windows.Forms.Label();
            this.APMinTextBox = new System.Windows.Forms.TextBox();
            this.APMachineIDLabel = new System.Windows.Forms.Label();
            this.APMachineIDTextBox = new System.Windows.Forms.TextBox();
            this.APSaveButton = new System.Windows.Forms.Button();
            this.APCancelButton = new System.Windows.Forms.Button();
            this.APNameLabel = new System.Windows.Forms.Label();
            this.APNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // APHeaderLabel
            // 
            this.APHeaderLabel.AutoSize = true;
            this.APHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APHeaderLabel.Location = new System.Drawing.Point(13, 23);
            this.APHeaderLabel.Name = "APHeaderLabel";
            this.APHeaderLabel.Size = new System.Drawing.Size(88, 25);
            this.APHeaderLabel.TabIndex = 0;
            this.APHeaderLabel.Text = "Add Part";
            // 
            // APInhouseRadio
            // 
            this.APInhouseRadio.AutoSize = true;
            this.APInhouseRadio.Location = new System.Drawing.Point(161, 40);
            this.APInhouseRadio.Name = "APInhouseRadio";
            this.APInhouseRadio.Size = new System.Drawing.Size(86, 21);
            this.APInhouseRadio.TabIndex = 1;
            this.APInhouseRadio.TabStop = true;
            this.APInhouseRadio.Text = "In-House";
            this.APInhouseRadio.UseVisualStyleBackColor = true;
            this.APInhouseRadio.CheckedChanged += new System.EventHandler(this.APInhouseRadio_CheckedChanged);
            // 
            // APOutsourcedRadio
            // 
            this.APOutsourcedRadio.AutoSize = true;
            this.APOutsourcedRadio.Location = new System.Drawing.Point(283, 40);
            this.APOutsourcedRadio.Name = "APOutsourcedRadio";
            this.APOutsourcedRadio.Size = new System.Drawing.Size(103, 21);
            this.APOutsourcedRadio.TabIndex = 2;
            this.APOutsourcedRadio.TabStop = true;
            this.APOutsourcedRadio.Text = "Outsourced";
            this.APOutsourcedRadio.UseVisualStyleBackColor = true;
            this.APOutsourcedRadio.CheckedChanged += new System.EventHandler(this.APOutsourcedRadio_CheckedChanged);
            // 
            // APIDLabel
            // 
            this.APIDLabel.AutoSize = true;
            this.APIDLabel.Location = new System.Drawing.Point(107, 129);
            this.APIDLabel.Name = "APIDLabel";
            this.APIDLabel.Size = new System.Drawing.Size(21, 17);
            this.APIDLabel.TabIndex = 3;
            this.APIDLabel.Text = "ID";
            // 
            // APIDTextBox
            // 
            this.APIDTextBox.Enabled = false;
            this.APIDTextBox.Location = new System.Drawing.Point(134, 129);
            this.APIDTextBox.Name = "APIDTextBox";
            this.APIDTextBox.Size = new System.Drawing.Size(190, 22);
            this.APIDTextBox.TabIndex = 4;
            this.APIDTextBox.TextChanged += new System.EventHandler(this.APIDTextBox_TextChanged);
            // 
            // APInventoryLabel
            // 
            this.APInventoryLabel.AutoSize = true;
            this.APInventoryLabel.Location = new System.Drawing.Point(62, 225);
            this.APInventoryLabel.Name = "APInventoryLabel";
            this.APInventoryLabel.Size = new System.Drawing.Size(66, 17);
            this.APInventoryLabel.TabIndex = 5;
            this.APInventoryLabel.Text = "Inventory";
            // 
            // APInventoryTextBox
            // 
            this.APInventoryTextBox.Location = new System.Drawing.Point(134, 222);
            this.APInventoryTextBox.Name = "APInventoryTextBox";
            this.APInventoryTextBox.Size = new System.Drawing.Size(190, 22);
            this.APInventoryTextBox.TabIndex = 6;
            this.APInventoryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VerifyNumeric);
            // 
            // APPriceLabel
            // 
            this.APPriceLabel.AutoSize = true;
            this.APPriceLabel.Location = new System.Drawing.Point(56, 272);
            this.APPriceLabel.Name = "APPriceLabel";
            this.APPriceLabel.Size = new System.Drawing.Size(72, 17);
            this.APPriceLabel.TabIndex = 7;
            this.APPriceLabel.Text = "Price/Cost";
            // 
            // APPriceTextBox
            // 
            this.APPriceTextBox.Location = new System.Drawing.Point(134, 269);
            this.APPriceTextBox.Name = "APPriceTextBox";
            this.APPriceTextBox.Size = new System.Drawing.Size(190, 22);
            this.APPriceTextBox.TabIndex = 8;
            this.APPriceTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.APPriceTextBox_KeyPress);
            // 
            // APMaxLabel
            // 
            this.APMaxLabel.AutoSize = true;
            this.APMaxLabel.Location = new System.Drawing.Point(95, 318);
            this.APMaxLabel.Name = "APMaxLabel";
            this.APMaxLabel.Size = new System.Drawing.Size(33, 17);
            this.APMaxLabel.TabIndex = 9;
            this.APMaxLabel.Text = "Max";
            // 
            // APMaxTextBox
            // 
            this.APMaxTextBox.Location = new System.Drawing.Point(134, 315);
            this.APMaxTextBox.Name = "APMaxTextBox";
            this.APMaxTextBox.Size = new System.Drawing.Size(89, 22);
            this.APMaxTextBox.TabIndex = 10;
            this.APMaxTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.APMaxTextBox_KeyPress);
            // 
            // APMinLabel
            // 
            this.APMinLabel.AutoSize = true;
            this.APMinLabel.Location = new System.Drawing.Point(241, 318);
            this.APMinLabel.Name = "APMinLabel";
            this.APMinLabel.Size = new System.Drawing.Size(30, 17);
            this.APMinLabel.TabIndex = 11;
            this.APMinLabel.Text = "Min";
            // 
            // APMinTextBox
            // 
            this.APMinTextBox.Location = new System.Drawing.Point(280, 315);
            this.APMinTextBox.Name = "APMinTextBox";
            this.APMinTextBox.Size = new System.Drawing.Size(89, 22);
            this.APMinTextBox.TabIndex = 12;
            this.APMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.APMinTextBox_KeyPress);
            // 
            // APMachineIDLabel
            // 
            this.APMachineIDLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.APMachineIDLabel.Location = new System.Drawing.Point(18, 360);
            this.APMachineIDLabel.Name = "APMachineIDLabel";
            this.APMachineIDLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.APMachineIDLabel.Size = new System.Drawing.Size(110, 19);
            this.APMachineIDLabel.TabIndex = 13;
            this.APMachineIDLabel.Text = "Machine ID";
            this.APMachineIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.APMachineIDLabel.Click += new System.EventHandler(this.APMachineIDLabel_Click);
            // 
            // APMachineIDTextBox
            // 
            this.APMachineIDTextBox.Location = new System.Drawing.Point(134, 357);
            this.APMachineIDTextBox.Name = "APMachineIDTextBox";
            this.APMachineIDTextBox.Size = new System.Drawing.Size(190, 22);
            this.APMachineIDTextBox.TabIndex = 14;
            this.APMachineIDTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.APMachineIDTextBox_KeyPress);
            // 
            // APSaveButton
            // 
            this.APSaveButton.Location = new System.Drawing.Point(216, 431);
            this.APSaveButton.Name = "APSaveButton";
            this.APSaveButton.Size = new System.Drawing.Size(75, 38);
            this.APSaveButton.TabIndex = 15;
            this.APSaveButton.Text = "Save";
            this.APSaveButton.UseVisualStyleBackColor = true;
            this.APSaveButton.Click += new System.EventHandler(this.APSaveButton_Click);
            // 
            // APCancelButton
            // 
            this.APCancelButton.Location = new System.Drawing.Point(311, 431);
            this.APCancelButton.Name = "APCancelButton";
            this.APCancelButton.Size = new System.Drawing.Size(75, 38);
            this.APCancelButton.TabIndex = 16;
            this.APCancelButton.Text = "Cancel";
            this.APCancelButton.UseVisualStyleBackColor = true;
            this.APCancelButton.Click += new System.EventHandler(this.APCancelButton_Click);
            // 
            // APNameLabel
            // 
            this.APNameLabel.AutoSize = true;
            this.APNameLabel.Location = new System.Drawing.Point(83, 176);
            this.APNameLabel.Name = "APNameLabel";
            this.APNameLabel.Size = new System.Drawing.Size(45, 17);
            this.APNameLabel.TabIndex = 17;
            this.APNameLabel.Text = "Name";
            // 
            // APNameTextBox
            // 
            this.APNameTextBox.Location = new System.Drawing.Point(134, 175);
            this.APNameTextBox.Name = "APNameTextBox";
            this.APNameTextBox.Size = new System.Drawing.Size(190, 22);
            this.APNameTextBox.TabIndex = 18;
            // 
            // AddPartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 501);
            this.Controls.Add(this.APNameTextBox);
            this.Controls.Add(this.APNameLabel);
            this.Controls.Add(this.APCancelButton);
            this.Controls.Add(this.APSaveButton);
            this.Controls.Add(this.APMachineIDTextBox);
            this.Controls.Add(this.APMachineIDLabel);
            this.Controls.Add(this.APMinTextBox);
            this.Controls.Add(this.APMinLabel);
            this.Controls.Add(this.APMaxTextBox);
            this.Controls.Add(this.APMaxLabel);
            this.Controls.Add(this.APPriceTextBox);
            this.Controls.Add(this.APPriceLabel);
            this.Controls.Add(this.APInventoryTextBox);
            this.Controls.Add(this.APInventoryLabel);
            this.Controls.Add(this.APIDTextBox);
            this.Controls.Add(this.APIDLabel);
            this.Controls.Add(this.APOutsourcedRadio);
            this.Controls.Add(this.APInhouseRadio);
            this.Controls.Add(this.APHeaderLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddPartForm";
            this.Text = "Part";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label APHeaderLabel;
        private System.Windows.Forms.RadioButton APInhouseRadio;
        private System.Windows.Forms.RadioButton APOutsourcedRadio;
        private System.Windows.Forms.Label APIDLabel;
        private System.Windows.Forms.TextBox APIDTextBox;
        private System.Windows.Forms.Label APInventoryLabel;
        private System.Windows.Forms.TextBox APInventoryTextBox;
        private System.Windows.Forms.Label APPriceLabel;
        private System.Windows.Forms.TextBox APPriceTextBox;
        private System.Windows.Forms.Label APMaxLabel;
        private System.Windows.Forms.TextBox APMaxTextBox;
        private System.Windows.Forms.Label APMinLabel;
        private System.Windows.Forms.TextBox APMinTextBox;
        private System.Windows.Forms.Label APMachineIDLabel;
        private System.Windows.Forms.TextBox APMachineIDTextBox;
        private System.Windows.Forms.Button APSaveButton;
        private System.Windows.Forms.Button APCancelButton;
        private System.Windows.Forms.Label APNameLabel;
        private System.Windows.Forms.TextBox APNameTextBox;
    }
}