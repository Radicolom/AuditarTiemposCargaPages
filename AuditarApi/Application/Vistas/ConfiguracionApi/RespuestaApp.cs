public class RespuestaApp<TVista>
{
    public bool ValidacionesNegocio { get; set; }
    public List<TVista> Vista { get; set; } = new();
    public bool OperacionExitosa { get; set; }
    public string Mensaje { get; set; } = string.Empty;
}