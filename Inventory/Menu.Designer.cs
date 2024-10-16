namespace Inventory
{
    partial class Menu
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
            this.lblQuery = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Button();
            this.lblMaster = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblInput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblQuery
            // 
            this.lblQuery.BackColor = System.Drawing.Color.Black;
            this.lblQuery.ForeColor = System.Drawing.Color.White;
            this.lblQuery.Location = new System.Drawing.Point(162, 260);
            this.lblQuery.Margin = new System.Windows.Forms.Padding(2);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(119, 100);
            this.lblQuery.TabIndex = 58;
            this.lblQuery.Text = "QUERY";
            this.lblQuery.UseVisualStyleBackColor = false;
            this.lblQuery.Click += new System.EventHandler(this.lblQuery_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Effra", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(158, 58);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(0, 20);
            this.lblUser.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Effra", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(75, 58);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 20);
            this.label11.TabIndex = 56;
            this.label11.Text = "USER-ID :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutput
            // 
            this.lblOutput.BackColor = System.Drawing.Color.DarkGreen;
            this.lblOutput.ForeColor = System.Drawing.Color.White;
            this.lblOutput.Location = new System.Drawing.Point(519, 130);
            this.lblOutput.Margin = new System.Windows.Forms.Padding(2);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(119, 100);
            this.lblOutput.TabIndex = 55;
            this.lblOutput.Text = "OUTGOING";
            this.lblOutput.UseVisualStyleBackColor = false;
            this.lblOutput.Click += new System.EventHandler(this.lblOutput_Click);
            // 
            // lblMaster
            // 
            this.lblMaster.BackColor = System.Drawing.Color.Sienna;
            this.lblMaster.ForeColor = System.Drawing.Color.White;
            this.lblMaster.Location = new System.Drawing.Point(162, 130);
            this.lblMaster.Margin = new System.Windows.Forms.Padding(2);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(119, 100);
            this.lblMaster.TabIndex = 54;
            this.lblMaster.Text = "MASTER";
            this.lblMaster.UseVisualStyleBackColor = false;
            this.lblMaster.Click += new System.EventHandler(this.lblMaster_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(626, 349);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 44);
            this.btnExit.TabIndex = 53;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblInput
            // 
            this.lblInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblInput.ForeColor = System.Drawing.Color.White;
            this.lblInput.Location = new System.Drawing.Point(338, 130);
            this.lblInput.Margin = new System.Windows.Forms.Padding(2);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(119, 100);
            this.lblInput.TabIndex = 52;
            this.lblInput.Text = "INCOMING";
            this.lblInput.UseVisualStyleBackColor = false;
            this.lblInput.Click += new System.EventHandler(this.lblInput_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblMaster);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblInput);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lblQuery;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button lblOutput;
        private System.Windows.Forms.Button lblMaster;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button lblInput;
    }
}