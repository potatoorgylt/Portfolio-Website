using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ContactFormModel Contact { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var mailbody = $@"Hallo website owner, This is a new contact request from your website: Name: {Contact.Name} LastName: {Contact.LastName} Email: {Contact.Email} Message: ""{Contact.Message}"" Cheers, The websites contact form";
            SendMail(mailbody);

            return RedirectToPage("Index");
        }

        public void OnGet()
        {
        }

        private void SendMail(string mailbody)
        {
            using (var message = new MailMessage(Contact.Email, "gynejas13@gmail.com"))
            {
                message.To.Add(new MailAddress("gynejas13@gmail.com"));
                message.From = new MailAddress(Contact.Email);
                message.Subject = "New E-Mail from my website";
                message.Body = mailbody;
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    //Or your Smtp Email ID and Password
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("gynejas13@gmail.com", "tomuliauskis");
                    // smtp.Port = 587;
                    
                    smtpClient.EnableSsl = true;
                    
                    // smtp.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
        }
    }
}
