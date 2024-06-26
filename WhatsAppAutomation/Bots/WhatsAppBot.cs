using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WhatsAppAutomation.Models.Messages.Requests;
using WhatsAppAutomation.Models.Messages.Responses;

namespace WhatsAppAutomation.Bots;

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
        }
    }


    public MessageResponseModel SendMessage(MessageRequestModel model)
    {
        try
        {
            _webDriver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={model.Phone}");
            Thread.Sleep(10000);

            var messageInput = _webDriver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p"));

            var lines = model.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                messageInput.SendKeys(line);
                messageInput.SendKeys(Keys.Shift + Keys.Enter);
            }

            messageInput.SendKeys(Keys.Backspace);

            messageInput.SendKeys(Keys.Enter);

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