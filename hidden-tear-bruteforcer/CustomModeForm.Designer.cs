namespace hidden_tear_bruteforcer
{
    partial class CustomModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomModeForm));
            this.CustomPasswordLength = new System.Windows.Forms.TextBox();
            this.CustomPasswordLengthLabel = new System.Windows.Forms.Label();
            this.CustomPasswordgeneratorLabel = new System.Windows.Forms.Label();
            this.CustomCharactersLabel = new System.Windows.Forms.Label();
            this.CustomPossibleCharacters = new System.Windows.Forms.TextBox();
            this.CustomPasswordGenerator = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CustomPasswordLength
            // 
            this.CustomPasswordLength.Location = new System.Drawing.Point(122, 10);
            this.CustomPasswordLength.Name = "CustomPasswordLength";
            this.CustomPasswordLength.Size = new System.Drawing.Size(150, 20);
            this.CustomPasswordLength.TabIndex = 0;
            this.CustomPasswordLength.Text = "32";
            // 
            // CustomPasswordLengthLabel
            // 
            this.CustomPasswordLengthLabel.AutoSize = true;
            this.CustomPasswordLengthLabel.Location = new System.Drawing.Point(13, 13);
            this.CustomPasswordLengthLabel.Name = "CustomPasswordLengthLabel";
            this.CustomPasswordLengthLabel.Size = new System.Drawing.Size(92, 13);
            this.CustomPasswordLengthLabel.TabIndex = 1;
            this.CustomPasswordLengthLabel.Text = "Password Length:";
            // 
            // CustomPasswordgeneratorLabel
            // 
            this.CustomPasswordgeneratorLabel.AutoSize = true;
            this.CustomPasswordgeneratorLabel.Location = new System.Drawing.Point(13, 39);
            this.CustomPasswordgeneratorLabel.Name = "CustomPasswordgeneratorLabel";
            this.CustomPasswordgeneratorLabel.Size = new System.Drawing.Size(106, 13);
            this.CustomPasswordgeneratorLabel.TabIndex = 2;
            this.CustomPasswordgeneratorLabel.Text = "Password Generator:";
            // 
            // CustomCharactersLabel
            // 
            this.CustomCharactersLabel.AutoSize = true;
            this.CustomCharactersLabel.Location = new System.Drawing.Point(13, 66);
            this.CustomCharactersLabel.Name = "CustomCharactersLabel";
            this.CustomCharactersLabel.Size = new System.Drawing.Size(103, 13);
            this.CustomCharactersLabel.TabIndex = 3;
            this.CustomCharactersLabel.Text = "Possible Characters:";
            // 
            // CustomPossibleCharacters
            // 
            this.CustomPossibleCharacters.Location = new System.Drawing.Point(122, 63);
            this.CustomPossibleCharacters.Name = "CustomPossibleCharacters";
            this.CustomPossibleCharacters.Size = new System.Drawing.Size(150, 20);
            this.CustomPossibleCharacters.TabIndex = 4;
            // 
            // CustomPasswordGenerator
            // 
            this.CustomPasswordGenerator.FormattingEnabled = true;
            this.CustomPasswordGenerator.Location = new System.Drawing.Point(122, 36);
            this.CustomPasswordGenerator.Name = "CustomPasswordGenerator";
            this.CustomPasswordGenerator.Size = new System.Drawing.Size(150, 21);
            this.CustomPasswordGenerator.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Apply Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ApplySettingsButton_Click);
            // 
            // CustomModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 121);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CustomPasswordGenerator);
            this.Controls.Add(this.CustomPossibleCharacters);
            this.Controls.Add(this.CustomCharactersLabel);
            this.Controls.Add(this.CustomPasswordgeneratorLabel);
            this.Controls.Add(this.CustomPasswordLengthLabel);
            this.Controls.Add(this.CustomPasswordLength);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomModeForm";
            this.Text = "Custom Mode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label CustomPasswordLengthLabel;
        private System.Windows.Forms.Label CustomPasswordgeneratorLabel;
        private System.Windows.Forms.Label CustomCharactersLabel;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox CustomPasswordLength;
        public System.Windows.Forms.TextBox CustomPossibleCharacters;
        public System.Windows.Forms.ComboBox CustomPasswordGenerator;
    }
}