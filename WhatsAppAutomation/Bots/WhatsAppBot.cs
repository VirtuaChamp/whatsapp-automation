using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WhatsAppAutomation.Models.Messages.Requests;
using WhatsAppAutomation.Models.Messages.Responses;
using System.Windows.Forms; // Ensure this is included for Clipboard operations

namespace WhatsAppAutomation.Bots
{
    public class WhatsAppBot
    {
        private readonly IWebDriver _webDriver;

        public WhatsAppBot()
        {
            _webDriver = new ChromeDriver();
        }

        public WhatsAppBot(string userDataDirectory, string profileDirectory)
        {
            var options = new ChromeOptions();
            options.AddArgument($"--user-data-dir={userDataDirectory}");
            options.AddArgument($"--profile-directory={profileDirectory}");
            options.AcceptInsecureCertificates = true;

            _webDriver = new ChromeDriver(options);
        }

        public void InitialiseWA()
        {
            try
            {
                _webDriver.Navigate().GoToUrl("https://web.whatsapp.com/");
            }
            catch (Exception ex)
            {
                // Handle exception or log error
            }
        }

        public MessageResponseModel SendMessage(MessageRequestModel model)
        {
            try
            {
                _webDriver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={model.Phone}");
                Thread.Sleep(10000);

                // Copy message to clipboard
                Clipboard.SetText(model.Message);

                Thread.Sleep(2000);

                _webDriver.SwitchTo().ActiveElement().SendKeys(OpenQA.Selenium.Keys.Control + "v");

                Thread.Sleep(2000);

                _webDriver.SwitchTo().ActiveElement().SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(2000);

                return new MessageResponseModel(true, model);
            }
            catch (Exception ex)
            {
                return new MessageResponseModel(false, model, ex);
            }
        }

        public void Close()
        {
            _webDriver.Quit();
        }
    }
}
