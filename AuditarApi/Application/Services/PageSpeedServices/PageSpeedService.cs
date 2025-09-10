using AuditarApi.Models.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuditarApi.Services
{
    public class PageSpeedService : IPageSpeedService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _apiKey;

        public PageSpeedService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _apiKey = _config["PageSpeed:ApiKey"];
        }

        public async Task<RespuestaApp<PageSpeedResultDto>> AnalyzeUrlAsync(string urlToAnalyze)
        {
            RespuestaApp<PageSpeedResultDto> respuesta = new RespuestaApp<PageSpeedResultDto>();
            try
            {
                if (string.IsNullOrEmpty(_apiKey))
                {
                    throw new System.Exception("API Key de PageSpeed no está configurada.");
                }
                var apiUrl = $"https://www.googleapis.com/pagespeedonline/v5/runPagespeed?url={System.Uri.EscapeDataString(urlToAnalyze)}&key={_apiKey}&category=PERFORMANCE";

                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    respuesta.OperacionExitosa = false;
                    respuesta.ValidacionesNegocio = false;
                    respuesta.Mensaje = "No se logro procesar acceso API";
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(jsonResponse))
                {
                    var root = document.RootElement;
                    var lighthouseResult = root.GetProperty("lighthouseResult");
                    var audits = lighthouseResult.GetProperty("audits"); // Obtenemos la sección de auditorías

                    var resultDto = new PageSpeedResultDto
                    {
                        AnalyzedUrl = lighthouseResult.GetProperty("finalUrl").GetString(),
                        PerformanceScore = (int)(lighthouseResult.GetProperty("categories").GetProperty("performance").GetProperty("score").GetDouble() * 100),

                        // Métricas modernas (valores en texto)
                        FirstContentfulPaint = ExtractMetric(audits, "first-contentful-paint"),
                        LargestContentfulPaint = ExtractMetric(audits, "largest-contentful-paint"),
                        CumulativeLayoutShift = ExtractMetric(audits, "cumulative-layout-shift"),
                        SpeedIndex = ExtractMetric(audits, "speed-index"),

                        // ---- LÍNEAS AÑADIDAS PARA COMPLETAR TU TABLA ----
                        // Métricas de tiempo (valores numéricos en ms)
                        PageLoadTimeMs = (int?)audits.GetProperty("interactive").GetProperty("numericValue").GetDouble(),
                        TimeToFirstByteMs = (int?)audits.GetProperty("server-response-time").GetProperty("numericValue").GetDouble(),
                        DomProcessingTimeMs = (int?)audits.GetProperty("mainthread-work-breakdown").GetProperty("numericValue").GetDouble()
                    };
                    respuesta.Vista = new List<PageSpeedResultDto> { resultDto };
                    respuesta.OperacionExitosa = true;
                    respuesta.ValidacionesNegocio = false;
                }
            }
            catch (Exception ex)
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
                respuesta.Mensaje = ex.Message.ToString();
            }

            return respuesta;
        }

        private Metric ExtractMetric(JsonElement audits, string auditId)
        {
            if (audits.TryGetProperty(auditId, out var audit))
            {
                return new Metric
                {
                    DisplayValue = audit.TryGetProperty("displayValue", out var displayValue) ? displayValue.GetString() : null,
                    Score = audit.TryGetProperty("score", out var score) ? score.GetDouble() * 100 : 0
                };
            }
            // Retornar un objeto por defecto o lanzar una excepción controlada
            return new Metric
            {
                DisplayValue = "No disponible",
                Score = 0
            };
        }
    }
}
