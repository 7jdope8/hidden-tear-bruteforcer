namespace hidden_tear_bruteforcer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bruteforceButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openSampleFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileButton = new System.Windows.Forms.Button();
            this.FileSelectedLabel = new System.Windows.Forms.Label();
            this.DisplayText = new System.Windows.Forms.TextBox();
            this.AttemptsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadKeyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openKeyListFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bruteforceButton
            // 
            this.bruteforceButton.Location = new System.Drawing.Point(72, 131);
            this.bruteforceButton.Name = "bruteforceButton";
            this.bruteforceButton.Size = new System.Drawing.Size(211, 31);
            this.bruteforceButton.TabIndex = 2;
            this.bruteforceButton.Text = "Start Bruteforce";
            this.bruteforceButton.UseVisualStyleBackColor = true;
            this.bruteforceButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hidden Tear Bruteforcer\r\ncoded by Michael Gillespie / Demonslay335";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label3.Location = new System.Drawing.Point(131, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Key Found!";
            this.label3.Visible = false;
            // 
            // openSampleFileDialog
            // 
            this.openSampleFileDialog.Filter = "Encrypted PNG (*.png.*)|*.png.*";
            this.openSampleFileDialog.Title = "Select Encrypted Sample";
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(106, 71);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(143, 31);
            this.FileButton.TabIndex = 1;
            this.FileButton.Text = "Browse Sample";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // FileSelectedLabel
            // 
            this.FileSelectedLabel.AutoSize = true;
            this.FileSelectedLabel.Location = new System.Drawing.Point(135, 105);
            this.FileSelectedLabel.Name = "FileSelectedLabel";
            this.FileSelectedLabel.Size = new System.Drawing.Size(85, 13);
            this.FileSelectedLabel.TabIndex = 2;
            this.FileSelectedLabel.Text = "No File Selected";
            // 
            // DisplayText
            // 
            this.DisplayText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayText.Location = new System.Drawing.Point(13, 195);
            this.DisplayText.Name = "DisplayText";
            this.DisplayText.ReadOnly = true;
            this.DisplayText.Size = new System.Drawing.Size(329, 20);
            this.DisplayText.TabIndex = 6;
            // 
            // AttemptsLabel
            // 
            this.AttemptsLabel.AutoSize = true;
            this.AttemptsLabel.Location = new System.Drawing.Point(150, 173);
            this.AttemptsLabel.Name = "AttemptsLabel";
            this.AttemptsLabel.Size = new System.Drawing.Size(54, 13);
            this.AttemptsLabel.TabIndex = 7;
            this.AttemptsLabel.Text = "Attempts: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 39);
            this.label1.TabIndex = 8;
            this.label1.Text = "Please select a sample encrypted PNG file.\r\nThe smaller the file, the faster the " +
    "bruteforce will run.\r\nPreferably a file less than 1KB.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InfoLabel
            // 
            this.InfoLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.Location = new System.Drawing.Point(13, 238);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(329, 38);
            this.InfoLabel.TabIndex = 9;
            this.InfoLabel.Text = "Info";
            this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InfoLabel.Visible = false;
            this.InfoLabel.Click += new System.EventHandler(this.InfoLabel_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainMenu.Size = new System.Drawing.Size(354, 24);
            this.mainMenu.TabIndex = 10;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadKeyListToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadKeyListToolStripMenuItem
            // 
            this.loadKeyListToolStripMenuItem.Name = "loadKeyListToolStripMenuItem";
            this.loadKeyListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadKeyListToolStripMenuItem.Text = "Load Key List";
            this.loadKeyListToolStripMenuItem.Click += new System.EventHandler(this.loadKeyListToolStripMenuItem_Click);
            // 
            // openKeyListFileDialog
            // 
            this.openKeyListFileDialog.Filter = "Key List (*.csv)|*.csv";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 325);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AttemptsLabel);
            this.Controls.Add(this.DisplayText);
            this.Controls.Add(this.FileSelectedLabel);
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bruteforceButton);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "HT BruteForcer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bruteforceButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openSampleFileDialog;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.Label FileSelectedLabel;
        private System.Windows.Forms.TextBox DisplayText;
        private System.Windows.Forms.Label AttemptsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadKeyListToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openKeyListFileDialog;
    }
}

