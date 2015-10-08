namespace PaySlipGeneratingTool
{
    partial class Template
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
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.btnUploadTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(36, 42);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(100, 20);
            this.txtTemplate.TabIndex = 0;
            // 
            // btnBrowseTemplate
            // 
            this.btnBrowseTemplate.Location = new System.Drawing.Point(142, 42);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(107, 23);
            this.btnBrowseTemplate.TabIndex = 1;
            this.btnBrowseTemplate.Text = "Browse Template";
            this.btnBrowseTemplate.UseVisualStyleBackColor = true;
            this.btnBrowseTemplate.Click += new System.EventHandler(this.btnBrowseTemplate_Click);
            // 
            // btnUploadTemplate
            // 
            this.btnUploadTemplate.Location = new System.Drawing.Point(98, 92);
            this.btnUploadTemplate.Name = "btnUploadTemplate";
            this.btnUploadTemplate.Size = new System.Drawing.Size(75, 23);
            this.btnUploadTemplate.TabIndex = 2;
            this.btnUploadTemplate.Text = "Upload";
            this.btnUploadTemplate.UseVisualStyleBackColor = true;
            this.btnUploadTemplate.Click += new System.EventHandler(this.btnUploadTemplate_Click);
            // 
            // Template
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnUploadTemplate);
            this.Controls.Add(this.btnBrowseTemplate);
            this.Controls.Add(this.txtTemplate);
            this.Name = "Template";
            this.Text = "Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.Button btnUploadTemplate;
    }
}