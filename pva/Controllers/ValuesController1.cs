using Microsoft.AspNetCore.Mvc;
using pva.Models;
using System.Net.Mail;

namespace pva.Controllers
{

    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateValue([FromBody] Value newValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authToken = Request.Headers["Authorization"];
            if (authToken != "123456789")
            {
                return Unauthorized();
            }

            var station = await _context.Stations.FindAsync(newValue.StationId);
            if (station == null)
            {
                return BadRequest("StationId does not exist.");
            }

            bool isFloodWarning = newValue.Level >= station.FloodLevel;
            bool isDroughtWarning = newValue.Level <= station.DroughtLevel;

            _context.Values.Add(newValue);
            await _context.SaveChangesAsync();

            if (isFloodWarning)
            {
                await SendEmailAsync(
                    "info@rivertechsolutions.com",
                    $"{station.Name} - Flood Warning Alert",
                    $"Flood warning level exceeded at station '{station.Name}'. Current level: {newValue.Level}. Maximum value: {station.FloodLevel}."
                );
            }

            if (isDroughtWarning)
            {
                await SendEmailAsync(
                    "info@rivertechsolutions.com",
                    $"{station.Name} - Drought Warning Alert",
                    $"Drought warning level exceeded at station '{station.Name}'. Current level: {newValue.Level}. Minimum value: {station.DroughtLevel}."
                );
            }

            return CreatedAtAction(nameof(GetValue), new { id = newValue.ValueId.ToString() }, newValue);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FindAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient("smtp.example.com")
            {
                Host = "localhost",
                Port = 25,
                Credentials = null,
                EnableSsl = false,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@wlms.com", "WLMS - Water level measuring system"),
                Subject = subject,
                Body = message,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(new MailAddress(toEmail));

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

