namespace TechForgeGUI.SubPages
{
    partial class ReportReceiptDetailFormGUI
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
            this.rpvReceiptDetail = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvReceiptDetail
            // 
            this.rpvReceiptDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvReceiptDetail.Location = new System.Drawing.Point(0, 0);
            this.rpvReceiptDetail.Name = "rpvReceiptDetail";
            this.rpvReceiptDetail.ServerReport.BearerToken = null;
            this.rpvReceiptDetail.Size = new System.Drawing.Size(1182, 753);
            this.rpvReceiptDetail.TabIndex = 0;
            // 
            // ReportReceiptDetailFormGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.rpvReceiptDetail);
            this.Name = "ReportReceiptDetailFormGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết hoá đơn";
            this.Load += new System.EventHandler(this.ReportReceiptDetailFormGUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvReceiptDetail;
    }
}