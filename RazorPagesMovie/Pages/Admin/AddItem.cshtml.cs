using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Admin
{
    [Authorize]
    public class AddItemModel : PageModel
    {
        private readonly IPortfolioService portfolioService;

        [FromRoute]
        public int? Id { get; set; }

        public bool IsNewPortfolioItem
        {
            get { return Id == null; }
        }

        [BindProperty]
        public Portfolio Portfolio { get; set; }
        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public AddItemModel(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }
        public async Task OnGetAsync()
        {

            Portfolio = await portfolioService.FindAsync(Id.GetValueOrDefault()) ?? new Portfolio();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var portfolio = await portfolioService.FindAsync(Id.GetValueOrDefault()) ?? new Portfolio();

            portfolio.Name = Portfolio.Name;
            portfolio.Description = Portfolio.Description;

            if (UploadedImage != null)
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    await UploadedImage.CopyToAsync(stream);
                    portfolio.Image = stream.ToArray();
                    portfolio.ImageContentType = UploadedImage.ContentType;
                }
            }


            await portfolioService.SaveAsync(portfolio);

            return RedirectToPage("/Index", new { id = portfolio.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await portfolioService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}
