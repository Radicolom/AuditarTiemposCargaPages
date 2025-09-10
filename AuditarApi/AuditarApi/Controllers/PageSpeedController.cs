using AuditarApi.Models.Dtos;
using AuditarApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuditarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PageSpeedController : ControllerBase
    {
        private readonly IPageSpeedService _pageSpeedService;
        private readonly PagesServices _pagesServices;

        public PageSpeedController(IPageSpeedService pageSpeedService, PagesServices pagesServices)
        {
            _pageSpeedService = pageSpeedService;
            _pagesServices = pagesServices;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze([FromBody] AnalyzeUrlRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _pageSpeedService.AnalyzeUrlAsync(request.Url);

            if (result.OperacionExitosa && !result.ValidacionesNegocio)
            {
                AuditarLogVista auditarLogVista = new AuditarLogVista();
                if (result.Vista != null && result.Vista.Count > 0)
                {
                    var pageSpeedData = result.Vista[0];
                    auditarLogVista.PerformanceScore = pageSpeedData.PerformanceScore;
                    auditarLogVista.FcpValue = pageSpeedData.FirstContentfulPaint?.DisplayValue;
                    auditarLogVista.LcpValue = pageSpeedData.LargestContentfulPaint?.DisplayValue;
                    auditarLogVista.ClsValue = pageSpeedData.CumulativeLayoutShift?.DisplayValue;
                    auditarLogVista.SpeedIndexValue = pageSpeedData.SpeedIndex?.DisplayValue;
                    auditarLogVista.PageLoadTimeMs = pageSpeedData.PageLoadTimeMs;
                    auditarLogVista.TimeToFirstByteMs = pageSpeedData.TimeToFirstByteMs;
                    auditarLogVista.DomProcessingTimeMs = pageSpeedData.DomProcessingTimeMs;
                    auditarLogVista.EstadoAuditarPagina = true;
                    auditarLogVista.AuditarPaginaId = request.Id;

                    var logResult = await _pagesServices.AddAuditarLogAsync(auditarLogVista);
                    if (!logResult.OperacionExitosa)
                    {
                        result.OperacionExitosa = false;
                        result.ValidacionesNegocio = false;
                        result.Mensaje = "Error al guardar el log de auditoría.";

                        return BadRequest(result);
                    }
                }
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
