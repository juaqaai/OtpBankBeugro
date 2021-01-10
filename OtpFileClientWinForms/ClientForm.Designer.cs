namespace OtpFileClientWinForms
{
    partial class ClientForm
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
            this.docsDataGridView = new System.Windows.Forms.DataGridView();
            this.saveOtpDownloadFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openOtpUploadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnOtpFileUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.docsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // docsDataGridView
            // 
            this.docsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.docsDataGridView.Location = new System.Drawing.Point(12, 31);
            this.docsDataGridView.Name = "docsDataGridView";
            this.docsDataGridView.Size = new System.Drawing.Size(776, 221);
            this.docsDataGridView.TabIndex = 0;
            this.docsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.docsDataGridView_CellContentClick);
            // 
            // saveOtpDownloadFileDialog
            // 
            this.saveOtpDownloadFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveOtpDownloadFileDialog_FileOk);
            // 
            // openOtpUploadFileDialog
            // 
            this.openOtpUploadFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openOtpUploadFileDialog_FileOk);
            // 
            // btnOtpFileUpload
            // 
            this.btnOtpFileUpload.Location = new System.Drawing.Point(12, 274);
            this.btnOtpFileUpload.Name = "btnOtpFileUpload";
            this.btnOtpFileUpload.Size = new System.Drawing.Size(145, 30);
            this.btnOtpFileUpload.TabIndex = 1;
            this.btnOtpFileUpload.Text = "Fájl feltöltése szerverre";
            this.btnOtpFileUpload.UseVisualStyleBackColor = true;
            this.btnOtpFileUpload.Click += new System.EventHandler(this.btnOtpFileUpload_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOtpFileUpload);
            this.Controls.Add(this.docsDataGridView);
            this.Name = "ClientForm";
            this.Text = "Fájl szerver dokumentumok";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.docsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView docsDataGridView;
        private System.Windows.Forms.SaveFileDialog saveOtpDownloadFileDialog;
        private System.Windows.Forms.OpenFileDialog openOtpUploadFileDialog;
        private System.Windows.Forms.Button btnOtpFileUpload;
    }
}

