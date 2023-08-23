# WhatsApp Automation

This project contains a C# bot aimed at automating messaging on WhatsApp. The bot uses Selenium and ChromeDriver to automatically access WhatsApp Web and send messages to a specified phone number.

## Getting Started

These instructions will guide you on setting up and getting started with the project in your local development environment.

### Prerequisites

To run this project, you will need the following prerequisites:

- .NET Core SDK
- ChromeDriver
- Google Chrome web browser

### Usage

This section explains how to use the WhatsApp automation bot.

1. Create an instance of the WhatsAppBot class using:

   ```csharp
   var bot = new WhatsAppBot();
   ```

   or create an instance using existing user data and profile directory:

   ```csharp
   var userDataDirectory = "path/to/user/data";
   var profileDirectory = "path/to/profile/directory";
   var bot = new WhatsAppBot(userDataDirectory, profileDirectory);
   ```

2. Create a message sending request and send the message using the SendMessage method:

   ```csharp
   var messageRequest = new MessageRequestModel
   {
       Phone = "recipient_phone_number",
       Message = "Your message here"
   };

   var response = bot.SendMessage(messageRequest);

   if (response.Success)
   {
       Console.WriteLine("Message sent successfully!");
   }
   else
   {
       Console.WriteLine("Message sending error: " + response.Exception.Message);
   }
   ```

3. Don't forget to close the bot session when you're done:

   ```csharp
   bot.Close();
   ```

## License
This project is licensed under the [MIT License](LICENSE). See the license file for details.

## Issues, Feature Requests or Support
Please use the [New Issue](https://github.com/akinbicer/whatsapp-automation/issues/new) button to submit issues, feature requests or support issues directly to me. You can also send an e-mail to akin.bicer@outlook.com.tr.
