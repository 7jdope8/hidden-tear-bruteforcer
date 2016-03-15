namespace hidden_tear_bruteforcer
{
    partial class ModifiedDateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifiedDateForm));
            this.ModifiedDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ModifiedTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SetModifiedButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ModifiedDatePicker
            // 
            this.ModifiedDatePicker.CustomFormat = "d-MMM-yyyy";
            this.ModifiedDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ModifiedDatePicker.Location = new System.Drawing.Point(9, 56);
            this.ModifiedDatePicker.Name = "ModifiedDatePicker";
            this.ModifiedDatePicker.Size = new System.Drawing.Size(112, 20);
            this.ModifiedDatePicker.TabIndex = 0;
            // 
            // ModifiedTimePicker
            // 
            this.ModifiedTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.ModifiedTimePicker.Location = new System.Drawing.Point(127, 56);
            this.ModifiedTimePicker.Name = "ModifiedTimePicker";
            this.ModifiedTimePicker.ShowUpDown = true;
            this.ModifiedTimePicker.Size = new System.Drawing.Size(112, 20);
            this.ModifiedTimePicker.TabIndex = 1;
            // 
            // SetModifiedButton
            // 
            this.SetModifiedButton.Location = new System.Drawing.Point(70, 95);
            this.SetModifiedButton.Name = "SetModifiedButton";
            this.SetModifiedButton.Size = new System.Drawing.Size(109, 23);
            this.SetModifiedButton.TabIndex = 2;
            this.SetModifiedButton.Text = "Set Modified Date";
            this.SetModifiedButton.UseVisualStyleBackColor = true;
            this.SetModifiedButton.Click += new System.EventHandler(this.SetModifiedButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "File Modified Date";
            // 
            // ModifiedDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 130);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SetModifiedButton);
            this.Controls.Add(this.ModifiedTimePicker);
            this.Controls.Add(this.ModifiedDatePicker);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifiedDateForm";
            this.Text = "Modified Date";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SetModifiedButton;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker ModifiedDatePicker;
        public System.Windows.Forms.DateTimePicker ModifiedTimePicker;
    }
}