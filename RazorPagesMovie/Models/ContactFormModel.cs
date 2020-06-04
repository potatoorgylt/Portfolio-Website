using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessageResourceName = "RequiredName", ErrorMessageResourceType = typeof(Resources.ContactUsTexts))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "RequiredLastName", ErrorMessageResourceType = typeof(Resources.ContactUsTexts))]
        public string LastName { get; set; }
        [Required(ErrorMessageResourceName = "RequiredEmail", ErrorMessageResourceType = typeof(Resources.ContactUsTexts))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resources.ContactUsTexts))]
        public string Message { get; set; }
    }
}
