using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace des2lab3.Pages;

public class SalirModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        Response.Cookies.Delete("contrasena");
        return RedirectToPage("/Index");
    }
}
