public class UsuarioVista
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Documento { get; set; }
    public string? Correo { get; set; }
    public bool? EmailConfirmado { get; set; }
    public string? Telefono { get; set; }
    public bool? TelefonoConfirmado { get; set; }
    public bool? DobleFactor { get; set; }
    public int? Intentos { get; set; }
    public bool? Estado { get; set; }
    public int? RolId { get; set; }
    public string? RolNombre { get; set; }
    public string? Password { get; set; }
}