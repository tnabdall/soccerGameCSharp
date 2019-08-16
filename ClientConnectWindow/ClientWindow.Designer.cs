namespace ClientConnectWindow
{
    partial class ClientConnectionApp
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
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.shooterConnectButton = new System.Windows.Forms.Button();
            this.keeperButton = new System.Windows.Forms.Button();
            this.spectateButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressLabel.Location = new System.Drawing.Point(12, 43);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(155, 31);
            this.ipAddressLabel.TabIndex = 0;
            this.ipAddressLabel.Text = "IP Address:";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressTextBox.Location = new System.Drawing.Point(173, 43);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(361, 38);
            this.ipAddressTextBox.TabIndex = 1;
            this.ipAddressTextBox.TextChanged += new System.EventHandler(this.ipAddressTextBox_TextChanged);
            // 
            // errorLabel
            // 
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(53, 105);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(636, 68);
            this.errorLabel.TabIndex = 2;
            // 
            // shooterConnectButton
            // 
            this.shooterConnectButton.Enabled = false;
            this.shooterConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shooterConnectButton.Location = new System.Drawing.Point(79, 176);
            this.shooterConnectButton.Name = "shooterConnectButton";
            this.shooterConnectButton.Size = new System.Drawing.Size(203, 63);
            this.shooterConnectButton.TabIndex = 3;
            this.shooterConnectButton.Text = "Play as Shooter";
            this.shooterConnectButton.UseVisualStyleBackColor = true;
            this.shooterConnectButton.Click += new System.EventHandler(this.shooterConnectButton_Click);
            // 
            // keeperButton
            // 
            this.keeperButton.Enabled = false;
            this.keeperButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keeperButton.Location = new System.Drawing.Point(486, 176);
            this.keeperButton.Name = "keeperButton";
            this.keeperButton.Size = new System.Drawing.Size(203, 63);
            this.keeperButton.TabIndex = 3;
            this.keeperButton.Text = "Play as Keeper";
            this.keeperButton.UseVisualStyleBackColor = true;
            this.keeperButton.Click += new System.EventHandler(this.keeperButton_Click);
            // 
            // spectateButton
            // 
            this.spectateButton.Enabled = false;
            this.spectateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spectateButton.Location = new System.Drawing.Point(281, 280);
            this.spectateButton.Name = "spectateButton";
            this.spectateButton.Size = new System.Drawing.Size(203, 63);
            this.spectateButton.TabIndex = 3;
            this.spectateButton.Text = "Spectate!";
            this.spectateButton.UseVisualStyleBackColor = true;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectButton.Location = new System.Drawing.Point(565, 47);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(145, 34);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ClientConnectionApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 382);
            this.Controls.Add(this.spectateButton);
            this.Controls.Add(this.keeperButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.shooterConnectButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.ipAddressLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientConnectionApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to Soccer Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button shooterConnectButton;
        private System.Windows.Forms.Button keeperButton;
        private System.Windows.Forms.Button spectateButton;
        private System.Windows.Forms.Button connectButton;
    }
}

