using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace Event_Registration_System.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailjetClient _client;

        // Constructor now initializes MailjetClient
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MailjetClient(_configuration["Mailjet:ApiKey"], _configuration["Mailjet:SecretKey"]);
        }

        // Updated SendEmailAsync method to return success status
        public async Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string textPart, string htmlPart)
        {
            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, "hamadanjo@gmail.com")
            .Property(Send.FromName, "Event Registration")
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlPart)  // HTML version of the email
            .Property(Send.Recipients, new JArray
            {
                new JObject
                {
                    { "Email", toEmail },
                    { "Name", toName }
                }
            });

            // Execute the request
            MailjetResponse response = await _client.PostAsync(request);

            // Return whether the request was successful
            return response.IsSuccessStatusCode;
        }
    }
}
