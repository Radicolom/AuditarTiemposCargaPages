public class AuditarPaginaVista
{
    public int? Id { get; set; }
    public string? Url { get; set; }
    public string? Nombre { get; set; }
    public bool? Estado { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public int? UsuarioId { get; set; }
    public string? UsuarioNombre { get; set; }
}