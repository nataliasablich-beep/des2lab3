using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace des2lab3.Pages;

public class DocumentosModel : PageModel
{
    private readonly IWebHostEnvironment _entorno;

    public DocumentosModel(IWebHostEnvironment entorno)
    {
        _entorno = entorno;
    }

    [BindProperty]
    public IFormFile? Archivo { get; set; }

    public string Usuario { get; set; } = "";

    public string NombreCompleto { get; set; } = "";

    public List<FileInfo> Archivos { get; set; } = new List<FileInfo>();

    public IActionResult OnGet()
    {
        string? usuario = HttpContext.Session.GetString("usuario");

        if (usuario == null)
        {
            return RedirectToPage("/Login");
        }

        Usuario = usuario;
        NombreCompleto = HttpContext.Session.GetString("nombre") ?? usuario;
        ListarArchivos();
        return Page();
    }

    public IActionResult OnPost()
    {
        string? usuario = HttpContext.Session.GetString("usuario");

        if (usuario == null)
        {
            return RedirectToPage("/Login");
        }

        Usuario = usuario;
        NombreCompleto = HttpContext.Session.GetString("nombre") ?? usuario;

        if (Archivo == null || Archivo.Length == 0)
        {
            ViewData["Mensaje"] = "Debe seleccionar un archivo.";
            ListarArchivos();
            return Page();
        }

        string[] permitidas = { ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png" };
        string extension = Path.GetExtension(Archivo.FileName).ToLower();

        if (!permitidas.Contains(extension))
        {
            ViewData["Mensaje"] = "Solo se permiten archivos PDF, DOC, DOCX, JPG o PNG.";
            ListarArchivos();
            return Page();
        }

        if (Archivo.Length > 5 * 1024 * 1024)
        {
            ViewData["Mensaje"] = "El archivo no puede superar los 5 MB.";
            ListarArchivos();
            return Page();
        }

        string carpeta = Path.Combine(_entorno.WebRootPath, "uploads", usuario);
        Directory.CreateDirectory(carpeta);

        string nombreArchivo = Path.GetFileName(Archivo.FileName);
        string ruta = Path.Combine(carpeta, nombreArchivo);

        using (FileStream flujo = new FileStream(ruta, FileMode.Create))
        {
            Archivo.CopyTo(flujo);
        }

        ViewData["Mensaje"] = "El archivo se cargó correctamente.";
        ListarArchivos();
        return Page();
    }

    private void ListarArchivos()
    {
        string carpeta = Path.Combine(_entorno.WebRootPath, "uploads", Usuario);
        Directory.CreateDirectory(carpeta);

        Archivos = new DirectoryInfo(carpeta).GetFiles()
            .OrderByDescending(a => a.LastWriteTime)
            .ToList();
    }
}
