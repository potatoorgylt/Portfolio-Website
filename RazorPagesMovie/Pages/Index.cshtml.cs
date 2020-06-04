using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnGetSetCultureAsync(string culture)
        {
            HttpContext.Response.Cookies.Append("Culture", "c=" + culture + "|uic=" + culture);
            var returnUrl = Request.Headers["Referer"].ToString();
            if (returnUrl.Contains("?culture="))
            {
                var url = returnUrl.Substring(0, returnUrl.IndexOf("?culture="));
                return Redirect(url + "?culture=" + culture);
            }
            else
            {
                return Redirect(returnUrl + "?culture=" + culture);
            }

        }
    }
}
