namespace WindowsFormsApplication1
{
    partial class frmAssessment
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
            this.txtCSVFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHeaders = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCSVFile
            // 
            this.txtCSVFile.Location = new System.Drawing.Point(99, 34);
            this.txtCSVFile.Name = "txtCSVFile";
            this.txtCSVFile.Size = new System.Drawing.Size(419, 20);
            this.txtCSVFile.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(355, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Produce Lists";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(447, 254);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "CSV File Name:";
            // 
            // chkHeaders
            // 
            this.chkHeaders.AutoSize = true;
            this.chkHeaders.Checked = true;
            this.chkHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHeaders.Location = new System.Drawing.Point(99, 73);
            this.chkHeaders.Name = "chkHeaders";
            this.chkHeaders.Size = new System.Drawing.Size(185, 17);
            this.chkHeaders.TabIndex = 4;
            this.chkHeaders.Text = "First line of the file is a header row";
            this.chkHeaders.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 144);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 5;
            // 
            // frmAssessment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 289);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.chkHeaders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCSVFile);
            this.Name = "frmAssessment";
            this.Text = "Assessment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCSVFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkHeaders;
        private System.Windows.Forms.Label lblStatus;
    }
}

