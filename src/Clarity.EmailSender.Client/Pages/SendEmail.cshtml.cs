using Clarity.EmailSender.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Clarity.EmailSender.Client.Pages
{
    public class SendEmailModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public SendEmailModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public EmailRequest Email { get; set; } = new EmailRequest();

        public string? ResultMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("EmailApi");

            var content = new StringContent(JsonSerializer.Serialize(Email), Encoding.UTF8, Application.Json);

            try
            {
                var response = await client.PostAsync("api/email", content);
                ResultMessage = response.IsSuccessStatusCode ? "Email send initiated successfully!" : $"Failed to send email: {await response.Content.ReadAsStringAsync()}";
            }
            catch (Exception ex)
            {
                ResultMessage = $"Error contacting API: {ex.Message}";
            }

            return Page();
        }
    }
}
