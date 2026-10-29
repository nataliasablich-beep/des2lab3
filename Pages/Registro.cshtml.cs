using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace des2lab3.Pages;

public class RegistroModel : PageModel
{
    [BindProperty]
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = "";

    [BindProperty]
    [Display(Name = "Apellido")]
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; } = "";

    [BindProperty]
    [Display(Name = "Correo electrónico")]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string Correo { get; set; } = "";

    [BindProperty]
    [Display(Name = "Nombre de usuario")]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [RegularExpression("^[a-zA-Z0-9_]{3,20}$", ErrorMessage = "Use entre 3 y 20 letras, números o guiones bajos.")]
    public string Usuario { get; set; } = "";

    [BindProperty]
    [Display(Name = "Edad")]
    [Required(ErrorMessage = "La edad es obligatoria.")]
    [Range(16, 120, ErrorMessage = "La edad debe ser mayor a 15 años.")]
    public int? Edad { get; set; }

    [BindProperty]
    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = "";

    [BindProperty]
    [Display(Name = "Confirmar contraseña")]
    [Required(ErrorMessage = "Debe confirmar la contraseña.")]
    [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden.")]
    [DataType(DataType.Password)]
    public string ConfirmarContrasena { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response.Cookies.Append("contrasena", Contrasena, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(1)
        });

        HttpContext.Session.SetString("usuario", Usuario);
        HttpContext.Session.SetString("nombre", Nombre + " " + Apellido);

        return RedirectToPage("/Documentos");
    }
}
