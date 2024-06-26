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
                }
            }
        }

        private void btnSendMessages_Click(object sender, EventArgs e)
        {
            string csvFilePath = txtCsvFilePath.Text;
            if (string.IsNullOrWhiteSpace(csvFilePath) || !File.Exists(csvFilePath))
            {
                MessageBox.Show("Please select a valid CSV file.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var messageRequests = ReadCsvFile(csvFilePath);
            int totalMessages = messageRequests.Count;
            int successCount = 0;
            int errorCount = 0;
            List<string> errorMessages = new List<string>();

            _bot.InitialiseWA();
            MessageBox.Show("Please scan the QR code and click OK when ready.", "WhatsApp Web", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int currentMessageIndex = 0;
            string progressText = $"{currentMessageIndex}/{totalMessages}";
            // Set initial progress text
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
                // Update progress text
                progressText = $"{currentMessageIndex}/{totalMessages}";
                Text = $"WhatsApp Automation - Sending {progressText}";

                // Update UI to show progress text
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

        private List<MessageRequestModel> ReadCsvFile(string csvFilePath)
        {
            var messageRequests = new List<MessageRequestModel>();

            using (var reader = new StreamReader(csvFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(new[] { ',' }, 2); // Split into two parts: phone number and message
                    if (values.Length == 2)
                    {
                        messageRequests.Add(new MessageRequestModel
                        {
                            Phone = values[0].Trim(),
                            Message = values[1].Trim().Replace("\\n", Environment.NewLine) // Replace \n with actual new lines
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
