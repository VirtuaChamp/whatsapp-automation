using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WhatsAppAutomation.Bots;
using WhatsAppAutomation.Models.Messages.Requests;

namespace WhatsAppAutomationGUI
{
    public partial class MainForm : Form
    {
        private WhatsAppBot _bot;

        public MainForm()
        {
            InitializeComponent();
            _bot = new WhatsAppBot();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCsvFilePath.Text = openFileDialog.FileName;

                    // Display one example of the formatted message
                    DisplayMessagePreview(txtCsvFilePath.Text, txtMessageTemplate.Text);
                }
            }
        }

        private void DisplayMessagePreview(string csvFilePath, string template)
        {
            txtMessagePreview.Clear(); // Clear previous preview

            if (string.IsNullOrWhiteSpace(csvFilePath) || !File.Exists(csvFilePath))
            {
                MessageBox.Show("Invalid CSV file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(template))
            {
                MessageBox.Show("Message template is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var reader = new StreamReader(csvFilePath))
            {
                string line = reader.ReadLine(); // Read only the first line
                if (line != null)
                {
                    var values = line.Split(new[] { ',' }, 2); // Split into phone number and name
                    if (values.Length == 2)
                    {
                        string phone = values[0].Trim();
                        string name = values[1].Trim();

                        // Replace placeholders in the template
                        string message = template.Replace("{Name}", name)
                                                 .Replace("\n", Environment.NewLine);

                        // Display the formatted message in the TextBox
                        txtMessagePreview.Text = message;
                    }
                }
            }
        }

        private void btnSendMessages_Click(object sender, EventArgs e)
        {
            string csvFilePath = txtCsvFilePath.Text;
            string customMessageTemplate = txtMessageTemplate.Text;

            if (string.IsNullOrWhiteSpace(csvFilePath) || !File.Exists(csvFilePath))
            {
                MessageBox.Show("Please select a valid CSV file.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(customMessageTemplate))
            {
                MessageBox.Show("Please enter a message template.", "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var messageRequests = ReadCsvFile(csvFilePath, customMessageTemplate);
            int totalMessages = messageRequests.Count;
            int successCount = 0;
            int errorCount = 0;
            List<string> errorMessages = new List<string>();

            _bot.InitialiseWA();
            MessageBox.Show("Please scan the QR code and click OK when ready.", "WhatsApp Web", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int currentMessageIndex = 0;
            string progressText = $"{currentMessageIndex}/{totalMessages}";
            Text = $"WhatsApp Automation - Sending {progressText}";

            foreach (var messageRequest in messageRequests)
            {
                currentMessageIndex++;
                var response = _bot.SendMessage(messageRequest);
                if (response.Success)
                {
                    successCount++;
                }
                else
                {
                    errorCount++;
                    errorMessages.Add($"Error to {messageRequest.Phone}: {response.Exception.Message}");
                }
                progressText = $"{currentMessageIndex}/{totalMessages}";
                Text = $"WhatsApp Automation - Sending {progressText}";

                Refresh(); // Ensure the form updates visually
            }

            string summary = $"Messages sent successfully: {successCount}\n" +
                             $"Messages failed: {errorCount}\n";
            if (errorMessages.Count > 0)
            {
                summary += "\nError details:\n" + string.Join("\n", errorMessages);
            }

            MessageBox.Show(summary, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _bot.Close();
        }

        private List<MessageRequestModel> ReadCsvFile(string csvFilePath, string template)
        {
            var messageRequests = new List<MessageRequestModel>();

            using (var reader = new StreamReader(csvFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(new[] { ',' }, 2); // Split into phone number and name
                    if (values.Length == 2)
                    {
                        string phone = values[0].Trim();
                        string name = values[1].Trim();

                        // Replace placeholders in the template
                        string message = template.Replace("{Name}", name)
                                                 .Replace("\n", Environment.NewLine);

                        messageRequests.Add(new MessageRequestModel
                        {
                            Phone = phone,
                            Message = message
                        });
                    }
                }
            }

            return messageRequests;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _bot?.Close();
            base.OnFormClosed(e);
        }
    }
}
