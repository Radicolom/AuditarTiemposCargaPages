using AuditarApi.Models.Dtos;
using System.Threading.Tasks;

namespace AuditarApi.Services
{
    public interface IPageSpeedService
    {
        Task<RespuestaApp<PageSpeedResultDto>> AnalyzeUrlAsync(string urlToAnalyze);
    }
}
