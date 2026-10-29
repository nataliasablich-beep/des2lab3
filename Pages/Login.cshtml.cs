using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace des2lab3.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    [Display(Name = "Nombre de usuario")]
    [Required(ErrorMessage = "Ingrese su nombre de usuario.")]
    public string Usuario { get; set; } = "";

    [BindProperty]
    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "Ingrese su contraseña.")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string? usuarioGuardado = HttpContext.Session.GetString("usuario");
        string? contrasenaGuardada = Request.Cookies["contrasena"];

        if (usuarioGuardado != null && usuarioGuardado == Usuario && contrasenaGuardada == Contrasena)
        {
            return RedirectToPage("/Documentos");
        }

        ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos. Si no tiene cuenta, primero debe registrarse.");
        return Page();
    }
}
