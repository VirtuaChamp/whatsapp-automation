namespace WhatsAppAutomationGUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtCsvFilePath;
        private System.Windows.Forms.TextBox txtMessageTemplate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSendMessages;
        private System.Windows.Forms.RichTextBox txtMessagePreview;
        private System.Windows.Forms.Button btnRefreshPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtCsvFilePath = new TextBox();
            txtMessageTemplate = new TextBox();
            btnBrowse = new Button();
            btnSendMessages = new Button();
            txtMessagePreview = new RichTextBox();
            btnRefreshPreview = new Button();
            SuspendLayout();
            // 
            // txtCsvFilePath
            // 
            txtCsvFilePath.Location = new Point(12, 12);
            txtCsvFilePath.Name = "txtCsvFilePath";
            txtCsvFilePath.Size = new Size(495, 23);
            txtCsvFilePath.TabIndex = 0;
            // 
            // txtMessageTemplate
            // 
            txtMessageTemplate.Location = new Point(12, 38);
            txtMessageTemplate.Multiline = true;
            txtMessageTemplate.Name = "txtMessageTemplate";
            txtMessageTemplate.Size = new Size(576, 246);
            txtMessageTemplate.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(513, 9);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSendMessages
            // 
            btnSendMessages.Location = new Point(12, 631);
            btnSendMessages.Name = "btnSendMessages";
            btnSendMessages.Size = new Size(576, 23);
            btnSendMessages.TabIndex = 3;
            btnSendMessages.Text = "Send Messages";
            btnSendMessages.UseVisualStyleBackColor = true;
            btnSendMessages.Click += btnSendMessages_Click;
            // 
            // txtMessagePreview
            // 
            txtMessagePreview.Location = new Point(12, 319);
            txtMessagePreview.Name = "txtMessagePreview";
            txtMessagePreview.ReadOnly = true;
            txtMessagePreview.Size = new Size(576, 306);
            txtMessagePreview.TabIndex = 4;
            txtMessagePreview.Text = "";
            // 
            // btnRefreshPreview
            // 
            btnRefreshPreview.Location = new Point(12, 290);
            btnRefreshPreview.Name = "btnRefreshPreview";
            btnRefreshPreview.Size = new Size(576, 23);
            btnRefreshPreview.TabIndex = 5;
            btnRefreshPreview.Text = "Refresh Preview";
            btnRefreshPreview.UseVisualStyleBackColor = true;
            btnRefreshPreview.Click += btnRefreshPreview_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(600, 666);
            Controls.Add(btnRefreshPreview);
            Controls.Add(btnSendMessages);
            Controls.Add(txtMessagePreview);
            Controls.Add(btnBrowse);
            Controls.Add(txtMessageTemplate);
            Controls.Add(txtCsvFilePath);
            Name = "MainForm";
            Text = "WhatsApp Automation";
            ResumeLayout(false);
            PerformLayout();
        }

        // Event handler for refreshing the message preview
        private void btnRefreshPreview_Click(object sender, EventArgs e)
        {
            DisplayMessagePreview(txtCsvFilePath.Text, txtMessageTemplate.Text);
        }
    }
}
